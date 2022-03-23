using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have bool arguments. {Example: Set player blink availability game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Bool Event Channel")]
    public class BoolEventChannelSO : ScriptableObject
    {
        private readonly HashSet<BoolEventListenerSO> _listeners = new HashSet<BoolEventListenerSO>();

        public void Invoke(bool value)
        {
            foreach (BoolEventListenerSO listener in _listeners)
                listener.RaiseEvent(value);
        }

        public void Register(BoolEventListenerSO listener) => _listeners.Add(listener);

        public void Deregister(BoolEventListenerSO listener) => _listeners.Remove(listener);
    }
}
