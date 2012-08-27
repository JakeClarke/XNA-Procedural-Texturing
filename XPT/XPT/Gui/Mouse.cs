using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace XPT.Gui
{
    static class Mouse
    {

        static Texture2D image;
        static MouseState mouseState;

        static public void Update()
        {
            mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        static public void LoadContent(Texture2D image)
        {
            Mouse.image = image;
        }

        static public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, Position, Color.White);
        }

        static public Vector2 Position
        {
            get { return new Vector2(mouseState.X, mouseState.Y); }
        }
    }
}
