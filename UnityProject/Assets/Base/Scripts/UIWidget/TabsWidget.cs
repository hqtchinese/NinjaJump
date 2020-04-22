using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.UI;
using UnityEngine.UI;

namespace  TrailerBattle3
{
    public class TabsWidget : UIElement
    {
        public GameObject[] contents;
        public Button[] tabsBtn;

        protected override void Awake()
        {
            base.Awake();
            if(contents.Length < tabsBtn.Length)
                Debug.LogWarning("Contents didn't match the tabBtns");

            for (int i = 0; i < tabsBtn.Length; i++)
            {
                GameObject content = contents[i];
                tabsBtn[i].onClick.AddListener(() => {
                    SwitchContent(content);
                });
            }
        }

        public void SwitchContent(GameObject content)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].SetActive(contents[i] == content);
            }
        }

    }

}
