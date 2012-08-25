using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XPTLib.Nodes
{
    public class Noise : BaseNode
    {
        public Noise()
        {
            this.registerOutput("Out", generateNoise);
        }

        Color[] generateNoise(int height, int width)
        {
            Color[] outC = new Color[height * width];

            for (int i = 0; i < height * width; i++)
            {
                float gs = (float)Rand.Random.NextDouble();
                outC[i] = new Color(gs, gs, gs);
            }

            return outC;
        }

        public Render Out { get { return this.GetOutput("Out"); } }
    }
}
