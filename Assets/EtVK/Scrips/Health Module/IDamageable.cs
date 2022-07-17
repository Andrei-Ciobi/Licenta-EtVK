namespace EtVK.Scrips.Health_Module
{
    public interface IDamageable
    {
        public void TakeHit(float damage);
        public void Die();
    }
}