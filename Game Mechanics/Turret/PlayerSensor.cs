using UnityEngine;

namespace DenizYanar.Turret
{
    public class PlayerSensor : MonoBehaviour
    {
        [SerializeField] private float _range = 20.0f;
        [SerializeField] private LayerMask _playerLayer;

        
        
        public Vector2 FindTarget()
        {
            var p = Physics2D.OverlapCircle(transform.position, _range, _playerLayer);
            return p.transform.position;
        }
    }
}
