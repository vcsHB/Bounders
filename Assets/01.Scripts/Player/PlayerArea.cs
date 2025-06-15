using Core.GameSystem;
using UnityEngine;
namespace Players
{

    public class PlayerArea : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _id;
        [SerializeField] private Health _ownerHealth;
        
        public void ApplyDamage(float damage)
        {
            _ownerHealth.ApplyDamage(damage);
            GameManager.Instance.GameStart(_id);
        }
    }
}