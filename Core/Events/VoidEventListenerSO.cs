using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class VoidEventListenerSO : MonoBehaviour
    {
        [SerializeField]
        private VoidEventChannelSO _eventChannel;

        [SerializeField]
        private UnityEvent _unityEvent;

        private void Awake() => _eventChannel.Register(this);

        private void OnDisable() => _eventChannel.Deregister(this);

        public void RaiseEvent() => _unityEvent.Invoke();
    }
}
