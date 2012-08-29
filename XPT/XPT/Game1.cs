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
using XPT.Gui;

namespace XPT
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GuiManager guiManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = 768;
            this.graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
            this.guiManager = new GuiManager(this);
            this.Components.Add(guiManager);
        }

        Graph graph;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

        }

        XPTLib.Nodes.Output output;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.graph = new Graph(this);

            XPTLib.Nodes.FlatColour redNode = new XPTLib.Nodes.FlatColour(graph, Color.Red), blueNode = new XPTLib.Nodes.FlatColour(graph, Color.Blue), blendMask = new XPTLib.Nodes.FlatColour(graph, Color.Gray);
            XPTLib.Nodes.Noise noise = new XPTLib.Nodes.Noise(graph);
            XPTLib.Nodes.Blend blend = new XPTLib.Nodes.Blend(graph);
            XPTLib.Nodes.FromTexture2D fTex = new XPTLib.Nodes.FromTexture2D(this.graph, this.Content.Load<Texture2D>("Textures\\Mouse"));
            blend.Foreground = redNode.Out;
            blend.Background = blueNode.Out;
            blend.BlendMask = fTex.Out;

            this.output = new XPTLib.Nodes.Output(graph, 200, 200);
            this.guiManager.PreviewOutputTarget = output;

            output.In = blend.Out;

            this.guiManager.ActiveGraph = this.graph;
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

            base.Draw(gameTime);
        }
    }
}
