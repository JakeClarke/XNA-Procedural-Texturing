using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XPT.Gui
{
    class TitledPanel : Panel
    {
        Label titleLabel;
        public TitledPanel(string title, Rectangle bounds) : base(bounds)
        {
            this.titleLabel = new Label(title, new Vector2(10f));
            this.AddChild(this.titleLabel);
        }

        /// <summary>
        /// Gets or set the title text for this panel.
        /// </summary>
        public string TitleText 
        {
            get
            {
                return this.titleLabel.Text;
            }
            set
            {
                this.titleLabel.Text = value;
            }
        }

    }
}
