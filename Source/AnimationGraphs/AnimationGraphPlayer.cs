using System.Collections.Generic;
using GUtils.Tick.Tickables;

namespace GUtils.AnimationGraphs;

public sealed class AnimationGraphPlayer : IAnimationGraphPlayer
{
    AnimationGraphNode _currentNode;
    bool _firstTick = true;

    public AnimationGraphPlayer(AnimationGraphNode startingNode)
    {
        _currentNode = startingNode;
    }
    
    public void Tick()
    {
        if (_firstTick)
        {
            _firstTick = false;
            _currentNode.Enter();
        }
        
        List<AnimationGraphConnection> connections = _currentNode.GetAvaliableOutConnections();

        AnimationGraphNode _previousNode = _currentNode;
        
        foreach (AnimationGraphConnection connection in connections)
        {
            if (connection.WaitForFullExecution)
            {
                if (_currentNode.Behaviour.Completed)
                {
                    connection.Set();
                    _currentNode = connection.To;
                }
            }
            else
            {
                connection.Set();
                _currentNode = connection.To;
            }
        }

        if (_previousNode != _currentNode)
        {
            _previousNode.Exit();
            _currentNode.Enter();
        }
        
        _currentNode.Behaviour.Tick();
    }
}