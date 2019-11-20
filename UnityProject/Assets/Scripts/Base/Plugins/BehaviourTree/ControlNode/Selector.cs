using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{
    public class Selector : ControlNode
    {

        public override NodeStatus Process()
        {
            
            NodeStatus status = CurrentSubNode.Execute();
            switch (status)
            {
                case NodeStatus.Complete :
                {
                    return NodeStatus.Complete;
                }
                case NodeStatus.Processing :
                case NodeStatus.Wait :
                {
                    return NodeStatus.Processing;
                }
                case NodeStatus.Break :
                {
                    if (Next())
                        return NodeStatus.Processing;
                    else
                        return NodeStatus.Break;
                }
                default :
                {
                    return NodeStatus.Complete;
                }
            }
            
        }


    }
    
}
