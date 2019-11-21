using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{
    public class BehaviourTree : MonoBehaviour
    {
        public ControlNode BaseControlNode { get; set; }

        public void Prepare()
        {
            BaseControlNode?.Prepare();
        }

        protected void Update()
        {
            switch (BaseControlNode.Status)
            {
                case NodeStatus.Ready :
                case NodeStatus.Processing :
                case NodeStatus.Wait :
                {
                    BaseControlNode.Execute();
                    break;
                }
                case NodeStatus.Complete:
                case NodeStatus.Break:
                {
                    BaseControlNode.Prepare();
                    break;
                }
            }
        }
    }
    
}
