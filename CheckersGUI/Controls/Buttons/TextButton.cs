using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CheckersGUI.Controls.Buttons
{
    public class TextButton : Text, IButton
    {
        protected InputManager inputManager;
        protected bool selected;
        protected Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public TextButton(InputManager im, SpriteFont f, string msg, Vector2 scale) : base(f, msg, scale)
        {
            inputManager = im;
        }

        public TextButton(InputManager im, SpriteFont f, string msg) : base(f, msg)
        {
            inputManager = im;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                //Color red while hovering
                if (Selected)
                    spriteBatch.DrawString(font, Message, Position, Color.Red, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                else
                    spriteBatch.DrawString(font, Message, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                var mouseRectangle = new Rectangle(inputManager.CurrentMouseState.X, 
                    inputManager.CurrentMouseState.Y, 1, 1);

                Selected = false;

                if (mouseRectangle.Intersects(Rectangle))
                    Selected = true;

                if (Selected)
                {
                    if (inputManager.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && inputManager.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        OnClick?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public EventHandler OnClick { get; set; }
        public bool Selected { get; set; }
        public EventHandler OnSelectedChange { get; set; }
    }
}
