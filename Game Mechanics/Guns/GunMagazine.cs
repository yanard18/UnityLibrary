using System.Collections;
using UnityEngine;

namespace DenizYanar.Guns
{
    public class GunMagazine : MonoBehaviour
    {
        public enum EMagazineStatus
        {
            FULL,
            LOADED,
            EMPTY
        };

        private enum EReloadStatus
        {
            NORMAL,
            RELOADING
        };
        
        private EReloadStatus _reloadStatus;

        [SerializeField] private int _magazineCapacity = 30;
        [SerializeField] private int _ammo = 30;
        [SerializeField] private int _totalAmmo = 90;
        [SerializeField] private bool _unlimitedAmmo;
        [SerializeField] private bool _unlimitedReload;
        [SerializeField] private float _reloadDuration = 1.0f;

        public EMagazineStatus MagazineStatus;

        

        public bool SpendAmmo()
        {
            if(_unlimitedAmmo || _ammo <= 0) return false;

            if (_ammo == _magazineCapacity)
                MagazineStatus = EMagazineStatus.FULL;
            else if (_ammo > 0 && _ammo < _magazineCapacity)
                MagazineStatus = EMagazineStatus.LOADED;
            else if (_ammo == 0)
                MagazineStatus = EMagazineStatus.EMPTY;

            return true;
        }
        
        public void Reload()
        {
            if (MagazineStatus == EMagazineStatus.FULL || _reloadStatus == EReloadStatus.RELOADING || _totalAmmo <= 0) return;
            StartCoroutine(StartReloadDuration(_reloadDuration));
        }

        private void FinishReload()
        {
            var reloadAmount = _magazineCapacity - _ammo;
            if (_totalAmmo < reloadAmount && !_unlimitedAmmo)
                reloadAmount = _totalAmmo;

            if(!_unlimitedReload)
                _totalAmmo -= reloadAmount;
            
            _ammo += reloadAmount;

            if (_ammo == _totalAmmo)
                MagazineStatus = EMagazineStatus.FULL;
            else if (_ammo < _totalAmmo)
                MagazineStatus = EMagazineStatus.LOADED;
        }


        private IEnumerator StartReloadDuration(float duration)
        {
            _reloadStatus = EReloadStatus.RELOADING;
            yield return new WaitForSeconds(duration);
            _reloadStatus = EReloadStatus.NORMAL;
            FinishReload();
        }

    }
}
