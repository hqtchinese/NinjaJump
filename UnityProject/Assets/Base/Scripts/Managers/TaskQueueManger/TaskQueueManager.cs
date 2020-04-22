using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameBase.Util;

namespace GameBase
{
    [DefaultExecutionOrder(1000)]
    public class TaskQueueManager : ManagerBase<TaskQueueManager>
    {
        protected Dictionary<int,Queue<TaskMember>> m_queueDict;
        protected ObjPool<TaskMember> m_pool;
        protected void Awake()
        {
            m_queueDict = new Dictionary<int, Queue<TaskMember>>();
            m_pool = new ObjPool<TaskMember>();
        }

        public void Enqueue(string queueName,IEnumerator enumerator)
        {
            Enqueue(queueName.GetHashCode(),enumerator);
        }

        public void Enqueue(int queueNum,IEnumerator enumerator)
        {
            TaskMember task = m_pool.Request();
            task.enumerator = enumerator;
            if(!m_queueDict.TryGetValue(queueNum, out Queue<TaskMember> taskQueue))
            {
                taskQueue = new Queue<TaskMember>();
                m_queueDict.Add(queueNum,taskQueue);
            }
            taskQueue.Enqueue(task);
        }
        
        protected void Update()
        {
            foreach (var item in m_queueDict.Values)
            {
                if(item.Count > 0)
                {
                    TaskMember firstTask = item.Peek();
                    if(!firstTask.enumerator.MoveNext())
                    {
                        item.Dequeue();
                        m_pool.GiveBack(firstTask);
                    }
                }
            }
        }
    }

    public class TaskMember
    {
        public IEnumerator enumerator;

    }    

}
