using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{
    public class Parallel : ControlNode
    {

        public override NodeStatus Process()
        {
            if (SubNodes != null)
            {
                for (int i = 0; i < SubNodes.Length; i++)
                {
                    SubNodes[i].Execute();
                }
            }

            return NodeStatus.Processing;
        }
    }
}
