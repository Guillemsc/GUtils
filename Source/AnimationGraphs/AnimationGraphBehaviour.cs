namespace GUtils.AnimationGraphs
{
    public abstract class AnimationGraphBehaviour
    {
        public bool Completed { get; set; }
    
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }   
}