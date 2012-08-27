using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPT.Gui
{
    class Panel : GuiRenderable
    {
        private Rectangle bounds;
        Texture2D blank;

        public Panel(Rectangle bounds)
        {
            this.bounds = bounds;
        }


        public override void LoadContent()
        {
            this.blank = this.Manager.Content.Load<Texture2D>("Textures\\Blank");
            base.LoadContent();
        }

        public override void Draw(SpriteBatch guiSpriteBatch, GameTime gameTime, Vector2 position)
        {
            Rectangle b = this.Bounds;
            b.X += (int)position.X;
            b.Y += (int)position.Y;
            guiSpriteBatch.Draw(blank, this.bounds, this.color);
            base.Draw(guiSpriteBatch, gameTime, new Vector2(bounds.X, bounds.Y));
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private Color color = Color.Black * 0.5f;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }


    }
}
