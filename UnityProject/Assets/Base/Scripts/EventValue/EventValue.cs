using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

namespace GameBase
{
    ///可以绑定事件的值,泛型最好只用值类型和string
    [Serializable]
    public class EventValue<T> : ISerializable
    {
        private T m_value;
        private event Action<T> OnValueChange;
        public T Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnValueChange?.Invoke(m_value);
            }
        }

        public EventValue()
        {
            
        }

        public EventValue(T _value)
        {
            m_value = _value;
        }

        public void Bind(Action<T> action,bool callNow = false)
        {
            OnValueChange += action;
            if(callNow)
                action(m_value);
        }

        public void Invoke()
        {
            OnValueChange?.Invoke(m_value);
        }

        public void UnBind(Action<T> action)
        {
            OnValueChange -= action;
        }

        public void UnBindAll()
        {
            OnValueChange = null;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

#region 序列化与反序列化
        public EventValue(SerializationInfo info, StreamingContext context)
        {
            m_value = (T)info.GetValue("Value",typeof(T));
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value",m_value);
        }
#endregion
    }
    
}
