using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class SimpleScroll : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public GameObject item;
        public float spaceX;
        public float spaceY;

        private Action<GameObject,int> m_initAction;
        private List<GameObject> m_itemList;
        public void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            if(!scrollRect)
                Debug.LogError("Can't find component <ScrollRect>");

            m_itemList = new List<GameObject>();
            scrollRect.onValueChanged.AddListener(OnVlaueChange);
        }

        public void SetInitAction(Action<GameObject,int> action)
        {
            m_initAction = action;
        }

        public void CreateItems(int count, Action<GameObject,int> initFunc = null)
        {
            if(!item)
                return;
            
            if(scrollRect.horizontal)
            {
                RectTransform trans = item.transform as RectTransform;
            }
        }

        [ContextMenu("Resize")]
        public void Resize()
        {
            RectTransform trans = item.transform as RectTransform;
            if(scrollRect.horizontal)
            {
                float itemWidth = trans.offsetMax.x - trans.offsetMin.x;
                float width = 5 * (itemWidth + spaceX) + spaceX;
                RectTransform contentTrans = scrollRect.content;
                contentTrans.offsetMin = new Vector2(-width,contentTrans.offsetMin.y);
                contentTrans.offsetMax = new Vector2(width,contentTrans.offsetMin.y);
            }
        }

        public void RemoveItem(int index)
        {
            
        }

        public void AddItem(int index, Action<GameObject> initFunc = null)
        {

        }

        public void OnVlaueChange(Vector2 pos)
        {
            
        }
    }

}
