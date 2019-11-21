using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase
{
    public class EventManager : ManagerBase<EventManager>
    {
        public delegate void EventDelegate (params object[] param);
        private Dictionary<Type,Dictionary<object,EventDelegate>> m_eventDict;

        private void Awake() 
        {
            m_eventDict = new Dictionary<Type, Dictionary<object, EventDelegate>>();
        }

        public void Broadcast(string eventName, params object[] param)
        {
            Broadcast(typeof(string), eventName, param);
        }

        public void Broadcast(Enum _enum, params object[] param)
        {
            Broadcast(_enum.GetType(), _enum, param);
        }

        public void Register(string eventName, EventDelegate registEvent)
        {
            Register(typeof(string),eventName,registEvent);
        }

        public void Register(Enum _enum, EventDelegate registEvent)
        {
            Register(_enum.GetType(), _enum, registEvent);
        }

        public void Unregister(Enum _enum, EventDelegate unregistEvent)
        {
            Unregister(_enum.GetType(), _enum, unregistEvent);
        }

        public void Unregister(string eventName, EventDelegate unregistEvent)
        {
            Unregister(typeof(string), eventName, unregistEvent);
        }

        private void Unregister(Type type, object eventType, EventDelegate unregistEvent)
        {
            if (m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(eventType, out EventDelegate eventDelegate))
                {
                    if ((eventDelegate -= unregistEvent) == null)
                    {
                        eventDict.Remove(eventType);
                    }
                }
            }
        }

        private void Broadcast(Type type, object eventType,params object[] param)
        {
            if (m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(eventType, out EventDelegate eventDelegate))
                {
                    Delegate[] delegates = eventDelegate.GetInvocationList();
                    for (int i = 0; i < delegates.Length; i++)
                    {
                        Delegate curDelegate = delegates[i];
                        if (curDelegate.Target as UnityEngine.Object != null)
                        {
                            curDelegate.Method.Invoke(curDelegate.Target,new object[]{param});
                        }
                        else
                        {
                            if ((eventDelegate -= (curDelegate as EventDelegate)) == null)
                            {
                                eventDict.Remove(eventType);
                            }
                        }
                    }
                }
            }
        }

        private void Register(Type type, object obj, EventDelegate registEvent)
        {
            if (!(registEvent.Target is UnityEngine.Object))
            {
                Debug.LogWarning("注册的委托必须是UnityEngine.Object子类对象下的方法,且不允许使用匿名函数");
                return;
            }

            if (m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(obj, out EventDelegate eventDelegate))
                    eventDelegate += registEvent;
                else
                    eventDict.Add(obj,registEvent);
            }
            else
            {
                Dictionary<object, EventDelegate> dict = new Dictionary<object, EventDelegate>();
                dict.Add(obj,registEvent);
                m_eventDict.Add(type,dict);
            }
        }

    }

}

