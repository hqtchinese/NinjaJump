using System;
using UnityEngine;

namespace GameBase.Util
{
    public class TimeUtil
    {
        
        public static int GetDay()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
            return ts.Days;
        }


        public static int GetSecond()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
            return ts.Seconds;
        }

        



    }

}
