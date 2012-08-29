using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XPTLib.Nodes;

namespace XPT.Gui.GraphEditor
{
    /// <summary>
    /// Node preview to appear in the graph container.
    /// </summary>
    class GraphNode : GuiRenderable
    {
        BaseNode node;
        TitledPanel container;
        string[] nodeOutputs, nodeInputs;
        public GraphNode(Vector2 position, BaseNode node)
        {
            this.node = node;
            // get all the inputs and outputs for display later.
            this.nodeOutputs = this.node.GetOuputNames();
            this.nodeInputs = this.node.GetInputNames();
            this.container = new TitledPanel(node.GetType().Name, new Rectangle((int)position.X, (int)position.Y, 100, 100));
            this.container.Color = Color.Red;
            this.AddChild(container);
        }

        public BaseNode Node { get { return this.node; } }
    }
}
