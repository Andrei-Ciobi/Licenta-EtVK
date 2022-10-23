using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//using  https://alexanderameye.github.io/notes/edge-detection-outlines/ as a base for this script, including comments
namespace EtVK.Render_Pipeline_Module
{
    public class CustomDepthFeature : ScriptableRendererFeature
    {
    
        public Material material;
        public RenderPassEvent renderPassEvent;
        public LayerMask layerMask = -1;

        class Pass : ScriptableRenderPass
        {
            private RenderTargetHandle handle { get; set; }
            internal RenderTextureDescriptor descriptor { get; private set; }
            public Material blitMaterial = null;
            private FilteringSettings m_FilteringSettings;
            string m_ProfilerTag = "Custom Depth Pass";
            ShaderTagId m_ShaderTagId = new ShaderTagId("DepthOnly");

            public Pass(RenderQueueRange renderQueueRange, LayerMask layerMask, Material material)
            {
                m_FilteringSettings = new FilteringSettings(renderQueueRange, layerMask);
                blitMaterial = material;
            }

            public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle depthAttachmentHandle)
            {
                this.handle = depthAttachmentHandle;
                baseDescriptor.colorFormat = RenderTextureFormat.R16;
                baseDescriptor.depthBufferBits = 0;
                descriptor = baseDescriptor;
            }

            // This method is called before executing the render pass.
            // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
            // When empty this render pass will render to the active camera render target.
            // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
            // The render pipeline will ensure target setup and clearing happens in an performance manner.
            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {

                // descriptor.colorFormat = RenderTextureFormat.R16;
                cmd.GetTemporaryRT(handle.id, descriptor);
                ConfigureTarget(handle.Identifier());
                //VERY IMPORTANT that it clears to white
                ConfigureClear(ClearFlag.All, Color.white);
            }

            // Here you can implement the rendering logic.
            // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
            // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
            // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);

                //this should be replaced with ProfilingScope
                using (new ProfilingSample(cmd, m_ProfilerTag))
                {
                    context.ExecuteCommandBuffer(cmd);
                    cmd.Clear();
                    SortingCriteria sortingCriteria = SortingCriteria.CommonTransparent;
                    var sortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
                    var drawSettings = CreateDrawingSettings(m_ShaderTagId, ref renderingData, sortFlags);
                    drawSettings.perObjectData = PerObjectData.None;

                    ref CameraData cameraData = ref renderingData.cameraData;
                    Camera camera = cameraData.camera;

                    drawSettings.overrideMaterial = blitMaterial;

                    context.DrawRenderers(renderingData.cullResults, ref drawSettings,
                        ref m_FilteringSettings);

                    cmd.SetGlobalTexture("_CameraDepth2Texture", handle.id);
                }

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            /// Cleanup any allocated resources that were created during the execution of this render pass.
            public override void FrameCleanup(CommandBuffer cmd)
            {
                if (handle != RenderTargetHandle.CameraTarget)
                {
                    cmd.ReleaseTemporaryRT(handle.id);
                    handle = RenderTargetHandle.CameraTarget;
                }
            }
        }

        Pass pass;
        RenderTargetHandle renderTexture;

        public override void Create()
        {
            //if you want to filter just transpprent objects, you can useRenderQueueRangeRenderQueueRange.transparent
            pass = new Pass(RenderQueueRange.all, layerMask, material);
            pass.renderPassEvent = renderPassEvent;

            renderTexture.Init("_CameraDepth2Texture");
        }

        // Here you can inject one or multiple render passes in the renderer.
        // This method is called when setting up the renderer once per-camera.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            pass.Setup(renderingData.cameraData.cameraTargetDescriptor, renderTexture);
            renderer.EnqueuePass(pass);
        }
    }
}