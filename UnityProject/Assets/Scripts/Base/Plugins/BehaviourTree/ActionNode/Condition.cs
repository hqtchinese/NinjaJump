using System;

namespace GameBase.Tool.Behaviour
{
    public class Condition : TreeNode
    {
        private Func<bool> m_checkFunc;        
        public Condition(Func<bool> checkFunc)
        {
            if (checkFunc == null)
                throw new Exception("检测条件的方法不能为空");
            m_checkFunc = checkFunc;
        }

        public override NodeStatus Begin()
        {
            if (m_checkFunc.Invoke())
                return NodeStatus.Complete;
            else
                return NodeStatus.Break;
        }
    }
    
}
