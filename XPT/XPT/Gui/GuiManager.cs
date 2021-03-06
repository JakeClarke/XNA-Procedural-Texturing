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
using XPTLib.Nodes;
using XPT.Gui.GraphEditor;


namespace XPT.Gui
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GuiManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<GuiRenderable> guiComponent = new List<GuiRenderable>();
        Graph activeGraph;
        bool initialized = false;
        OutputPreview outputPreview;

        public GuiManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            this.Content = Game.Content;
            this.outputPreview = new OutputPreview(new Vector2(700f, 200f));
            this.AddChild(outputPreview);
            this.NodeGraph = new GraphContainer(new Rectangle(0, 0, 600, 600));
            this.AddChild(NodeGraph);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Mouse.Update();
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            Mouse.LoadContent(this.Game.Content.Load<Texture2D>("Textures\\Mouse"));
            this.DefaultFont = this.Game.Content.Load<SpriteFont>("Fonts\\Default");

            foreach (var item in this.guiComponent)
            {
                item.LoadContent();
            }
            this.initialized = true;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (var item in this.guiComponent)
            {
                item.Draw(spriteBatch, gameTime, Vector2.Zero);
            }

            Mouse.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddChild(GuiRenderable child)
        {
            this.guiComponent.Add(child);
            child.Manager = this;
            if (this.initialized)
                child.LoadContent();
        }

        public void RemoveChild(GuiRenderable child)
        {
            this.guiComponent.Remove(child);
            child.Manager = null;
        }

        public Graph ActiveGraph {
            get { return this.activeGraph; }
            set 
            {
                this.NodeGraph.LoadGraph(value);
                this.activeGraph = value; 
            }
        }

        public ContentManager Content { get; set; }

        public SpriteFont DefaultFont { get; private set; }

        public Output PreviewOutputTarget
        {
            get { return this.outputPreview.OutputNode; }
            set { this.outputPreview.OutputNode = value; }
        }

        internal GraphContainer NodeGraph { get; set; }
    }
}
