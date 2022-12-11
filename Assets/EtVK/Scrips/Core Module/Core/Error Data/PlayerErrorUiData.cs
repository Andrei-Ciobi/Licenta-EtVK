using UnityEngine;

namespace EtVK.Core.Error_Data
{
    [CreateAssetMenu( menuName = "ScriptableObjects/UiErrorData/Player")]
    public class PlayerErrorUiData : ScriptableObject
    {
        [SerializeField] private string attackWithNoWeaponDraw;
        [SerializeField] private string noStamina;
        [SerializeField] private string abilityOnCd;

        public string AttackWithNoWeaponDraw => attackWithNoWeaponDraw;
        public string NoStamina => noStamina;

        public string AbilityOnCd => abilityOnCd;
    }
}