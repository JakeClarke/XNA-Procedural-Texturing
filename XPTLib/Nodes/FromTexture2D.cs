using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPTLib.Nodes
{
    public class FromTexture2D : BaseNode
    {
        public Texture2D Texture { get; set; }

        public FromTexture2D(Graph g, Texture2D texture) : base(g)
        {
            this.Texture = Texture;
            this.registerOutput("File", getTexture);
        }

        Color[] getTexture(int height, int width)
        {
            Color[] buff = new Color[height * width];
            this.Texture.GetData<Color>(buff);

            return buff;
        }

    }
}
