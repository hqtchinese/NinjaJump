using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BlockRayCast : Graphic
    {
        protected override void Awake()
        {
            base.Awake();
            raycastTarget = true;
        }
    }

}
