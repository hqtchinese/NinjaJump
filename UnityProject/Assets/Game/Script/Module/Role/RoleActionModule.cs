using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleActionModule : Module<RoleController>
    {

        public RoleActionModule(RoleController dock) : base(dock)
        {
            CanMutiModule = false;
        }    
        
        

    }
}
