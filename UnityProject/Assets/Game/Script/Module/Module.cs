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
        //是否允许一个Dock上有多个该类型模组,在子类中重写
        public virtual bool CanMutiModule => false;
        //依赖的模组类型,在构造器中初始化,没有可为空
        public Type[] Dependence { get; protected set; }

        public Module()
        {
            Enable = true;
        }

        public virtual void Init()
        {

        }

        public virtual void Awake()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }
        
        public virtual void Destroy()
        {

        }

    }

}
