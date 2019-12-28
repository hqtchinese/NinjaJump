using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class CollisionHandler2D : MonoBehaviour
    {
        
        public event Action<Collision2D> CollisionEnterEvent;
        public event Action<Collision2D> CollisionStayEvent;
        public event Action<Collision2D> CollisionExitEvent;


        private void OnCollisionEnter2D(Collision2D other) 
        {
            CollisionEnterEvent?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other) 
        {
            CollisionStayEvent?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            CollisionExitEvent?.Invoke(other);
        }

    }

}
