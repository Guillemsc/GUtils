using System;

namespace GUtils.Sequencing.Instructions
{
    public sealed class ActionInstruction : InstantInstruction
    {
        readonly Action _action;

        public ActionInstruction(Action action)
        {
            _action = action;
        }

        protected override void OnInstantExecute()
        {
            _action?.Invoke();
        }
    }
}
