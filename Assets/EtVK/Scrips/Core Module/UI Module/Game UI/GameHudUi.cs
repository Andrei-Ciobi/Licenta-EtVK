using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Game_UI
{
    public class GameHudUi : BaseGameUi
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider staminaBar;
        private GameUiManager gameUiManager;
        private bool open;
        public override void Initialize(GameUiManager manager)
        {
            gameUiManager = manager;
        }

        public override void OnOpen()
        {
            open = true;
        }

        public override void OnClose()
        {
            open = false;
        }

        public void UpdateHealthBar(int amount)
        {
            healthBar.value = amount;
        }

        public void UpdateStaminaBar(int amount)
        {
            staminaBar.value = amount;
        }

        public void SetMaxStamina(int maxValue)
        {
            staminaBar.maxValue = maxValue;
            Debug.Log(staminaBar.maxValue);
        }
    }
}