using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretRotor : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private float _velocity;
        public void LookPosition(Vector2 pos)
        {
            var dir = pos - (Vector2) transform.position;
            var desiredAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var selfAngle = transform.eulerAngles.z;
            //var angle = Mathf.MoveTowardsAngle(selfAngle, desiredAngle, Time.deltaTime * 120.0f);
            //var angle = Mathf.SmoothDampAngle(selfAngle, desiredAngle, ref _velocity, 1/ (_speed * Time.deltaTime), _maxSpeed);


            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.AngleAxis(desiredAngle, Vector3.forward), Time.deltaTime * _speed);
        }
    }
}
