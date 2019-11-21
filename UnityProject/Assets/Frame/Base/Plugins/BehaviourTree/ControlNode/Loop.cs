using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{
    public class Loop : ControlNode
    {
        public int LoopTimes { get; set; }
        private int m_loopCount;

        public override NodeStatus Begin()
        {
            m_loopCount = 0;
            return base.Begin();
        }

        public override NodeStatus Process()
        {
            if (m_loopCount >= LoopTimes)
                return NodeStatus.Complete;

            NodeStatus status = CurrentSubNode.Execute();
            switch (status)
            {
                case NodeStatus.Complete :
                case NodeStatus.Break :
                {
                    if (Next())
                        return NodeStatus.Processing;
                    else
                    {
                        m_loopCount++;
                        ResetLoop();
                        return NodeStatus.Processing;
                    }
                }
                case NodeStatus.Processing :
                case NodeStatus.Wait :
                {
                    return NodeStatus.Processing;
                }
                default :
                {
                    return NodeStatus.Complete;
                }
            }
        }

        private void ResetLoop()
        {
            m_curNodeIndex = 0;
            for (int i = 0; i < SubNodes.Length; i++)
            {
                SubNodes[i].Prepare();
            }
        }
    }
    
}
