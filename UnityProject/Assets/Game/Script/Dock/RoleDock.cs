using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [Resource("Prefab/Role/Ninja")]
    public class RoleDock : Dock
    {
        public Transform Arrow;

        public RoleStatus Status { get; set; }
        public FaceDir Dir { get; set; }

        protected override void Init()
        {
            AddModule<RoleControlModule>();
        }

    }
}
