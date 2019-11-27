using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    
    public enum GameEvent
    {
        OnGameStart,
        OnGamePause,
        OnGameResume,
        OnGameOver
    }

    public enum GameState
    {
        Ready,
        Gaming,
        Pause,
        GameOver,
    }
}
