using GUtils.Locations.Enums;

namespace GUtils.Locations.Extensions;

public static class HorizontalLocationExtensions
{
    public static HorizontalLocation Opposite(this HorizontalLocation horizontalLocation)
    {
        return horizontalLocation == HorizontalLocation.Left ? HorizontalLocation.Right : HorizontalLocation.Left;
    }
}