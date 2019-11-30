using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleActionModule : Module<RoleDock>
    {

        public RoleActionModule(RoleDock dock) : base(dock)
        {
        }    
        
        public void Jump(Vector2 direction)
        {
            Debug.Log("jump");
            if (m_dock.Status != RoleStatus.Aim)
                return;
        }

    }
}
