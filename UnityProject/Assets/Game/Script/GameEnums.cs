using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    
    public enum SceneInitState
    {
        InitScene,
        InitRole,
    }

    public enum GameEvent
    {
        OnGameStart,
        OnGamePause,
        OnGameResume,
        OnGameOver,


        OnJumpOff,
        
    }

    public enum GameState
    {
        Ready,
        Gaming,
        Pause,
        GameOver,
    }

    public enum RoleStatus
    {
        None,
        Stand,
        Jump,
        Move,
        Attack,
        Land,
        Hold,
        Aim,
        Dying
    }

    public enum FaceDir
    {
        Left,
        Right
    }
}
