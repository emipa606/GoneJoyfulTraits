using System.Collections.Generic;

namespace EBTools;

public static class EBExtensions
{
    public static List<T> OrEmptyIfNull<T>(this List<T> source)
    {
        return source ?? [];
    }

    public static int AsInt(this bool source)
    {
        return source ? 1 : 0;
    }
}