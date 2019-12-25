using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class Dock : MonoBehaviour
    {
        protected List<Module> ModuleList { get; set; }

        protected void Awake()
        {
            ModuleList = new List<Module>();
            Init();
        }

        protected void Start()
        {
            InitAllModule();
        }

        //初始化方法,添加模块写在这里
        protected virtual void Init()
        {

        }
        
        protected virtual void InitAllModule()
        {
            foreach (var module in ModuleList)
            {
                module.Init();
            }
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

        private Module AddModule(Module module,bool createDependence = true)
        {
            if (module == null)
                return null;
            
            ModuleList.Add(module);
            if (createDependence)
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(module.GetType(),typeof(RequireModuleAttribute));
                foreach (var attr in attributes)
                {
                    RequireModuleAttribute requireAttr = attr as RequireModuleAttribute;
                    if (requireAttr != null)
                        AddModule(requireAttr.type, createDependence);
                }
            }

            module.Awake();
            Debug.Log($"{gameObject.name}增加模块:{module.GetType().Name}");
            return module;
        }

        public T AddModule<T>(bool createDependence = true) where T : Module
        {
            return AddModule(typeof(T),createDependence) as T;
        }

        public Module AddModule(Type type, bool createDependence = true)
        {
            foreach (var item in ModuleList)
            {
                if (item.GetType() == type.GetType())
                {
                    Debug.LogWarning($"模块已存在:{item.GetType().Name}");
                    return null;
                }
            }
            return AddModule(Activator.CreateInstance(type,new object[]{this}) as Module,createDependence);
        }

        public void RemoveModule(Module module)
        {
            ModuleList.Remove(module);
            module.Destroy();
        }

        public T GetModule<T>(bool createIfNull = false) where T : Module
        {
            foreach (var module in ModuleList)
            {
                T retModule = module as T;
                if (retModule != null)
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

        public void OnDestroy()
        {
            foreach (var module in ModuleList)
            {
                module.Destroy();
            }

            ModuleList.Clear();
        }
    }

}
