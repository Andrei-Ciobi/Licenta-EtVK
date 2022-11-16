using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Character_Creation
{
    public class CharacterCreationManager : BaseUiManager<CharacterCreationManager>
    {
        public new class UxmlFactory : UxmlFactory<CharacterCreationManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }
        
        
        
    }
}