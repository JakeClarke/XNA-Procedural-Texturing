using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XPTLib;
using System.Diagnostics;

namespace XPT
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gRes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Graph g = new Graph(this);

            DateTime start = DateTime.Now;
            XPTLib.Nodes.FlatColour redNode = new XPTLib.Nodes.FlatColour(Color.Red), blueNode = new XPTLib.Nodes.FlatColour(Color.Blue), blendMask = new XPTLib.Nodes.FlatColour(Color.Gray);
            XPTLib.Nodes.Noise noise = new XPTLib.Nodes.Noise();
            XPTLib.Nodes.Blend blend = new XPTLib.Nodes.Blend();
            blend.Foreground = redNode.Out;
            blend.Background = blueNode.Out;
            blend.BlendMask = noise.Out;

            g.AddNode(redNode);
            g.AddNode(blueNode);
            g.AddNode(blendMask);
            g.AddNode(noise);

            this.output = new XPTLib.Nodes.Output(200, 200);
            g.AddNode(output);
            output.In = blend.Out;
            res = output.GetResult();
            Debug.WriteLine("Time to compute texture: " + DateTime.Now.Subtract(start).TotalSeconds);
            base.Initialize();
        }
        XPTLib.Nodes.Output output;
        Texture2D res;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(output.GetResult(), Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
