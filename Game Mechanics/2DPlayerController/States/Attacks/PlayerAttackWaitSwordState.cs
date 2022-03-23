using DenizYanar.FSM;

namespace DenizYanar.Player
{
    public class PlayerAttackWaitSwordState : State
    {
        private readonly PlayerAttackController _player;

        #region Constructor

        public PlayerAttackWaitSwordState(PlayerAttackController player)
        {
            _player = player;
        }

        #endregion

        #region State Callbacks
        
        public override void OnExit()
        {
            base.OnExit();
            _player.IsSwordTurnedBack = false;
        }
        
        #endregion
    }
}