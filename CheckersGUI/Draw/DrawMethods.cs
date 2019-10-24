using CheckersLogic;
using CheckersLogic.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGUI.Draw
{
    public static class DrawMethods
    {
        /// <summary>
        /// Draw method for pawns and squares
        /// </summary>
        /// <param name="c"></param>
        /// <param name="pos"></param>
        /// <param name="spriteBatch"></param>
        public static void Draw(this DrawableComponent c, Vector2 pos, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Game1.Textures[c.GetType().Name], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
        }

        public static void Draw(this GameState gameState, Vector2 pos, SpriteBatch spriteBatch)
        {
            gameState.board.Draw(pos, spriteBatch);
        }

        /// <summary>
        /// Draw method for board
        /// </summary>
        /// <param name=""></param>
        /// <param name="pos"></param>
        /// <param name="spriteBatch"></param>
        public static void Draw(this Board board, Vector2 pos, SpriteBatch spriteBatch)
        {
            //Draw squares (hard coded texture width and height)
            for(int i=0;i<board.squares.Length;i++)
            {
                for(int j=0;j<board.squares[i].Length;j++)
                {
                    if (board.squares[i][j] == board.currentlySelectedSquare)
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + j * 32, pos.Y + i * 32), spriteBatch, Color.Green);
                    else
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + j * 32, pos.Y + i * 32), spriteBatch, Color.White);
                    if(board.squares[i][j] is BrownSquare)
                    {
                        var pawn = (board.squares[i][j] as BrownSquare).Pawn;
                        if (pawn != null)
                            (pawn as DrawableComponent).Draw(new Vector2(pos.X + j * 32, pos.Y + i * 32), spriteBatch, Color.White);
                    }
                }
            }
        }
    }
}
