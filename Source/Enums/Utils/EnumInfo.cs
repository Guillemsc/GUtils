using System;

namespace GUtils.Enums.Utils
{
    /// <summary>
    /// Base class used for retreiving basic information from an Enum.
    /// To use it, just inherit from this class with the wanted Enum type. Then you
    /// just need to call the static methods from the inherited class.
    /// Values are generated once on the first method call, and then they get cached.
    /// </summary>
    public abstract class EnumInfo<T> where T : Enum
    {
        static T[]? _values;
        
        public static T[] Values => _values ??= (T[])Enum.GetValues(typeof(T));
    }
}
