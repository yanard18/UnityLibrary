using UnityEngine;

namespace DenizYanar.Guns
{
    public class Gun : MonoBehaviour
    {
        private bool _isFiring;
        private float _backupFireCooldown;
        
        private GunInputReader _input;
        private GunLauncher _launcher;
        private GunMagazine _magazine;

        
        
        [SerializeField] private float _fireCooldown = 1.0f;

        #region Monobehaviour
        
        private void OnEnable()
        {
            _input.OnFireStarted += FireStarted;
            _input.OnFireCancelled += FireCancelled;
            _input.OnReload += Reload;
        }

        private void OnDisable()
        {
            _input.OnFireStarted -= FireStarted;
            _input.OnFireCancelled -= FireCancelled;
            _input.OnReload -= Reload;
        }

        private void Awake()
        {
            _backupFireCooldown = _fireCooldown;
            
            _input = GetComponentInChildren<GunInputReader>();
            _magazine = GetComponentInChildren<GunMagazine>();
            _launcher = GetComponentInChildren<GunLauncher>();
        }
        
        #endregion

        private void FireStarted()
        {
            _isFiring = true;
        }

        private void FireCancelled()
        {
            _isFiring = false;
        }

        private void Reload()
        {
            _magazine.Reload();
        }


        private void Update()
        {
            FireCycle();
        }

        private void FireCycle()
        {
            if (_fireCooldown > 0)
                _fireCooldown -= Time.deltaTime;
            else
            {
                if (!_isFiring) return;
                if (_magazine.SpendAmmo() is false) return;
                _launcher.Shot();
                _fireCooldown = _backupFireCooldown;
            }
        }
    }
}
