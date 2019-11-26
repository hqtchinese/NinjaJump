using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Editor;

namespace NinjaJump
{
    public class PositionFixer : MonoBehaviour
    {

        [Range(0,1)]
        public float Factor;

        public Vector2 Position;
        private Vector2 ViewPos => GameController.Instance.ViewPos;

        public void Update()
        {
            transform.position = (Position - ViewPos) * Factor;
        }

    }

}
