using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class StringEventListener : MonoBehaviour
    {
        [SerializeField] private StringEventChannelSO _eventChannel;

        [SerializeField] private UnityEvent<string> _unityEvent;
        
        private void Awake() => _eventChannel.Register(this);

        private void OnDisable() => _eventChannel.Deregister(this);

        public void RaiseEvent(string value) => _unityEvent.Invoke(value);
    }
}