namespace GUtils.AnimationGraphs;

public sealed class NopAnimationGraphPlayer : IAnimationGraphPlayer
{
    public static readonly NopAnimationGraphPlayer Instance = new();
    
    public void Tick() { }
}