using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [Resource("")]
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
