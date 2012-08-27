using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPTLib.Nodes
{
    public class FlatColour : BaseNode
    {
        public Color Colour { get; set; }


        public FlatColour(Graph g) : this(g, Color.White)
        {
            
        }

        public FlatColour(Graph g, Color colour) : base(g)
        {
            this.Colour = colour;
            this.registerOutput("Out", generateDiffuse);
        }


        public Color[] generateDiffuse(int height, int width)
        {
            Color[] cTemp = new Color[height * width];

            for (int i = 0; i < cTemp.Length; i++)
            {
                cTemp[i] = this.Colour;
            }

            return cTemp;
        }

        public Render Out { get { return this.generateDiffuse; } }
    }
}
