using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameBase
{
    public class TimerManager : ManagerBase<TimerManager>
    {
        private TimerTaskQueue m_taskQueue;
        private float m_timer;
        private float m_firstTaskTime = float.MaxValue;

        private void Awake()
        {
            m_taskQueue = new TimerTaskQueue();
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if (m_firstTaskTime > m_timer)
                return;

            m_firstTaskTime = ProcessTask();
        }

        /// <summary>
        /// 定时重复执行
        /// </summary>
        /// <param name="time">间隔时间</param>
        /// <param name="action">执行的action</param>
        /// <param name="immediate" default="false">是否立刻执行一次</param>
        /// <returns>返回当前的TimerTask,如果需要停止任务,则直接将该任务对象Enable属性设置为false,下次执行时会跳过并将其移出队列</returns>
        public TimerTask DoRepeat(float time, Action action, bool immediate = false)
        {
            TimerTask task = new TimerTask()
            {
                Repeat = true,
                Enable = true,
                RepeatTime = time,
                NextTime = m_timer + time,
                Task = action,
            };

            if (immediate)
                action.Invoke();

            AddTask(task);
            return task;
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <param name="action"></param>
        /// <returns>返回当前的TimerTask,如果需要停止任务,则直接将该任务对象Enable属性设置为false,下次执行时会跳过并将其移出队列</returns>
        public TimerTask DoDelay(float time, Action action)
        {
            TimerTask task = new TimerTask()
            {
                Repeat = false,
                Enable = true,
                RepeatTime = time,
                NextTime = m_timer + time,
                Task = action
            };
            AddTask(task);
            return task;
        }


        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="task"></param>
        private void AddTask(TimerTask task)
        {
            m_taskQueue.Enqueue(task);
            m_firstTaskTime = m_taskQueue.Peek().NextTime;
        }

        /// <summary>
        /// 从队首开始处理所有到时间的任务
        /// </summary>
        /// <returns>下次任务执行时间</returns>
        private float ProcessTask()
        {
            int safeCount = 0;
            while (true)
            {
                TimerTask firstTask = m_taskQueue.Peek();
                
                //为空时队列里面没有任务
                if (firstTask == null)
                    return m_firstTaskTime = float.MaxValue;

                //不可用的任务移出队列
                if (!firstTask.Enable)
                {
                    m_taskQueue.Dequeue();
                    continue;
                }
                
                if (firstTask.NextTime > m_timer)
                    return m_firstTaskTime = firstTask.NextTime;


                m_taskQueue.Dequeue();
                firstTask.Task.Invoke();
                if (firstTask.Repeat)
                {
                    firstTask.NextTime += firstTask.RepeatTime;
                    m_taskQueue.Enqueue(firstTask);
                }

                if (safeCount++ > BaseConst.TIMER_MAX_TASK_PER_FRAME)
                {
                    Debug.LogWarning($"该帧执行超过{BaseConst.TIMER_MAX_TASK_PER_FRAME}个任务,剩下至下一帧执行");
                    return m_firstTaskTime;
                }
            }
        }
    }

}
