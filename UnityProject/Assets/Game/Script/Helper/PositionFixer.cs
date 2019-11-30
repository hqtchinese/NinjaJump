using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Editor;

namespace NinjaJump
{
    // [ExecuteInEditMode]
    public class PositionFixer : MonoBehaviour
    {

        [Range(0,1)]
        public float Factor;
        public bool VerticleLoop;
        public Vector2 Position;
        private Vector2 ViewPos => GameController.Instance.ViewPos;
        private LinkedList<SpriteRenderer> m_spriteList;
        private float m_tempHeight;

        public void Awake()
        {
            if (VerticleLoop)
                AddChildren();
        }

        
        public void Update()
        {
            transform.position = (Position - ViewPos) * Factor;

            if (VerticleLoop)
                CheckChildren();
        }


        private void AddChildren()
        {
            m_spriteList = new LinkedList<SpriteRenderer>();
            List<SpriteRenderer> tempList = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++)
            {
                SpriteRenderer sprite = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (sprite)
                    tempList.Add(sprite);

            }

            tempList.Sort((a,b) => {
                    float result = a.transform.localPosition.y - b.transform.localPosition.y;
                    if (result > 0)
                        return 1;
                    else if (result == 0)
                        return 0;
                    else
                        return -1;
                });

            foreach (var item in tempList)
            {
                m_spriteList.AddLast(item);
            }
        }

        private void CheckChildren()
        {
            if (m_spriteList.Count == 0)
                return;

            if (m_tempHeight < ViewPos.y)
            {
                SpriteRenderer lowestSprite = m_spriteList.First.Value;
                if (lowestSprite.transform.position.y <  ScreenSpaceHelper.BottomY - lowestSprite.size.y / 2)
                {
                    BottomToTop();
                }
            }
            else if (m_tempHeight > ViewPos.y)
            {
                SpriteRenderer lowestSprite = m_spriteList.Last.Value;
                if (lowestSprite.transform.position.y >  ScreenSpaceHelper.TopY + lowestSprite.size.y / 2)
                {
                    TopToBottom();
                }
            }
            m_tempHeight = ViewPos.y;
        }
        
        private void BottomToTop()
        {
            SpriteRenderer bottom = m_spriteList.First.Value;
            SpriteRenderer top = m_spriteList.Last.Value;
            float addHeight = (bottom.size.y + top.size.y) / 2f;
            bottom.transform.localPosition = new Vector2(
                bottom.transform.localPosition.x,
                top.transform.localPosition.y + addHeight
            );

            var firstNode = m_spriteList.First;
            m_spriteList.RemoveFirst();
            m_spriteList.AddLast(firstNode);
        }

        private void TopToBottom()
        {
            SpriteRenderer bottom = m_spriteList.First.Value;
            SpriteRenderer top = m_spriteList.Last.Value;
            float minusHeight = (bottom.size.y + top.size.y) / 2f;
            top.transform.localPosition = new Vector2(
                top.transform.localPosition.x,
                bottom.transform.localPosition.y - minusHeight
            );

            var lastNode = m_spriteList.Last;
            m_spriteList.RemoveLast();
            m_spriteList.AddFirst(lastNode);
        }

    }

}
