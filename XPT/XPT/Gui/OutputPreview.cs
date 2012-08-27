using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XPTLib.Nodes;

namespace XPT.Gui
{
    class OutputPreview : TitledPanel
    {
        Texture2D preview;
        bool isDirty = true;
        static readonly Rectangle minBounds = new Rectangle(0,0,200,200);

        public OutputPreview(Vector2 position) : base("Output preview", new Rectangle((int)position.X, (int)position.Y, minBounds.Width, minBounds.Height)) 
        {

        }

        public override void Draw(SpriteBatch guiSpriteBatch, GameTime gameTime, Vector2 position)
        {
            // generate a new preview image if we need one.
            if (isDirty)
            {
                this.isDirty = false;
                if (this.outputNode != null)
                {
                    this.preview = this.outputNode.GetResult();
                    Rectangle newBounds = this.Bounds;
                    newBounds.Width = this.preview.Width + 10;
                    newBounds.Height = this.preview.Height + 40;
                    this.Bounds = newBounds;
                }
                else
                {
                    this.preview = null;
                    Rectangle newBounds = this.Bounds;
                    newBounds.Width = minBounds.Width;
                    newBounds.Height = minBounds.Height;
                    this.Bounds = newBounds;
                }
            }

            base.Draw(guiSpriteBatch, gameTime, position);

            if (this.preview != null)
            {
                guiSpriteBatch.Draw(this.preview, new Rectangle((int)position.X + this.Bounds.X + 5, (int)position.Y + this.Bounds.Y + 35, this.preview.Width, this.preview.Height), Color.White); 
            }

        }

        public void Invalidate()
        {
            this.isDirty = true;
        }

        private Output outputNode;

        public Output OutputNode
        {
            get { return outputNode; }
            set 
            {
                this.Invalidate();
                outputNode = value; 
            }
        }

        
    }
}
