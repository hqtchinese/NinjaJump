using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Editor;

namespace NinjaJump
{
    public enum UpdateType
    {
        Update,
        LateUpdate
    }
    // [ExecuteInEditMode]
    public class PositionFixer : MonoBehaviour
    {

        [Range(0,1)]
        public float Factor;
        public bool Lerp;
        public float LerpRate;
        public UpdateType UpdateType;

        // [HideInInspector]
        public Vector2 Position;
        private Vector2 ViewPos => GameController.Instance.ViewPos;

        public void Update()
        {   
            if(UpdateType != UpdateType.Update)
                return;

            FixPos();
        }


        public void LateUpdate()
        {
            if(UpdateType != UpdateType.LateUpdate)
                return;
                
            FixPos();
        }

        public void FixPos()
        {
            if (Lerp)
                transform.position = Vector3.Lerp(transform.position,(Position - ViewPos) * Factor,LerpRate);
            else
                transform.position = (Position - ViewPos) * Factor;
        }
    }

}
