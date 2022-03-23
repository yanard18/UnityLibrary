using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class BoolEventListenerSO : MonoBehaviour
    {
        [SerializeField]
        private BoolEventChannelSO _eventChannel;

        [SerializeField]
        private UnityEvent<bool> _unityEvent;

        private void Awake() => _eventChannel.Register(this);

        private void OnDisable() => _eventChannel.Deregister(this);

        public void RaiseEvent(bool value) => _unityEvent.Invoke(value);

    }
}
