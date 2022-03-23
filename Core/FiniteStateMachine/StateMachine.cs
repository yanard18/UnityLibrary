using System;
using System.Collections.Generic;
using System.Linq;

namespace DenizYanar.FSM
{
    public class StateMachine
    {
        private State _currentState;

        private readonly List<Transition> _anyTransitions = new List<Transition>();

        

        public void Tick()
        {
            var transition = GetTriggeredTransition();
            if(transition != null)
                SetState(transition.To);
            
            _currentState.Tick();
        }
        
        public void PhysicsTick() => _currentState?.PhysicsTick();


        public void AddTransition(State from, State to, Func<bool> condition)
        {
            from.Transitions.Add(new Transition(to, condition));
        }
        

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }

        private void SetState(State state)
        {
            if (state == _currentState)
                return;

            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }

        public void InitState(State state)
        {
            _currentState = state;
            _currentState.OnEnter();
        }

        public bool TriggerState(State state)
        {
            foreach (var unused in _currentState.Transitions.Where(transition => transition.To == state))
            {
                SetState(state);
                return true;
            }

            return false;
        }

        private Transition GetTriggeredTransition()
        {
            //Any transitions has priority
            foreach (var anyTransition in _anyTransitions.Where(anyTransition => anyTransition.Condition()))
                return anyTransition;

            
            foreach (var transition in _currentState.Transitions)
                if(transition.Condition())
                    return transition;

            return null;
        }

    }
}
