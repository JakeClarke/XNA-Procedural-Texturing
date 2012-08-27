using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPTLib.Nodes
{
    public class Output : BaseNode
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Output(int height, int width)
        {
            this.registerInput("In");
            this.Height = height;
            this.Width = width;
        }

        public Texture2D GetResult()
        {
            Texture2D res = new Texture2D(this.Graph.Game.GraphicsDevice, Width, Height);
            res.SetData<Color>(this.getInput("In")(this.Height, this.Width));
            return res;
        }

        public Render In
        {
            get { return this.getInput("In"); }
            set { this.SetInput("In", value); }
        }

        public string Name { get; set; }
    }
}
