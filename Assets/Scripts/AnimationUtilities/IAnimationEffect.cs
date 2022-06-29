using System.Collections;

namespace AnimationUtilities
{
    public interface IAnimationEffect
    {
        public IEnumerator Execute();
    }
}
