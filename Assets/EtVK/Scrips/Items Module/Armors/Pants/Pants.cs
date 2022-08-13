using UnityEngine;

namespace EtVK.Items_Module.Armors.Pants
{
    public class Pants : Armor
    {
        [SerializeField] private PantsData pantsData;

        private void Awake()
        {
            armorData = pantsData;
            InitializeReferences();
        }
    }
}