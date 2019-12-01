using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleControlModule : Module<RoleDock>
    {
        private Transform Arrow => m_dock.Arrow;
        private RoleActionModule m_actionModule;
        private float m_baseHeight;
        private float m_baseAngle;
        private float m_factor = 0.5f;

        public RoleControlModule(RoleDock dock) : base(dock)
        {
            Dependence = new System.Type[]{typeof(RoleActionModule)};
        }
        
        public override void Init()
        {
            m_dock.Status = RoleStatus.Hold;
            m_dock.Dir = FaceDir.Right;
        }

        public override void Awake()
        {
            GameController.Instance.TouchBoard.ClickEvent += DoClick;
            GameController.Instance.TouchBoard.DragEvent += DoDrag;
            GameController.Instance.TouchBoard.BeginDragEvent += DoBeginDrag;
            GameController.Instance.TouchBoard.EndDragEvent += DoEndDrag;
            m_actionModule = m_dock.GetModule<RoleActionModule>();
        }

        public void DoClick()
        {
            m_actionModule.Jump(Vector2.zero);
        }

        public void DoDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Aim)
                return;

            float angleChange = m_dock.Dir == FaceDir.Left ? m_baseHeight - pos.y : pos.y - m_baseHeight;

            angleChange *= m_factor;

            float angle = Mathf.Clamp(Arrow.rotation.eulerAngles.z + angleChange,m_baseAngle - 80, m_baseAngle + 80);

            Arrow.rotation = Quaternion.Euler(0,0,angle);

            m_baseHeight = pos.y;
        }

        public void DoBeginDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Hold)
                return;
            
            m_dock.Status = RoleStatus.Aim;

            m_baseHeight = pos.y;
            m_baseAngle = m_dock.Dir == FaceDir.Left ? 90 : 270;
            Arrow.rotation = Quaternion.Euler(0,0,m_baseAngle);
        }

        public void DoEndDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Aim)
                return;
            
            m_actionModule.Jump(pos);
        }

        public override void Destroy()
        {
            GameController.Instance.TouchBoard.ClickEvent -= DoClick;
            GameController.Instance.TouchBoard.DragEvent -= DoDrag;
            GameController.Instance.TouchBoard.BeginDragEvent -= DoBeginDrag;
            GameController.Instance.TouchBoard.EndDragEvent -= DoEndDrag;
        }

    }
}
