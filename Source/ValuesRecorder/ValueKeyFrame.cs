namespace GUtils.Source.ValuesRecorder
{
    public readonly struct ValueKeyFrame<T>
    {
        public readonly float Time;
        public readonly T Value;

        public ValueKeyFrame(float time, T value)
        {
            Time = time;
            Value = value;
        }
    }
}