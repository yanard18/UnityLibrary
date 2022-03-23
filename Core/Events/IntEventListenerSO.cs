using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class IntEventListenerSO : MonoBehaviour
    {
        [SerializeField]
        private IntEventChannelSO _eventChannel;

        [SerializeField]
        private UnityEvent<int> _unityEvent;

        private void Awake() => _eventChannel.Register(this);

        private void OnDisable() => _eventChannel.Deregister(this);

        public void RaiseEvent(int value) => _unityEvent.Invoke(value);
    }
}
