using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPTLib.Nodes
{
    public class Blend : BaseNode
    {
        public Blend()
        {
            this.registerInput("Background");
            this.registerInput("Foreground");
            this.registerInput("Blend Mask");
            this.registerOutput("Out", renderBlend);
        }

        Color[] renderBlend(int height, int width)
        {
            int pixels = height * width;
            Color[] tex1 = this.getInput("Background")(height, width);
            Color[] tex2 = this.getInput("Foreground")(height, width);
            Color[] blend = this.getInput("Blend Mask")(height, width);
            Color[] resBuff = new Color[pixels];

            for (int i = 0; i < pixels; i++)
            {
                float blendFactor = ((((float)(int)blend[i].R + (int)blend[i].G + (int)blend[i].B)) / 3) / 255f;
                resBuff[i] = Color.Lerp(tex1[i], tex2[i], blendFactor);
            }

            return resBuff;
        }

        public Render Out { get { return this.renderBlend; } }

        public Render Background
        {
            get { return this.getInput("Background"); }
            set { this.SetInput("Background", value); }
        }

        public Render Foreground
        {
            get { return this.getInput("Foreground"); }
            set { this.SetInput("Foreground", value); }
        }

        public Render BlendMask
        {
            get { return this.getInput("Blend Mask"); }
            set { this.SetInput("Blend Mask", value); }
        }
    }
}
