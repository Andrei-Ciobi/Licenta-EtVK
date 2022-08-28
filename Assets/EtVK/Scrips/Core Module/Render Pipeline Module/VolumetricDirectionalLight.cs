using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Shader;

namespace EtVK.Render_Pipeline_Module
{
    [System.Serializable]
    public class VolumetricDirectionalLightSettings
    {
        [SerializeField] [Range(0.1f, 1f)] private float resolutionScale = 0.5f;
        [SerializeField] [Range(0.1f, 1f)] private float intensity = 1f;
        [SerializeField] [Range(0.1f, 1f)] private float blurWidth = 0.85f;

        public float ResolutionScale => resolutionScale;

        public float Intensity => intensity;

        public float BlurWidth => blurWidth;
    }
    public class VolumetricDirectionalLight : ScriptableRendererFeature
    {
        class DirectionalLightPass : ScriptableRenderPass
        {
            private readonly List<ShaderTagId> shaderTagIdList = new();
            private readonly RTHandle  renderTarget;
            private readonly float resolutionScale;
            private readonly float intensity;
            private readonly float blurWidth;
            private readonly Material renderTargetMaterial;
            private readonly Material radialBlurMaterial;

            private RenderTargetIdentifier cameraColorTargetIdent;
            private FilteringSettings filteringSettings = new(RenderQueueRange.opaque);
            public DirectionalLightPass(VolumetricDirectionalLightSettings settings)
            {
                
                renderTarget = RTHandles.Alloc("_OccludersMap", name: "_OccludersMap");
                resolutionScale = settings.ResolutionScale;
                intensity = settings.Intensity;
                blurWidth = settings.BlurWidth;
                renderTargetMaterial = new Material(Find("Hidden/RW/UnlitColor"));
                radialBlurMaterial = new Material(Find("Hidden/RW/RadialBlur"));
                
                shaderTagIdList.Add(new ShaderTagId("UniversalForward"));
                shaderTagIdList.Add(new ShaderTagId("UniversalForwardOnly"));
                shaderTagIdList.Add(new ShaderTagId("LightweightForward"));
                shaderTagIdList.Add(new ShaderTagId("SRPDefaultUnlit"));
            }

            // This method is called before executing the render pass.
            // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
            // When empty this render pass will render to the active camera render target.
            // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
            // The render pipeline will ensure target setup and clearing happens in a performant manner.
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                // 1
                var cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                
                // 2
                cameraTextureDescriptor.depthBufferBits = 0;
                cameraTextureDescriptor.colorFormat = RenderTextureFormat.ARGB32;
                
                // 3
                cameraTextureDescriptor.width = Mathf.RoundToInt(cameraTextureDescriptor.width * resolutionScale);
                cameraTextureDescriptor.height = Mathf.RoundToInt(cameraTextureDescriptor.height * resolutionScale);
                
                // 4
                cmd.GetTemporaryRT(PropertyToID(renderTarget.name), cameraTextureDescriptor, FilterMode.Bilinear);
                
                // 5
                ConfigureTarget(renderTarget);
            }

            // Here you can implement the rendering logic.
            // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
            // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
            // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (!renderTargetMaterial || !radialBlurMaterial)
                    return;

                CommandBuffer cmd = CommandBufferPool.Get();

                using (new ProfilingScope(cmd, new ProfilingSampler("VolumetricDirectionalLight")))
                {
                    context.ExecuteCommandBuffer(cmd);
                    cmd.Clear();
                    
                    Camera camera = renderingData.cameraData.camera;
                    context.DrawSkybox(camera);
                    
                    var drawSettings = CreateDrawingSettings(shaderTagIdList, ref renderingData, SortingCriteria.CommonOpaque);
                    drawSettings.overrideMaterial = renderTargetMaterial;
                    
                    context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);
                    
                    
                    // 1
                    var sunDirectionWorldSpace = RenderSettings.sun.transform.forward;
                    // 2
                    var cameraPositionWorldSpace = camera.transform.position;
                    // 3
                    var sunPositionWorldSpace = cameraPositionWorldSpace + sunDirectionWorldSpace;
                    // 4
                    var sunPositionViewportSpace = camera.WorldToViewportPoint(sunPositionWorldSpace);

                    radialBlurMaterial.SetVector("_Center",
                        new Vector4(sunPositionViewportSpace.x, sunPositionViewportSpace.y, 0, 0));
                    radialBlurMaterial.SetFloat("_Intensity", intensity);
                    radialBlurMaterial.SetFloat("_BlurWidth", blurWidth);
                    
                    
                    Blit(cmd, PropertyToID(renderTarget.name), cameraColorTargetIdent, 
                        radialBlurMaterial);
                    
                }
                
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            // Cleanup any allocated resources that were created during the execution of this render pass.
            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(PropertyToID(renderTarget.name));
            }
            
            public void SetCameraColorTarget(RenderTargetIdentifier cameraColorTargetIdent)
            {
                this.cameraColorTargetIdent = cameraColorTargetIdent;
            }
            
            
        }

        DirectionalLightPass m_ScriptablePass;
        public VolumetricDirectionalLightSettings settings = new();
        /// <inheritdoc/>
        public override void Create()
        {
            m_ScriptablePass = new DirectionalLightPass(settings);

            // Configures where the render pass should be injected.
            m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        // Here you can inject one or multiple render passes in the renderer.
        // This method is called when setting up the renderer once per-camera.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(m_ScriptablePass);
            m_ScriptablePass.SetCameraColorTarget(renderer.cameraColorTarget); 
        }
    }
}


