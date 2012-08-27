﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTLib.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace XPTLib
{
    public sealed class Graph
    {
        Game game;
        List<BaseNode> nodes = new List<BaseNode>();
        List<Output> outputs = new List<Output>(); // for quick differentiation.

        public Graph(Game game)
        {
            this.game = game;
            
        }

        public void AddNode(BaseNode node)
        {
            if (!this.nodes.Contains(node))
            {
                this.nodes.Add(node);
                node.Graph = this;

                if (node is Output)
                {
                    this.outputs.Add((Output)node);
                }
            }
        }

        public void RemoveNode(BaseNode node)
        {
            if (this.nodes.Contains(node))
            {
                this.nodes.Remove(node);

                if (node is Output)
                {
                    this.outputs.Remove((Output)node);
                }
            }
        }

        /// <summary>
        /// Gets a output texture.
        /// </summary>
        /// <param name="index">Index of the output node.</param>
        /// <returns>Texture generated by the specified output node.</returns>
        public Texture2D GetOutputTexture(int index)
        {
            return this.outputs[index].GetResult();
        }

        /// <summary>
        /// Gets a output texture.
        /// </summary>
        /// <param name="name">Name of the output to get.</param>
        /// <returns>Texture generated by the specified output node.</returns>
        public Texture2D GetOutputTexture(string name)
        {
            foreach (Output item in this.outputs)
            {
                if (item.Name == name)
                {
                    return item.GetResult();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a node
        /// </summary>
        /// <param name="index">Index of the node to be retrieved.</param>
        /// <returns>The specified node.</returns>
        public BaseNode GetNode(int index)
        {
            return this.nodes[index];
        }

        /// <summary>
        /// Number of output nodes within this graph.
        /// </summary>
        public int OutputNodeCount { get { return this.outputs.Count; } }

        /// <summary>
        /// Number of nodes within this graph.
        /// </summary>
        public int NodeCount { get { return this.nodes.Count; } }

        /// <summary>
        /// Array of nodes within this graph.
        /// </summary>
        public BaseNode[] Nodes { get { return this.nodes.ToArray(); } }

        /// <summary>
        /// Gets or sets the content manager for this graph.
        /// </summary>
        public ContentManager Content { get; set; }

        /// <summary>
        /// Gets the game context for this graph.
        /// </summary>
        public Game Game { get { return this.game; } }
    }
}
