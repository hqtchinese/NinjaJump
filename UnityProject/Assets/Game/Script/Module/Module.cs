using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public abstract class Module<T> : Module where T : Dock
    {
        protected T m_dock { get; set; }
        public Module(T _dock) : base()
        {
            m_dock = _dock;
        }
    }
    public abstract class Module
    {
        public bool Enable { get; set; }

        public Module()
        {
            Enable = true;
        }

        public virtual void Init(){}
        public virtual void Awake(){}
        public virtual void Start(){}
        public virtual void Update(){}
        public virtual void LateUpdate(){}
        public virtual void OnEnable(){}
        public virtual void OnDisable(){}
        public virtual void Destroy(){}

    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    public class RequireModuleAttribute : Attribute
    {
        public Type type;
        public RequireModuleAttribute(Type _type)
        {
            type = _type;
        }
    }
}
