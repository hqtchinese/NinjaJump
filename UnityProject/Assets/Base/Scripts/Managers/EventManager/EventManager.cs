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
                        if (curDelegate.Target as System.Object != null)
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
            //这里主要是为了方便解绑,所以不允许绑定匿名函数,没找到什么有效的判断方式,
            //暂时使用方法定义类型的定义类型来判断
            if (registEvent.Method.DeclaringType.DeclaringType != null)
            {
                Debug.LogError("不允许使用匿名方法或者嵌套类中的方法");
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

