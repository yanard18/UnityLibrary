using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class FloatEventListenerSO : MonoBehaviour
    {
        [SerializeField]
        private FloatEventChannelSO _eventChannel;

        [SerializeField]
        private UnityEvent<float> _unityEvent;

        private void Awake() => _eventChannel.Register(this);

        private void OnDisable() => _eventChannel.Deregister(this);

        public void RaiseEvent(float value) => _unityEvent.Invoke(value);

    }
}
