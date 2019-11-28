using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [Resource("Prefab/Role/Ninja")]
    public class RoleController : Dock
    {
        public RoleStatus Status { get; protected set; }
        
        protected override void Init()
        {
            AddModule<RoleInputeModule>();
            AddModule<RoleActionModule>();
        }

    }
}
