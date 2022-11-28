using EtVK.Core.Manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Components
{
    public class VersionUiComponent : VisualElement
    {
        private const string versionStyleSheet = "UiStyles/GameVersion";
        public new class UxmlFactory : UxmlFactory<VersionUiComponent, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public VersionUiComponent()
        {
            Resources.Load<StyleSheet>(versionStyleSheet);
            var versionLabel = new Label();
            versionLabel.AddToClassList("version-label");

            AddToClassList("version-container");
            Add(versionLabel);
            
            if(!Application.isPlaying)
            {
                versionLabel.text = "version 0.1 beta";
                return;
            }
            
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        private void OnGeometryChange(GeometryChangedEvent evt)
        {
            var versionLabel = this.Q<Label>();
            if(versionLabel == null || GameManager.Instance == null)
                return;
            versionLabel.text = GameManager.Instance.GameData.Version;
            
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
    }
}