using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XPTLib;
using XPTLib.Nodes;

namespace XPT.Gui.GraphEditor
{
    /// <summary>
    /// Class to store all the nodes for the graph that is being previewed.
    /// </summary>
    class GraphContainer : TitledPanel
    {
        Dictionary<BaseNode, GraphNode> graphMap = new Dictionary<BaseNode, GraphNode>();
        Graph graph;

        public GraphContainer(Rectangle bounds) : base("Graph preview", bounds)
        {

        }

        public void AddNode(BaseNode node)
        {
            GraphNode guiNode = new GraphNode(Vector2.Zero, node);
            this.graphMap.Add(node, guiNode);
            this.AddChild(guiNode);
        }

        public void RemoveNode(BaseNode node)
        {
            GraphNode guiNode = this.graphMap[node];
            this.RemovedChild(guiNode);
            this.graphMap.Remove(node);
        }

        public void LoadGraph(Graph graph)
        {
            // need to clear anything preview stuff that was there before.
            foreach (var item in this.graphMap)
            {
                this.RemovedChild(item.Value);
            }

            this.graphMap.Clear();

            this.graph = graph;
            foreach (var item in this.graph.Nodes)
            {
                this.AddNode(item);
            }
        }
    }
}
