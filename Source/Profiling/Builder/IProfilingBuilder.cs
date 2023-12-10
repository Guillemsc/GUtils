using GUtils.Profiling.Results;

namespace GUtils.Profiling.Builder
{
    public interface IProfilingBuilder
    {
        void Next(string name);
        void Complete();

        ProfilingResult Build();
    }
}
