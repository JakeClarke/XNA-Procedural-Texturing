﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XPT.Gui
{
    public abstract class GuiRenderable
    {
        List<GuiRenderable> children = new List<GuiRenderable>();
        private bool initalized = false;

        public bool Enabled { get; set; }

        public GuiRenderable()
        {
            this.Enabled = true;
        }

        public virtual void LoadContent()
        {
            initalized = true;
            foreach (var item in children)
            {
                item.LoadContent();
            }
        }

        public virtual void Draw(SpriteBatch guiSpriteBatch, GameTime gameTime, Vector2 position)
        {
            if (this.Enabled == false)
            {
                return;
            }

            foreach (var item in children)
            {
                if (item.Enabled)
                {
                    item.Draw(guiSpriteBatch, gameTime, position);
                }

            }
        }

        public void AddChild(GuiRenderable child)
        {
            this.children.Add(child);
            child.Manager = Manager;
            if (this.initalized)
                child.LoadContent();
        }

        public void RemovedChild(GuiRenderable child)
        {
            this.children.Remove(child);
            child.Manager = null;
        }

        private GuiManager manager;

        public GuiManager Manager
        {
            get { return this.manager; }
            set
            {
                this.manager = value;
                foreach (var item in this.children)
                {
                    item.Manager = this.manager;
                }
            }
        }
    }
}
