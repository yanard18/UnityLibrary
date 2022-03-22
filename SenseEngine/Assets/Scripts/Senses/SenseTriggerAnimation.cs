using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;


namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
    [SenseEnginePath("Animations/Trigger Animation")]
    public class SenseTriggerAnimation : Sense
    {
        [SerializeField] private string _triggerName;
        [SerializeField] private Animator _animator;
        
        private void Awake()
        {
            Label = "Trigger Animation";
        }
        
            
        public override void Play()
        {  
            _animator.SetTrigger(_triggerName);
        }
    }
}
