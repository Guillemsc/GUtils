using System;
using System.Collections.Generic;
using GUtils.Optionals;
using GUtils.Predicates.Extensions;

namespace GUtils.AnimationGraphs
{
    public sealed class AnimationGraphNode
    {
        public AnimationGraphBehaviour Behaviour { get; }

        public List<AnimationGraphConnection> OutConnections { get; } = new();
        public List<AnimationGraphConnection> InConnections { get; } = new();

        public List<Action> OnEnterActions { get; } = new();
        public List<Action> OnExitActions { get; } = new();

        public AnimationGraphNode(AnimationGraphBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        public Optional<AnimationGraphConnection> ConnectTo(AnimationGraphNode to)
        {
            if (to == this)
            {
                return Optional<AnimationGraphConnection>.None;
            }

            Optional<AnimationGraphConnection> optionalConnection = GetConnectionTo(to);

            if (optionalConnection.HasValue)
            {
                return Optional<AnimationGraphConnection>.None;
            }

            AnimationGraphConnection connection = new(this, to);

            OutConnections.Add(connection);
            to.InConnections.Add(connection);

            return connection;
        }

        public AnimationGraphConnection ConnectToUnsafe(AnimationGraphNode to)
        {
            return ConnectTo(to).UnsafeGet();
        }

        Optional<AnimationGraphConnection> GetConnectionTo(AnimationGraphNode to)
        {
            foreach (AnimationGraphConnection connection in OutConnections)
            {
                if (connection.To == to)
                {
                    return connection;
                }
            }

            return Optional<AnimationGraphConnection>.None;
        }

        public List<AnimationGraphConnection> GetAvaliableOutConnections()
        {
            List<AnimationGraphConnection> ret = new();

            foreach (AnimationGraphConnection connection in OutConnections)
            {
                bool avaliable = connection.Conditions.AreAllPredicatesSatisfied();

                if (avaliable)
                {
                    ret.Add(connection);
                }
            }

            return ret;
        }

        public void Enter()
        {
            OnEnterActions.ForEach(o => o.Invoke());
            Behaviour.Completed = false;
            Behaviour.Enter();
        }

        public void Exit()
        {
            OnExitActions.ForEach(o => o.Invoke());
            Behaviour.Exit();
        }
    }
}