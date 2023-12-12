using System;

namespace GUtils.Di.Container
{
    /// <summary>
    /// Contains a mapping of how registered objects need to be created, including their dependencies.
    /// </summary>
    public interface IDiContainer : IDiResolveContainer, IDisposable
    {

    }
}
