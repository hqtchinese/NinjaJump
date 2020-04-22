
using UnityEngine;

namespace GameBase.Util
{
    public class RandomUtil 
    {
        public static Vector2 RandomV2(float xRange,float yRange)
        {
            xRange = Mathf.Abs(xRange);
            yRange = Mathf.Abs(yRange);
            float x =Random.Range(-xRange,xRange);
            float y =Random.Range(-yRange,yRange);
            return new Vector2(x,y);
        }
    }

}
