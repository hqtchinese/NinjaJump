using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using Spine.Unity;

namespace NinjaJump
{
    [Resource("Prefab/Role/Ninja")]
    public class RoleDock : Dock
    {
        public Transform Arrow;
        public Transform ArrowCenter;
        [SpineAnimation]
        public string MoveAnimeName;
        [SpineAnimation]
        public string LandAnimeName;
        [SpineAnimation]
        public string HangAnimeName;
        public SkeletonAnimation Anime;
        public PositionFixer PositionFixer;

        public RoleStatus Status { get; set; }
        public FaceDir Dir { get; set; }

        protected override void Init()
        {

            AddModule<RoleControlModule>();
        }
        
    }
}
