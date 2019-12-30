using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class EventManager : ManagerBase<EventManager>
    {
        public delegate void EventDelegate (object[] param);
        private Dictionary<Type,Dictionary<object,List<InvokeInfo>>> m_eventDict;
        private void Awake() 
        {
            m_eventDict = new Dictionary<Type, Dictionary<object, List<InvokeInfo>>>();
        }

        public void Broadcast(string eventName, params object[] param)
        {
            Broadcast(typeof(string), eventName, param);
        }

        public void Broadcast(Enum _enum, params object[] param)
        {
            Broadcast(_enum.GetType(), _enum, param);
        }

        public void Register(Component comp) 
        {
            Type type = comp.GetType();
            MethodInfo[] methods = type.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                Attribute[] attrs = Attribute.GetCustomAttributes(methods[i]);
                for (int j = 0; j < attrs.Length; j++)
                {
                    if(attrs[j] is RegistEventAttribute)
                    {
                        try
                        {
                            RegistEventAttribute attr = attrs[j] as RegistEventAttribute;
                            InvokeInfo invokeInfo = new InvokeInfo();
                            invokeInfo.method = methods[i];
                            invokeInfo.target = comp;
                            Register(attr.eventType.GetType(), attr.eventType, invokeInfo);
                            Debug.Log($"方法注册成功:{type.Name}.{methods[i].Name}");
                        }
                        catch (System.Exception)
                        {
                            Debug.LogError($"方法注册失败:{type.Name}.{methods[i].Name}");
                        }
                    }
                }
            }
        }

        public void Register(object eventName, EventDelegate registEvent)
        {
            Register(eventName.GetType(),eventName, new InvokeInfo(registEvent));
        }

        public void Unregister(Component comp)
        {
            Type type = comp.GetType();
            MethodInfo[] methods = type.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                Attribute[] attrs = Attribute.GetCustomAttributes(methods[i]);
                for (int j = 0; j < attrs.Length; j++)
                {
                    if(attrs[j] is RegistEventAttribute)
                    {
                        RegistEventAttribute attr = attrs[j] as RegistEventAttribute;
                        InvokeInfo invokeInfo = new InvokeInfo();
                        invokeInfo.method = methods[i];
                        invokeInfo.target = comp;
                        Unregister(attr.eventType.GetType(), attr.eventType, invokeInfo);
                    }
                }
            }
        }

        public void Unregister(object eventName, EventDelegate unregistEvent)
        {
            Unregister(eventName.GetType(), eventName, new InvokeInfo(unregistEvent));
        }

        private void Unregister(Type type, object eventType, InvokeInfo invokeInfo)
        {
            if (m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(eventType, out List<InvokeInfo> invokeList))
                {
                    for (int i = 0; i < invokeList.Count; i++)
                    {
                        if (invokeList[i].target == invokeInfo.target
                            && invokeList[i].method == invokeInfo.method)
                        {
                            invokeList.RemoveAt(i);
                            break;
                        }
                    }
                    if (invokeList.Count == 0)
                        eventDict.Remove(eventType);

                    if (m_eventDict.Count == 0)
                        m_eventDict.Remove(type);
                }
            }
        }

        private void Broadcast(Type type, object eventType,params object[] param)
        {
            if (m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(eventType, out var invokeList))
                {
                    for (int i = 0; i < invokeList.Count; i++)
                    {
                        InvokeInfo invokeInfo = invokeList[i];
                        invokeInfo.method.Invoke(invokeInfo.target,new object[]{param});
                    }
                }
            }
        }

        private void Register(Type type, object eventType, InvokeInfo invokeInfo)
        {
            if(m_eventDict.TryGetValue(type, out var eventDict))
            {
                if (eventDict.TryGetValue(eventType,out var invokeList))
                {
                    invokeList.Add(invokeInfo);
                }
                else
                {
                    invokeList = new List<InvokeInfo>();
                    invokeList.Add(invokeInfo);
                    eventDict.Add(eventType,invokeList);
                }
            }
            else
            {
                List<InvokeInfo> invokeList = new List<InvokeInfo>();
                invokeList.Add(invokeInfo);
                m_eventDict.Add(type,new Dictionary<object, List<InvokeInfo>>());
                m_eventDict[type].Add(eventType,invokeList);
            }
        }

    }

    public class InvokeInfo
    {
        public object target;
        public MethodInfo method;
        public InvokeInfo()
        {

        }
        public InvokeInfo(EventManager.EventDelegate eventDelegate)
        {
            target = eventDelegate.Target;
            method = eventDelegate.Method;
        }
    }
}

