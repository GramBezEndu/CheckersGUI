using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGUI
{
    public interface IDrawableComponent : IComponent
    {
        bool Hidden { get; set; }
        Vector2 Position { get; set; }
        Vector2 Size { get; }
        Color Color { get; set; }
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
