using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Behaviour
{
    public abstract class ControlNode : TreeNode
    {
        public TreeNode[] SubNodes { get; set; }

        protected TreeNode CurrentSubNode => SubNodes[m_curNodeIndex];
        protected int m_curNodeIndex;

        public override void Prepare()
        {
            base.Prepare();
            if (SubNodes != null)
            {
                for (int i = 0; i < SubNodes.Length; i++)
                {
                    SubNodes[i].Prepare();
                }
            }
            m_curNodeIndex = 0;
        }
        
        public override NodeStatus Begin()
        {
            if (SubNodes == null || SubNodes.Length == 0)
                return NodeStatus.Complete;
             
            return NodeStatus.Processing;
        }
        
        protected bool Next()
        {
            return ++m_curNodeIndex < SubNodes.Length;
        }
    }
    
}
