using System;
using System.Collections;
using System.Collections.Generic;
using GameBase.Data;

namespace GameBase
{
    public class TimerTask : IComparable
    {
        public Action Task { get; set; }
        public bool Repeat { get; set; }
        public float RepeatTime { get; set; }
        public float NextTime { get; set; }
        public bool Enable { get; set; }
        
        public int CompareTo(object obj)
        {
            float val = NextTime - (obj as TimerTask).NextTime;
            if (val > 0)
            {
                return 1;
            }
            else if (val == 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }

    public class TimerTaskQueue 
    {

        private BinaryHeap<TimerTask> queue = new BinaryHeap<TimerTask>();

        public void Enqueue(TimerTask task)
        {
            queue.Add(task);
        }

        public TimerTask Dequeue()
        {
           return queue.Pop();
        }

        public TimerTask Peek()
        {
            return queue.Peek();
        }

    }

}
