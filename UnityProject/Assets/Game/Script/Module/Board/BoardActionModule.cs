using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class BoardActionModule : Module<BoardDock>
    {
        public BoardActionModule(BoardDock dock) : base(dock){}
        public override void Init()
        {
            m_dock.CollisionHandler.CollisionEnterEvent += OnCollisionEnter;
        }

        private void OnCollisionEnter(Collision2D collision)
        {
        }
    }

}
