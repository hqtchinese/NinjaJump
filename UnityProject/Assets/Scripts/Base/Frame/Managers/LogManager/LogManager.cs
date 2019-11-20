using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Text;
using System.IO;
using System;

namespace GameBase
{
    //日志管理,可以注册日志输出时接受的方法,在系统日志实际输出之前执行,可以用作日志收集
    public class LogManager : ManagerBase<LogManager>
    {
        private string m_logSaveDir;
        private StringBuilder m_strBuilder;

        private readonly int MAX_LOG_LENGTH = 1048576;

        private void Awake() 
        {
            m_logSaveDir = Application.persistentDataPath + "/" +"Log";
            m_strBuilder = new StringBuilder();
            Debug.Log("日志保存路径:" + m_logSaveDir);
            Application.logMessageReceived += LogCallBack;
        }

        private void LogCallBack(string condition, string stackTrace, LogType type)
        {
            m_strBuilder.Append($"[{DateTime.Now.ToLocalTime()}]{type}:{condition}\n{stackTrace}");
            if (m_strBuilder.Length >= MAX_LOG_LENGTH)
            {
                StringBuilder temp = m_strBuilder;
                m_strBuilder = new StringBuilder();
                Thread th = new Thread(() => {SaveLog(temp);});
                th.Start();
            }
        }

        private void OnDestroy() 
        {
            Application.logMessageReceived -= LogCallBack;
            SaveLog(m_strBuilder);
        }

        private void SaveLog(StringBuilder sb)
        {
            DateTime nowTime = DateTime.Now;

            string dateDir = $"{m_logSaveDir}/{nowTime.ToString("yyyy-MM-dd")}";
            if (!Directory.Exists(dateDir))
            {
                Directory.CreateDirectory(dateDir);
            }

            string filePath = $"{dateDir}/{nowTime.ToString("HH-mm-ss")}.log";
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(sb.ToString());
            writer.Flush();
            writer.Close();
        }

    }

}
