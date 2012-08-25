using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XPTLib
{
    public sealed class GraphManager
    {
        Game game;

        List<Graph> graphs = new List<Graph>();

        public GraphicsDevice Graphics
        {
            get { return this.game.GraphicsDevice; }
        }

        public ContentManager Content { get; set; }

        public GraphManager(Game game)
        {
            this.game = game;
            this.Content = game.Content;
        }


        public void AddGraph(Graph graph)
        {
            if(!this.graphs.Contains(graph))
                this.graphs.Add(graph);
        }

        public void RemoveGraph(Graph graph)
        {
            if (this.graphs.Contains(graph))
                this.graphs.Remove(graph);
        }

    }
}
