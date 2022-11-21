using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace EtVK.UI_Module.Components
{
    public class ResourceBarUiComponent : VisualElement, INotifyValueChanged<float>
    {
        public enum ResourceType
        {
            Health,
            Stamina,
            Magick
        }
        
        public enum FillType
        {
            Horizontal,
            Vertical
        }

        public int width { get; set; }
        public int height { get; set; }
        public Color backgroundColor { get; set; }
        public Color overlayColor { get; set; }
        public ResourceType resourceType;
        public FillType fillType;

        public float value
        {
            get => Mathf.Clamp(m_value, 0f, 1f);
            set
            {
               if(EqualityComparer<float>.Default.Equals(m_value, value)) 
                   return;

               if (panel != null)
               {
                   using (ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(m_value, value))
                   {
                       pooled.target = this;
                       SetValueWithoutNotify(value);
                       SendEvent(pooled);
                   }
               }
               else
               {
                   SetValueWithoutNotify(value);
               }
            }
        }

        private float m_value;
        private VisualElement overlay;

        public new class UxmlFactory : UxmlFactory<ResourceBarUiComponent, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlIntAttributeDescription m_width = new() {name = "width", defaultValue = 300};
            private readonly UxmlIntAttributeDescription m_height = new() {name = "height", defaultValue = 50};
            private readonly UxmlFloatAttributeDescription m_value = new() {name = "value", defaultValue = 1};
            
            private readonly UxmlEnumAttributeDescription<ResourceType> m_resourceType = new()
                {name = "resource-type", defaultValue = 0};
            private readonly UxmlEnumAttributeDescription<FillType> m_fillType = new()
                {name = "fill-type", defaultValue = 0};
            
            private readonly UxmlColorAttributeDescription m_overlayColor = new()
                {name = "overlay-color", defaultValue = Color.black};
            private readonly UxmlColorAttributeDescription m_backgroundColor = new()
                {name = "background-color", defaultValue = Color.white};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as ResourceBarUiComponent;
                ate.width = m_width.GetValueFromBag(bag, cc);
                ate.height = m_height.GetValueFromBag(bag, cc);
                ate.value = m_value.GetValueFromBag(bag, cc);
                ate.resourceType = m_resourceType.GetValueFromBag(bag, cc);
                ate.fillType = m_fillType.GetValueFromBag(bag, cc);
                ate.backgroundColor = m_backgroundColor.GetValueFromBag(bag, cc);
                ate.overlayColor = m_overlayColor.GetValueFromBag(bag, cc);

                ate.Clear();

                ate.style.width = ate.width;
                ate.style.height = ate.height;
                ate.style.backgroundColor = ate.backgroundColor;
                ate.AddToClassList("resource-bar-overlay");

                var overlay = new VisualElement();
                ate.overlay = overlay;
                ate.Add(overlay);
                overlay.style.width = ate.width;
                overlay.style.height = ate.height;
                overlay.style.backgroundColor = ate.overlayColor;
                overlay.style.transformOrigin = new TransformOrigin(0, 100, 0);
                overlay.AddToClassList("resource-bar-background");
                
                ate.RegisterValueChangedCallback(ate.UpdateBar);
                ate.FillBar();
                
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get{yield break;} }
        }
        
        public void SetValueWithoutNotify(float newValue)
        {
            m_value = newValue;
        }

        public void UpdateBar(ChangeEvent<float> evt)
        {
            FillBar();
        }

        private void FillBar()
        {
            overlay.style.scale = fillType == FillType.Horizontal
                ? new Scale(new Vector3(value, 1, 0))
                : new Scale(new Vector3(1, value, 0));
        }
    }
}