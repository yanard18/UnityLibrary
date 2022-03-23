using System;
using UnityEngine;
using DenizYanar.FSM;


namespace DenizYanar.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        #region Private Variables

        private StateMachine _stateMachine;

        private PlayerAttackSlashState _slash;
        private PlayerAttackSwordThrowState _throw;
        private PlayerAttackWaitSwordState _wait;
        private PlayerAttackIdleState _idle;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject _katanaGameObject;
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private PlayerInputs _inputs;

        #endregion
        
        #region Public Variables

        public bool IsSwordTurnedBack { get; set; }

        #endregion


        #region Monobehaviour

        private void OnEnable()
        {
            _inputs.OnAttack1Started += OnAttack1Started;
            _inputs.OnAttack2Started += OnAttack2Started;
        }

        private void OnDisable()
        {
            _inputs.OnAttack1Started -= OnAttack1Started;
            _inputs.OnAttack2Started -= OnAttack2Started;
        }

        private void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new PlayerAttackIdleState();
            _slash = new PlayerAttackSlashState(this, _katanaGameObject, _inputs);
            _throw = new PlayerAttackSwordThrowState(ThrowKatana, transform, _settings, _inputs);
            _wait = new PlayerAttackWaitSwordState(this);

            _stateMachine.InitState(_idle);
            
            To(_idle,_slash,() => false);
            To(_idle, _throw, () => false);
            To(_throw, _wait, () => false);
            To(_wait, _idle, WhenSwordReturned());
            To(_slash, _idle, IsSwordSlashFinished());
            
            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            
            Func<bool> IsSwordSlashFinished() => () => _slash.IsFinished;
            Func<bool> WhenSwordReturned() => () => IsSwordTurnedBack;
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        #endregion

        #region Inputs

        private void OnAttack1Started()
        {
            _stateMachine.TriggerState(_slash);
        }

        private void OnAttack2Started()
        {
            if(_stateMachine.TriggerState(_wait))
                return;

            _stateMachine.TriggerState(_throw);
        }

        #endregion
        
        #region Method Referances
        
        private KatanaProjectile ThrowKatana(Vector2 dir, float throwSpeed, float angularVelocity)
        {
            var p = Instantiate(_settings.SwordProjectile, transform.position, Quaternion.identity);
            p.Init(dir.normalized * throwSpeed, angularVelocity: angularVelocity, author: gameObject, lifeTime: 0f);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            p.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return p.GetComponent<KatanaProjectile>();
        }

        #endregion
        
        

    }
}
