using System.Collections;
using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretBrain : MonoBehaviour
    {
        private enum ETurretState
        {
            PASSIVE,
            PATROL,
            ATTACK
        };

        private ETurretState _turretState;


        private Transform _target;
        
        private TurretGunInputReader _gun;
        private TurretLaserSensor _laser;
        private TurretRotor _rotor;
        private TurretTargetSensor _sensor;

        #region Monobehaviour

        private void Awake()
        {
            _laser = GetComponentInChildren<TurretLaserSensor>();
            _sensor = GetComponentInChildren<TurretTargetSensor>();
            _gun = GetComponentInChildren<TurretGunInputReader>();
            _rotor = GetComponentInChildren<TurretRotor>();
        }
        
        private void Update() => Tick();

        #endregion

        private void Tick()
        {
            if (_laser.HandleDetection() && _turretState != ETurretState.ATTACK)
            {
                _turretState = ETurretState.ATTACK;
                _gun.InvokeOnFireStarted();
            }
            else if (_laser.HandleDetection() is false && _turretState == ETurretState.ATTACK)
            {
                _turretState = ETurretState.PATROL;
                _gun.InvokeOnFireCancelled();
            }

            var target = _sensor.Detect();
            if(target is {})
                _rotor.LookPosition(target.position);

        }

        

        private IEnumerator PrepareForAttack(float duration)
        {
            //Play sound effect
            yield return new WaitForSeconds(duration);
            _gun.InvokeOnFireStarted();
        }
    }
}
