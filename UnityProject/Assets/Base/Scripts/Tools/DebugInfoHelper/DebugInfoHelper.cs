using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool
{
    public class DebugInfoHelper : MonoBehaviour
    {
        private Rect logBtnPos = new Rect(0,0,100,50);
        private Rect logPos = new Rect(100,0,500,300);
        private Rect clearBtnPos = new Rect(0,50,100,50);
        private string logText = "";
        private bool isShowLog = false;
        private int maxTextLength = 50000;
        private int removeTextLength = 1000;
        private void Awake() 
        {
            Application.logMessageReceived += LogReceiver;
        }


        private void LogReceiver(string condition, string stack, LogType type)
        {
            if (logText.Length > maxTextLength)
            {
                logText.Remove(0,removeTextLength);
            }
            
            logText = $"{type}:{condition}\n{stack}\n" + logText;
        }


        private void OnGUI() 
        {
            if (isShowLog)
            {
                logText = GUI.TextArea(logPos,logText);
            }
            if (GUI.Button(logBtnPos,"Log"))
            {
                isShowLog = !isShowLog;
            }
            if (GUI.Button(clearBtnPos,"Clear"))
            {
                logText = "";
            }
        }


        private void OnDestroy() 
        {
            Application.logMessageReceived -= LogReceiver;    
        }
    }
    
}
