using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTLib.Nodes;

namespace XPTLib
{
    public sealed class Graph
    {
        GraphManager gm;
        List<BaseNode> nodes = new List<BaseNode>();

        public Graph(GraphManager gm)
        {
            this.gm = gm;
            this.gm.AddGraph(this);
        }

        public void AddNode(BaseNode node)
        {
            if (!this.nodes.Contains(node))
            {
                this.nodes.Add(node);
                node.Graph = this;
            }
        }

        public void RemoveNode(BaseNode node)
        {
            if (this.nodes.Contains(node))
            {
                this.nodes.Remove(node);
            }
        }

        public GraphManager Manager { get { return this.gm; } }
    }
}
