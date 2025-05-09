using System.Collections.Generic;
using UnityEngine;

namespace GUtils.Source.Cameras.Stacks
{
    public sealed class CameraStack
    {
        public Camera? Current;
        
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

            Current = camera;
        }

        public void Pop()
        {
            Current = null;
            
            if (_cameras.Count > 0)
            {
                Camera current = _cameras[^1];
                _cameras.Remove(current);
                current.gameObject.SetActive(false);
            }
            
            if (_cameras.Count > 0)
            {
                Current = _cameras[^1];
                Current.gameObject.SetActive(true);
            }
        }
    }
}