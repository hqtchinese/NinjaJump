using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{

    public enum NodeStatus
    {
        None =          0x000,
        Ready =         0x001,
        Processing =    0x002,
        Wait =          0x004,
        Complete =      0x008,
        Break =         0x010
    }

    public abstract class TreeNode
    {
        public NodeStatus Status { get; protected set; }

        public virtual void Prepare()
        {
            Status = NodeStatus.Ready;
        }
        
        public virtual NodeStatus Begin(){return NodeStatus.Complete;}
        public virtual NodeStatus Wait(){return NodeStatus.Complete;}
        public virtual NodeStatus Process(){return NodeStatus.Complete;}
        public virtual NodeStatus Execute()
        {
            switch (Status)
            {
                case NodeStatus.Ready :
                {
                    return Status = Begin();
                }
                case NodeStatus.Processing :
                {
                    return Status = Process();
                }
                case NodeStatus.Wait :
                {
                    return Status = Wait();
                }
                default:
                {
                    return Status;
                }
            }
        }
    }
    
}
