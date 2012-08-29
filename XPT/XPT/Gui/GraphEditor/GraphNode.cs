using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XPTLib.Nodes;

namespace XPT.Gui.GraphEditor
{
    /// <summary>
    /// Node preview to appear in the graph container.
    /// </summary>
    class GraphNode : GuiRenderable
    {
        BaseNode node;
        TitledPanel container;
        string[] nodeOutputs, nodeInputs;
        bool isInvalid = true;
        Texture2D[] previewTextures;
        static readonly Rectangle previewBounds = new Rectangle(5, 35, 90, 90);
        static readonly Color defaultColor = Color.Green * 0.5f, unlinkedColor = Color.Red * 0.5f, outputColor = Color.Blue * 0.5f;

        public GraphNode(Vector2 position, BaseNode node)
        {
            this.Position = position;
            this.node = node;
            // get all the inputs and outputs for display later.
            this.nodeOutputs = this.node.GetOuputNames();
            this.nodeInputs = this.node.GetInputNames();
            // set up the output preview texture array.
            if (this.node is Output)// doesn't have a normal output
                this.previewTextures = new Texture2D[1];
            else
                this.previewTextures = new Texture2D[nodeOutputs.Length];
            this.container = new TitledPanel(node.GetType().Name, new Rectangle(0, 0, 100, 130));
            this.container.Color = defaultColor;
            this.Linked = false;
            this.AddChild(container);
        }

        public override void LoadContent()
        {
            for (int i = 0; i < this.previewTextures.Length; i++)
            {
                this.previewTextures[i] = new Texture2D(this.Manager.GraphicsDevice, previewBounds.Height, previewBounds.Width);
            }

            this.Invalidate();
            base.LoadContent();
        }

        public override void Draw(SpriteBatch guiSpriteBatch, GameTime gameTime, Vector2 position)
        {

            position += this.Position;

            this.container.Color = this.node is Output ? outputColor : 
                this.Linked ? defaultColor : unlinkedColor;

            base.Draw(guiSpriteBatch, gameTime, position);

            if (this.isInvalid)
            {
                // if this a ouput node then we should just use its normal method of retrieving the texture.
                if (this.node is Output)
                {
                    // There can only be one!
                    this.previewTextures[0] = ((Output)this.node).GetResult();
                }
                else
                {
                    for (int i = 0; i < this.nodeOutputs.Length; i++)
                    {
                        this.previewTextures[i].SetData<Color>(this.node.GetOutput(this.nodeOutputs[i])(previewBounds.Width, previewBounds.Height));
                    }
                }

                this.isInvalid = false;
            }

            Rectangle b = previewBounds;
            b.X += this.container.Bounds.X + (int)(position.X);
            b.Y += this.container.Bounds.Y + (int)(position.Y);

            guiSpriteBatch.Draw(this.previewTextures[0], b, Color.White);
        }

        public BaseNode Node { get { return this.node; } }

        public Vector2 Position { get; set; }

        public void Invalidate()
        {
            this.isInvalid = true;
        }

        public bool Linked { get; set; }
    }
}
