using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.UI_Module.Core
{
    public abstract class BaseUiData : ScriptableObject
    {
        [SerializeField] protected GameUi gameUi;

        public GameUi GameUi => gameUi;
    }
}