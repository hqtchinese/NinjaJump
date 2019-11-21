using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase
{
    public class EventManager : ManagerBase<EventManager>
    {
        public delegate void EventDelegate (params object[] param);
        
        private Dictionary<string,EventDelegate> m_eventDict;

        private void Awake() 
        {
            m_eventDict = new Dictionary<string, EventDelegate>();
        }

        public void Broadcast(string eventName, params object[] param)
        {
            if (m_eventDict.TryGetValue(eventName, out EventDelegate eventDelegate))
            {
                Delegate[] delegates = eventDelegate.GetInvocationList();
                for (int i = 0; i < delegates.Length; i++)
                {
                    Delegate curDelegate = delegates[i];
                    if (curDelegate.Target as UnityEngine.Object != null)
                        curDelegate.Method.Invoke(curDelegate.Target,new object[]{param});
                    else
                        eventDelegate -= (curDelegate as EventDelegate);
                }
                
            }
        }

        public void Register(string eventName, EventDelegate registEvent)
        {
            if (registEvent.Target is UnityEngine.Object)
            {
                if (m_eventDict.TryGetValue(eventName, out EventDelegate eventDelegate))
                    m_eventDict[eventName] += registEvent;
                else
                    m_eventDict.Add(eventName,registEvent);
            }
            else
            {
                Debug.LogWarning("注册的委托必须是UnityEngine.Object子类对象下的方法,且不允许使用匿名函数");
            }
        }

        public void Register()
        {
            
        }

        public void Unregister(string eventName, EventDelegate unregistEvent)
        {
            if (m_eventDict.ContainsKey(eventName))
                m_eventDict[eventName] -= unregistEvent;
        }
    }

}

