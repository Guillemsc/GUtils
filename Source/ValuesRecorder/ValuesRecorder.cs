using System.Collections.Generic;
using GUtils.Extensions;
using UnityEngine;

namespace GUtils.Source.ValuesRecorder
{
    public sealed class ValuesRecorder<T>
    {
        readonly List<ValueKeyFrame<T>> _valueKeyFrames = new();
        
        public void Record(float time, T value)
        {
            _valueKeyFrames.Add(new ValueKeyFrame<T>(time, value));
        }

        public SeekResult<T> Seek(float time)
        {
            if (_valueKeyFrames.Count < 2)
            {
                return SeekResult<T>.FromInvalid();
            }
            
            int index = _valueKeyFrames.FindLastIndex(f => f.Time <= time);

            if (index < 0)
            {
                index = 0;
            }
            
            int nextIndex = index + 1;
            
            if (_valueKeyFrames.IsOutsideBounds(nextIndex))
            {
                nextIndex = index;
            }
            
            ValueKeyFrame<T> f0 = _valueKeyFrames[index];
            ValueKeyFrame<T> f1 = _valueKeyFrames[nextIndex];
            
            float normalizedTime = Mathf.InverseLerp(f0.Time, f1.Time, time);

            bool isEnd = nextIndex == _valueKeyFrames.Count - 1 && index == nextIndex;
            
            return new SeekResult<T>(true, f0.Value, f1.Value, normalizedTime, isEnd);
        }
    }
}