using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    [RequireModule(typeof(RoleActionModule))]
    public class RoleControlModule : Module<RoleDock>
    {
        private Transform Arrow => m_dock.Arrow;
        private RoleActionModule m_actionModule;
        private float m_baseHeight;
        private float m_baseAngle;
        private float m_factor = 0.5f;

        public RoleControlModule(RoleDock dock) : base(dock)
        {
        }
        
        public override void Init()
        {
            m_dock.Status = RoleStatus.Hold;
            m_dock.Dir = FaceDir.Right;
        }

        public override void Awake()
        {
            GameController.Instance.GameMode.TouchBoard.ClickEvent += DoClick;
            GameController.Instance.GameMode.TouchBoard.DragEvent += DoDrag;
            GameController.Instance.GameMode.TouchBoard.BeginDragEvent += DoBeginDrag;
            GameController.Instance.GameMode.TouchBoard.EndDragEvent += DoEndDrag;
            m_actionModule = m_dock.GetModule<RoleActionModule>();
        }

        public void DoClick()
        {
        }

        public void DoDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Aim)
                return;

            float angleChange = m_dock.Dir == FaceDir.Right ? m_baseHeight - pos.y : pos.y - m_baseHeight;

            angleChange *= m_factor;

            float angle = Mathf.Clamp(Arrow.rotation.eulerAngles.z + angleChange,m_baseAngle - 80, m_baseAngle + 80);

            Arrow.gameObject.SetActive(true);
            Arrow.rotation = Quaternion.Euler(0,0,angle);

            m_baseHeight = pos.y;
        }

        public void DoBeginDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Hold)
                return;
            
            m_actionModule.SetAiming();

            m_baseHeight = pos.y;
            m_baseAngle = m_dock.Dir == FaceDir.Right ? 90 : 270;
            Arrow.rotation = Quaternion.Euler(0,0,m_baseAngle);
        }

        public void DoEndDrag(Vector2 pos)
        {
            if (m_dock.Status != RoleStatus.Aim)
                return;
            
            m_actionModule.Jump();
            Arrow.gameObject.SetActive(false);
        }

        public override void Update()
        {

        }

        public override void Destroy()
        {
            GameController.Instance.GameMode.TouchBoard.ClickEvent -= DoClick;
            GameController.Instance.GameMode.TouchBoard.DragEvent -= DoDrag;
            GameController.Instance.GameMode.TouchBoard.BeginDragEvent -= DoBeginDrag;
            GameController.Instance.GameMode.TouchBoard.EndDragEvent -= DoEndDrag;
        }

    }
}
