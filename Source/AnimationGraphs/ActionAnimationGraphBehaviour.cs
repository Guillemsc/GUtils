using System;

namespace GUtils.AnimationGraphs
{
    public sealed class ActionAnimationGraphBehaviour : AnimationGraphBehaviour
    {
        readonly Action _action;

        public ActionAnimationGraphBehaviour(Action action)
        {
            _action = action;
        }

        public override void Enter()
        {
            _action.Invoke();
            Completed = true;
        }

        public override void Tick()
        {

        }

        public override void Exit()
        {

        }
    }
}