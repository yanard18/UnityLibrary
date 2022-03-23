using System.Collections;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar
{
    
    [RequireComponent(typeof(CircleCollider2D), typeof(PointEffector2D))]
    public class MagnetController : MonoBehaviour
    {
        private Coroutine _impulseCoroutine;
        
        private CircleCollider2D _col;
        private PointEffector2D _effector;

        private MagnetConfigurations _conf;

        private bool _hasUsageCooldown;

        [SerializeField] private SenseEnginePlayer _magnetActivateSense;
        [SerializeField] private SenseEnginePlayer _magnetDisableSense;
        [SerializeField] private SenseEnginePlayer _magnetImpulseSense;

        private void Awake()
        {
            _col = GetComponent<CircleCollider2D>();
            _effector = GetComponent<PointEffector2D>();
            ActivateMagnet(false);

            _conf = new MagnetConfigurations(EMagnetPolar.PULL, _effector.forceMagnitude, _col.radius);
        }
        
        private void SetMagnet(MagnetConfigurations conf)
        { 
            _effector.forceMagnitude = conf.Polar == EMagnetPolar.PULL ? -Mathf.Abs(conf.Power) : conf.Power;
            _col.radius = conf.Radius;
            _conf = conf;
        }

        public void ImpulseMagnet(EMagnetPolar polar, float impulsePower, float impulseDecay, float usageCooldown = 1.0f)
        {
            if(_impulseCoroutine is { }) return;
            _impulseCoroutine = StartCoroutine(ImpulseMagnetEnumerator(polar, impulsePower, impulseDecay, usageCooldown));
        }

        public void ActivateMagnet(bool value)
        {
            if(value && _hasUsageCooldown) return;
            
            
            _effector.enabled = value;
            PlaySenseEffects();
            
            
            
            void PlaySenseEffects() {
                if (value)
                {
                    if(_magnetActivateSense != null) 
                        _magnetActivateSense.Play();
                }
                else
                {
                    if(_magnetDisableSense != null) 
                        _magnetDisableSense.Play();
                }
            }
        }

        private IEnumerator ImpulseMagnetEnumerator(EMagnetPolar polar, float impulsePower, float impulseDecay, float usageCooldown)
        {
            var conf = _conf;
            SetMagnet(new MagnetConfigurations(polar, impulsePower, 15f));
            ActivateMagnet(true);
            StartCoroutine(StartUsageCooldown(usageCooldown));
            PlayEffect(_magnetImpulseSense);
            yield return new WaitForSeconds(impulseDecay);
            SetMagnet(conf);
            ActivateMagnet(false);
            
            _impulseCoroutine = null;
        }

        private IEnumerator StartUsageCooldown(float cooldownDuration)
        {
            _hasUsageCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            _hasUsageCooldown = false;
        }

        private void PlayEffect(SenseEnginePlayer player)
        {
            if(player != null)
                player.Play();
        }
        
        
    }
}
