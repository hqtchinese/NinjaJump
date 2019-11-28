using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleInputeModule : Module<RoleController>
    {
        
        public RoleInputeModule(RoleController dock) : base(dock)
        {
            CanMutiModule = false;
            Dependence = new System.Type[]{typeof(RoleActionModule)};
        }
        
        public override void Init()
        {
            GameController.Instance.TouchBoard.ClickEvent += DoClick;
            GameController.Instance.TouchBoard.DragEvent += DoDrag;
            GameController.Instance.TouchBoard.BeginDragEvent += DoBeginDrag;
        }

        public void DoClick()
        {
        }

        public void DoDrag(Vector2 pos)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            worldPos.z = 0;
            m_dock.transform.position = worldPos;
        }

        public void DoBeginDrag(Vector2 pos)
        {

        }

        public override void Distroy()
        {
            GameController.Instance.TouchBoard.ClickEvent -= DoClick;
            GameController.Instance.TouchBoard.DragEvent -= DoDrag;
            GameController.Instance.TouchBoard.BeginDragEvent -= DoBeginDrag;
        }

    }
}
