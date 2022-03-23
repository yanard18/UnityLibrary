using System;
using UnityEngine;

namespace DenizYanar.Turret
{
    
    [RequireComponent(typeof(LineRenderer))]
    public class TurretLaserSensor : MonoBehaviour
    {
        private LineRenderer _lr;

        [SerializeField] private float _range = 20.0f;

        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private LayerMask _targetLayer;


        public bool IsDetected;

        private void Awake() => _lr = GetComponent<LineRenderer>();

        private void Update()
        {
            var localTransform = transform;
            Vector2 pos = localTransform.position;
            
            SetLineRenderer(pos, localTransform);
        }

        public bool HandleDetection()
        {
            var hitTarget = Physics2D.Raycast(transform.position, transform.right, _range, _targetLayer);
            return hitTarget;
        }

        private void SetLineRenderer(Vector2 pos, Transform localTransform)
        {
            var hit = Physics2D.Raycast(pos, localTransform.right, _range, _obstacleLayer);

            _lr.SetPosition(0, pos);

            if (hit)
                _lr.SetPosition(1, hit.point);
            else
                _lr.SetPosition(1, (Vector2) (localTransform.right * 20) + pos);
        }
        
    }
}
