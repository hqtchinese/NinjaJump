using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class BoardDock : Dock
    {
        public SpriteRenderer TopSide,Board,BottomSide;
        public float Length;

        protected override void Init()
        {
            AddModule<BoardSizeModule>();
        }
    }

}
