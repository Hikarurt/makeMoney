using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Windows.Controls;
using System.Windows.Forms;

namespace zj
{
    public class ClassHirachy
    {
        private TreeNode m_node;
        private int m_hirachy;

        public ClassHirachy()
        {
            m_node = new TreeNode();
            m_hirachy = 0;
        }
        public void SetNode(TreeNode srcNode)
        {
            m_node = srcNode;
        }
        public TreeNode GetNode()
        {
            return m_node;
        }

        
        public void SetHirachy(int hirachy)
        {
            m_hirachy = hirachy;
        }
        public int GetHirachy()
        {
            return m_hirachy;
        }
 
    }
}
