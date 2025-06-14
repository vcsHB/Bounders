using Players;
using UnityEngine;
namespace PongGameSystem
{

    public class BallDamageCaster : MonoBehaviour
    {
        [SerializeField] private BallComboCounter _comboCounter;
        
        [SerializeField] private float _detectRadius = 0.5f;
        [SerializeField] private LayerMask _targetLayer;
        private Collider[] _hits = new Collider[2];
        public bool CastDamage()
        {
            int amount = Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, _hits, _targetLayer);
            for (int i = 0; i < amount; i++)
            {
                if (_hits[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_comboCounter.GetComboDamage);
                    _comboCounter.ResetCombo();
                    return true;
                }
            }
            return false;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
        }
#endif
    }
}