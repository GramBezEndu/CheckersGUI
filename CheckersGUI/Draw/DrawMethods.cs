using CheckersLogic;
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
        public static void Draw(this DrawableComponent c, Vector2 pos, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Textures[c.GetType().Name], pos, null, null, null, 0f, new Vector2(1f, 1f), Color.White, SpriteEffects.None, 0f);
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
                    (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + j*32, pos.Y + i*32), spriteBatch);
            }

            //Draw pawns (hard coded texture width and height)
            foreach(var pawn in board.pawns)
            {
                //Index of square in squares
                int iIndex = -1, jIndex = -1;
                if (pawn.position == null)
                    continue;
                else
                {
                    //Find which square it is
                    //Note: if we keep xIndex and yIndex in square then it can be written simpler
                    for (int i = 0; i < board.squares.Length; i++)
                    {
                        for (int j = 0; j < board.squares[i].Length; j++)
                        {
                            if (pawn.position == board.squares[i][j])
                            {
                                iIndex = i;
                                jIndex = j;
                                (pawn as DrawableComponent).Draw(new Vector2(pos.X + j*32, pos.Y + i*32), spriteBatch);
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}
