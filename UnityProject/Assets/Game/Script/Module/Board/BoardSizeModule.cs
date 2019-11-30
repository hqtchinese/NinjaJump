using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class BoardSizeModule : Module<BoardDock>
    {
        public BoardSizeModule(BoardDock dock) : base(dock)
        {
            
        }

        public override void Init()
        {
            m_dock.Board.size = new Vector2(m_dock.Board.size.x,m_dock.Length);
            float halfLength = m_dock.Board.size.y / 2;
            m_dock.TopSide.transform.localPosition = new Vector2(0,halfLength);
            m_dock.BottomSide.transform.localPosition = new Vector2(0,-halfLength);
        }

    }

}
