using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public abstract class ManagerBase : MonoBehaviour
    {

    }
    public class ManagerBase<T> : ManagerBase where T : ManagerBase<T>
    {
        private static T m_instance;
        public static T Instance => m_instance == null ? m_instance = GameMain.Instance.AddManager<T>() : m_instance;


    }

}
