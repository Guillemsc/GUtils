namespace GUtils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="False"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class True
    {
        public static readonly True Instance = new ();

        True() { }
    }
}
