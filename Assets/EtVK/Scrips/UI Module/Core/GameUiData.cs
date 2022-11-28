using System.Collections.Generic;
using EtVK.Core.Utyles;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameUiData", order = 1)]
    public class GameUiData : ScriptableObject
    {
        [SerializeField] private VisualTreeAsset loadingUi;
        [SerializeField] private List<SerializableSet<GameUi, VisualTreeAsset>> uiList;
        [SerializeField] private List<BaseUiData> uiDataList;

        public VisualTreeAsset LoadingUi => loadingUi;

        public VisualTreeAsset GetUi(GameUi ui)
        {
            return uiList.Find(x => x.GetKey() == ui)?.GetValue();
        }

        public T GetUiData<T>() where T : BaseUiData
        {
            var uiData = uiDataList.Find(x => x is T);
            if (uiData == null)
            {
                Debug.LogError($"No ui data set for {typeof(T)}");
            }

            return uiData as T;
        }
    }
}