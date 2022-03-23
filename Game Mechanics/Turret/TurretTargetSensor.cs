using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretTargetSensor : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private LayerMask _obstacleLayerMask;
        [SerializeField] private float _range = 20.0f;
        [SerializeField] private float _angle = 60.0f;
        
        public Transform Detect()
        {
            var t = Physics2D.OverlapCircle(transform.position, _range, _targetLayerMask);

            if (t is null)
                return null;
            
            var dir = t.transform.position - transform.position;

            var obstacleHit = Physics2D.Raycast(transform.position, dir.normalized, dir.magnitude, _obstacleLayerMask);
            if (obstacleHit)
                return null;
            
            var angle = Vector2.Angle(dir, transform.right);
            

            return Mathf.Abs(angle) <= _angle ? t.transform : null;
        }
        
        
        
    }
}
