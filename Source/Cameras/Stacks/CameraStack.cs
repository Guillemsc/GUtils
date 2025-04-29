using System.Collections.Generic;
using UnityEngine;

namespace GUtils.Source.Cameras.Stacks
{
    public sealed class CameraStack
    {
        readonly List<Camera> _cameras = new();
        
        public void Push(Camera camera)
        {
            if (_cameras.Count > 0)
            {
                Camera current = _cameras[^1];
                current.gameObject.SetActive(false);
            }
            
            camera.gameObject.SetActive(true);
            _cameras.Add(camera);
        }

        public void Pop()
        {
            if (_cameras.Count > 0)
            {
                Camera current = _cameras[^1];
                _cameras.Remove(current);
                current.gameObject.SetActive(false);
            }
            
            if (_cameras.Count > 0)
            {
                Camera current = _cameras[^1];
                current.gameObject.SetActive(true);
            }
        }
    }
}