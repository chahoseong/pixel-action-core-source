using System.Collections.Generic;
using System.Linq;
using StateMachine.Actions;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "State Machine/State", fileName = "New State")]
    public class State : ScriptableObject
    {
        [SerializeField] private List<Action> actions;
        [SerializeField] private List<Transition> transitions;

        public virtual void Enter(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Ready(controller.Context);
            }
        }


        public virtual void Exit(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Finish(controller.Context);
            }
        }

        public virtual void Tick(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Execute(controller.Context);
            }

            CheckTransitions(controller);
        }

        private void CheckTransitions(StateController controller)
        {
            var transition = transitions.FirstOrDefault(transition =>
                transition.Condition.Evaluate(controller.Context));
            if (transition.IsValid)
            {
                controller.ChangeState(transition.NextState);
            }
        }
    }
}
