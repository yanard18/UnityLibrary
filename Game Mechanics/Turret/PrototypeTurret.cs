using UnityEngine;

namespace DenizYanar
{
    public class PrototypeTurret : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayerMask;
        private TurretGunInputReader _input;

        private void Awake() => _input = GetComponentInChildren<TurretGunInputReader>();
        private void Update()
        {
            var player = Physics2D.OverlapCircle(transform.position, 15.0f, _playerLayerMask);
            if (player is null)
            {
                _input.InvokeOnFireCancelled();
                return;
            }

            var dir = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(Vector3.forward * 90);
            _input.InvokeOnFireStarted();
        }
    }
}
