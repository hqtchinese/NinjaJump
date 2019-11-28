using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class Dock : MonoBehaviour
    {
        protected List<Module> ModuleList { get; set; }

        protected virtual void Awake()
        {
            ModuleList = new List<Module>();
            Init();
        }

        protected virtual void Init()
        {

        }

        protected virtual void OnEnable()
        {
            foreach (var module in ModuleList)
            {
                module.OnEnable();
            }
        }

        protected virtual void Update()
        {
            foreach (var module in ModuleList)
            {
                if (module.Enable)
                    module.Update();
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var module in ModuleList)
            {
                module.OnDisable();
            }
        }

        public Module AddModule(Module module,bool createDependence = false)
        {
            if (module == null)
                return null;
            
            if (module.CanMutiModule)
            {
                if (ModuleList.IndexOf(module) < 0)
                    ModuleList.Add(module);
            }
            else
            {
                foreach (var item in ModuleList)
                {
                    if (item == module || item.GetType() == module.GetType())
                        return null;
                }
                ModuleList.Add(module);
            }
            
            if (createDependence)
            {
                if (module.Dependence != null)
                {
                    foreach (var item in module.Dependence)
                    {
                        AddModule(Activator.CreateInstance(item,new object[]{this}) as Module);
                    }
                }
            }

            module.Init();
            return module;
        }

        public T AddModule<T>(bool createDependence = false) where T : Module
        {
            T module = Activator.CreateInstance(typeof(T),new object[]{this}) as T;
            return AddModule(module,createDependence) as T;
        }

        public void RemoveModule(Module module)
        {
            ModuleList.Remove(module);
            module.Distroy();
        }

        public T GetModule<T>(bool createIfNull = false) where T : Module
        {
            foreach (var module in ModuleList)
            {
                if (module is T)
                    return module as T;            
            }

            if (createIfNull)
                return AddModule<T>();
            
            return null;
        }

        public List<T> GetModules<T>() where T : Module
        {
            List<T> list = new List<T>();
            foreach (var module in ModuleList)
            {
                if (module is T)
                    list.Add(module as T);
            }
            return list;
        }

        public void Ondestroy()
        {
            foreach (var module in ModuleList)
            {
                module.Distroy();
            }

            ModuleList.Clear();
        }
    }

}
