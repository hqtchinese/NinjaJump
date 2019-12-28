using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [Resource("Prefab/Board/Board")]
    public class BoardDock : Dock
    {
        public SpriteRenderer TopSide,Board,BottomSide;
        public float Length;
        public BoxCollider2D Collider;
        public CollisionHandler2D CollisionHandler;

        protected override void Init()
        {
            AddModule<BoardSizeModule>();
            AddModule<BoardActionModule>();
        }

    }
}
