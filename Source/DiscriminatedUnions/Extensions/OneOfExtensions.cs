namespace GUtils.DiscriminatedUnions
{
    public static class OneOfExtensions
    {
        public static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";
    }
}
