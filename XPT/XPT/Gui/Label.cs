using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPT.Gui
{
    class Label : GuiRenderable
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public SpriteFont Font { get; set; }

        public Label(string text, Vector2 position)
        {
            this.Text = text;
            this.Position = position;
        }

        public override void LoadContent()
        {
            this.Font = this.Manager.DefaultFont;
            base.LoadContent();
        }

        public override void Draw(SpriteBatch guiSpriteBatch, GameTime gameTime, Vector2 position)
        {
            guiSpriteBatch.DrawString(this.Font, this.Text, this.Position + position, Color.White);
            base.Draw(guiSpriteBatch, gameTime, position);
        }

        
    }
}
