using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class ScreenSpaceHelper
    {
        public static float TopY { get; private set; }
        public static float BottomY { get; private set; }
        public static float LeftX { get; private set; }
        public static float RightX { get; private set; }

        public static float ScreenWidth { get; private set; }
        public static float ScreenHeight { get; private set; }

        static ScreenSpaceHelper()
        {
            CheckScreenSpace();
        }

        public static void CheckScreenSpace()
        {
            Vector3 delta = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
            TopY = Mathf.Abs(delta.y);
            BottomY = -Mathf.Abs(delta.y);
            LeftX = -Mathf.Abs(delta.x);
            RightX = Mathf.Abs(delta.x);
            ScreenWidth = Mathf.Abs(delta.x * 2);
            ScreenHeight = Mathf.Abs(delta.y * 2);
        }
    }

}
