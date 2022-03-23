using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have float arguments. {Example: Set player health game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Float Event Channel")]
    public class FloatEventChannelSO : ScriptableObject
    {
        private readonly HashSet<FloatEventListenerSO> _listeners = new HashSet<FloatEventListenerSO>();

        public void Invoke(float value)
        {
            foreach (FloatEventListenerSO listener in _listeners)
                listener.RaiseEvent(value);
        }

        public void Register(FloatEventListenerSO listener) => _listeners.Add(listener);

        public void Deregister(FloatEventListenerSO listener) => _listeners.Remove(listener);
    }
}
