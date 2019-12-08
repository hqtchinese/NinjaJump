using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public abstract class GameModeBase
    {
        public TouchEventHelper TouchBoard { get; set; }

        public abstract void Init();

        public abstract void Start();

        public virtual void GameUpdate(float deltaTime)
        {
            
        }

        public abstract void EndGame();

    }

}
