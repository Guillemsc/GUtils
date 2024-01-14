using System;
using System.Collections.Generic;
using GUtils.Predicates;

namespace GUtils.AnimationGraphs
{
    public sealed class AnimationGraphConnection
    {
        public AnimationGraphNode From { get; set; }
        public AnimationGraphNode To { get; set; }
        public List<IPredicate> Conditions { get; } = new();
        public List<Action> OnSetActions { get; } = new();
        public bool WaitForFullExecution { get; set; } = false;

        public AnimationGraphConnection(AnimationGraphNode from, AnimationGraphNode to)
        {
            From = from;
            To = to;
        }

        public void CopyConditions(AnimationGraphConnection animationGraphConnection)
        {
            Conditions.AddRange(animationGraphConnection.Conditions);
        }
        
        public void Set()
        {
            OnSetActions.ForEach(o => o.Invoke());
        }
    }
}
    
    