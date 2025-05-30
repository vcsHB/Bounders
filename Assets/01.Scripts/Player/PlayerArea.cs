using UnityEngine;
namespace Players
{

    public class PlayerArea : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _ownerHealth;
        
        public void ApplyDamage(float damage)
        {
            _ownerHealth.ApplyDamage(damage);
        }
    }
}