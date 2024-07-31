using System.Collections.Generic;
using System.Text;

namespace GUtils.Pooling
{
    public sealed class Pools
    {
        public static Pools Instance => _instance ??= new Pools();
        private static Pools _instance;

        public Pool<List<int>> IntsList { get; private set; }
        public Pool<StringBuilder> StringBuilders { get; private set; }
        
        Pools()
        {
            IntsList = new Pool<List<int>>(() => new List<int>());
            IntsList.GetAction += o => o.Clear();
            
            StringBuilders = new Pool<StringBuilder>(() => new StringBuilder());
            StringBuilders.GetAction += o => o.Clear();
        }
    }
}