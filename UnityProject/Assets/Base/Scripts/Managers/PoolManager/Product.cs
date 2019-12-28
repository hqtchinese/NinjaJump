using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Pool
{

    public class Production
    {
        public Product Product { get; set; }
        public GameObject Obj { get; set; }
        public float BornTime { get; set; }
        public float LifeTime { get; set; }
        public Production(Product _product)
        {
            Product = _product;
        }
        
    }
    
    public class Product : IComparer<Production>
    {

        public int Max { get; set; }

        private GameObject prefab;
        private SortedSet<Production> ActivatedSet { get; set; }
        private SortedSet<Production> RecycledSet { get; set; }


        public Product(GameObject _prefab)
        {
            prefab = _prefab;
            Max = 0;
            ActivatedSet = new SortedSet<Production>(this);
            RecycledSet = new SortedSet<Production>(this);
        }

        public int Compare(Production production1, Production production2)
        {
            float diff = (production1.BornTime + production1.LifeTime) - (production2.BornTime + production2.LifeTime);
            if (diff > 0)
                return 1;
            else 
                return -1;
        }


        public Production Spawn(Transform parent, float lifeTime)
        {
            //如果设置了最大值,并且存活对象达到了最大限制,则先回收
            if (Max > 0 && ActivatedSet.Count >= Max)
                Recycle();

            if (RecycledSet.Count > 0)
                return Reuse(lifeTime);
            else
                return CreateNewInstance(parent, lifeTime);
        }

        public void Despawn(Production production, bool isDestroy)
        {
            ActivatedSet.Remove(production);
            if (isDestroy)
            {
                GameObject.Destroy(production.Obj);
            }
            else
            {
                production.Obj.SetActive(false);
                RecycledSet.Add(production);
            }
        }

        public void Update()
        {
            Production production = ActivatedSet.Min;
            while (production != null )
            {
                if (production.BornTime + production.LifeTime > GameMain.Instance.GameTime)
                {
                    break;
                }
                
                Despawn(production, false);
                production = ActivatedSet.Min;
            }
        }

        public void Destroy()
        {
            foreach (var production in ActivatedSet)
            {
                GameObject.Destroy(production.Obj);
            }

            foreach (var production in RecycledSet)
            {
                GameObject.Destroy(production.Obj);
            }

            ActivatedSet = null;
            RecycledSet = null;
        }


        private bool Recycle()
        {
            if (ActivatedSet.Count > 0)
            {
                
            }
            return true;
        }

        private void Recycle(GameObject obj)
        {

        }

        private Production Reuse(float lifeTime)
        {
            Production production = RecycledSet.Min;
            //必须先执行移除再赋值,因为移除的时候会对比Hash值,先赋值的话Hash会变
            RecycledSet.Remove(production);

            production.BornTime = GameMain.Instance.GameTime;
            production.LifeTime = lifeTime;

            production.Obj.SetActive(true);
            ActivatedSet.Add(production);

            return production;
        }

        private Production CreateNewInstance(Transform parent, float lifeTime)
        {
            GameObject obj = GameObject.Instantiate(prefab,parent);
            Production production = new Production(this)
            {
                Obj = obj,
                BornTime = GameMain.Instance.GameTime,
                LifeTime = lifeTime,
            };

            obj.SetActive(true);
            ActivatedSet.Add(production);
            return production;
        }

    }

}
