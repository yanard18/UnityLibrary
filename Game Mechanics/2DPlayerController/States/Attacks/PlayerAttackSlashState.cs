using System.Collections;
using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar.Player
{
    public class PlayerAttackSlashState : State
    {
        private readonly PlayerAttackController _player;
        private readonly GameObject _katana;
        private readonly PlayerInputs _inputs;
        
        private float _startAngle;
        private float _direction;

        public bool IsFinished = false;


        #region Constructor

        public PlayerAttackSlashState(PlayerAttackController player, GameObject katana, PlayerInputs inputs)
        {
            _player = player;
            _katana = katana;
            _inputs = inputs;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            IsFinished = false;
            _katana.SetActive(true);
            if (Camera.main is { })
            {
                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_player.transform.position);
                _direction = Mathf.Sign(dir.x);
                _startAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            }

            _startAngle += _direction > 0 ? 90 : -90;
            
            _katana.transform.rotation = Quaternion.AngleAxis(_startAngle, Vector3.forward);
            _katana.transform.localScale = _direction > 0 ? Vector3.one : new Vector3(1, -1, 1);

            _player.StartCoroutine(Slash(0.15f));
        }

        public override void OnExit()
        {
            base.OnExit();
            IsFinished = false;
            _katana.SetActive(false);
        }

        #endregion

        #region Local Methods

        private IEnumerator Slash(float slashDuration)
        {
            var elapsedTime = 0f;
            var targetAngle = _direction > 0 ? _startAngle - 179 : _startAngle + 179;
            var startRotation = _katana.transform.rotation;
            while (elapsedTime < slashDuration)
            {
                elapsedTime += Time.deltaTime;
                _katana.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(targetAngle, Vector3.forward), elapsedTime / slashDuration);
                yield return null;
            }

            IsFinished = true;
            
        }

        #endregion
        
    }
}