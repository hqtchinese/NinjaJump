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
            Resize();
        }

        public void Resize()
        {
            Vector2 boardSize = new Vector2(m_dock.Board.size.x,m_dock.Length);
            m_dock.Board.size = boardSize;
            float halfLength = m_dock.Board.size.y / 2;
            m_dock.TopSide.transform.localPosition = new Vector2(0,halfLength);
            m_dock.BottomSide.transform.localPosition = new Vector2(0,-halfLength);
            m_dock.Collider.size = boardSize / 2;
        }

    }

}
