namespace GUtils.Source.ValuesRecorder
{
    public readonly struct SeekResult<T>
    {
        public readonly bool IsValid;
        public readonly T? ValueLeft;
        public readonly T? ValueRight;
        public readonly float NormalizedProgress;
        public readonly bool IsEnd;
        
        public SeekResult(bool isValid, T? valueLeft, T? valueRight, float normalizedProgress, bool isEnd)
        {
            ValueLeft = valueLeft;
            ValueRight = valueRight;
            NormalizedProgress = normalizedProgress;
            IsValid = isValid;
            IsEnd = isEnd;
        }

        public static SeekResult<T> FromInvalid()
        {
            return new SeekResult<T>(false, default, default, 0f, false);
        }
    }
}