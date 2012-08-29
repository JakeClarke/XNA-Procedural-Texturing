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

        Vector2 posOffset = new Vector2(10f, 35f);
        static readonly Vector2 perNodeOffset = new Vector2(110f, 0f);
        public void AddNode(BaseNode node)
        {
            GraphNode guiNode = new GraphNode(posOffset, node);
            posOffset += perNodeOffset; // hack.
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

            organiseNodes();
        }

        /// <summary>
        /// Organises and sets the positions for the nodes.
        /// </summary>
        private void organiseNodes()
        {
            List<BaseNode> onodes = new List<BaseNode>();
            List<BaseNode> nodesToSort = new List<BaseNode>(graph.Nodes);
            for (int i = nodesToSort.Count - 1; i > -1; i--)
            {
                if (nodesToSort[i] is Output)
                {
                    onodes.Add(nodesToSort[i]);
                    nodesToSort.RemoveAt(i);
                }
            }
            List<List<BaseNode>> tree = new List<List<BaseNode>>();
            tree.Add(onodes);

            bool nextLayerReg = true;
            while (nextLayerReg)
            {
                List<BaseNode> current = tree[tree.Count - 1];
                List<BaseNode> next = new List<BaseNode>();
                tree.Add(next);
                nextLayerReg = false;
                foreach (var item in current)
                {
                    // reserved so that we can easily can easily use the same node pile as we are using.
                    for (int i = 0; i < item.GetInputCount(); i++)
                    {
                        // check that these are actually pointing to nodes incase someone has done something wierd & that they are linking to a node on this graph
                        if (item.GetInput(i) != null && 
                            item.GetInput(i).Target is BaseNode && 
                            ((BaseNode)item.GetInput(i).Target).Graph == this.graph) 
                        {
                            BaseNode target = (BaseNode)item.GetInput(i).Target;
                            if (nodesToSort.Contains(target))
                            {
                                next.Add(target);
                                nodesToSort.Remove(target);
                                this.graphMap[target].Linked = true;
                            }
                        }

                        // set to true because we have added something to the next layer this round.
                        nextLayerReg = true; 
                    }
                }
            }

            if (nodesToSort.Count > 0)
            {
                // gotta put whats left somewhere.
                tree[tree.Count - 1] = nodesToSort;
                foreach (var leaf in nodesToSort)
                {
                    this.graphMap[leaf].Linked = false;
                }
            }
            else
            {
                tree.RemoveAt(tree.Count - 1);
            }

            
            // time to set their positions.
            float start_y = 40f;
            Vector2 b_offset = new Vector2(this.Bounds.Width / tree.Count, 135f), pos = new Vector2(5f, 0f);
            
            // reversed so that outputs are on the right.
            for (int i = tree.Count - 1; i > -1; i--)
            {
                List<BaseNode> branch = tree[i];

                pos.Y = start_y;
                foreach (BaseNode leaf in branch)
                {
                    // get our gui preview.
                    this.graphMap[leaf].Position = pos;
                    pos.Y += b_offset.Y;
                }

                pos.X += b_offset.X;
            }

        }
    }
}
