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

        // needed for image resizing.
        RenderTarget2D renderTarget;
        static SpriteBatch spriteBatch;

        public FromTexture2D(Graph g, Texture2D texture) : base(g)
        {
            this.Texture = texture;
            this.registerOutput("File", getTexture);
        }

        Color[] getTexture(int height, int width)
        {
            Color[] buff = new Color[height * width];

            if (this.Texture.Height != height && this.Texture.Width != width)
            {
                // we need to resize.
                if (this.renderTarget == null || this.renderTarget.Height != height || this.renderTarget.Width != width) // render target is no good.
	            {
                    if (spriteBatch == null)
                    {
                        spriteBatch = new SpriteBatch(this.Graph.Game.GraphicsDevice);
                    }

                    this.renderTarget = new RenderTarget2D(this.Graph.Game.GraphicsDevice, width, height);

                    // making use of the gpu to generate a scaled version of the image we want.
                    // i feel that this is kind of a hack but if it works...
                    this.Graph.Game.GraphicsDevice.SetRenderTarget(renderTarget);
                    this.Graph.Game.GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin();
                    spriteBatch.Draw(this.Texture, new Rectangle(0, 0, width, height), Color.White);
                    spriteBatch.End();
                    this.Graph.Game.GraphicsDevice.SetRenderTarget(null);
	            }

                renderTarget.GetData<Color>(buff);
            }
            else
            {
                this.Texture.GetData<Color>(buff);
            }

            return buff;
        }

        public Render Out { get { return this.GetOutput("File"); } }
    }
}
