namespace EtVK.Items_Module.Weapons
{
    public interface IWeaponDamageable
    {
        public float DealDamage();
        public void DrawWeapon();
        public void WithdrawWeapon();
    }
}