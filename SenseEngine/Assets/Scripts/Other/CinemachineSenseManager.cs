using Cinemachine;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CinemachineSenseManager : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;
        
        [SerializeField] private CinemachineImpulseSenseEvent _eventChannel;

        private void Awake() => _impulseSource = GetComponent<CinemachineImpulseSource>();
        
        private void OnEnable() => _eventChannel.ImpulseEvent += Shake;

        private void OnDisable() => _eventChannel.ImpulseEvent -= Shake;

        private void Shake(CinemachineImpulseDefinition impulseDefinition)
        {
            if(Camera.main is null) return;
            _impulseSource.m_ImpulseDefinition = impulseDefinition;
            _impulseSource.GenerateImpulse(Camera.main.transform.forward);
        }
    }
}
