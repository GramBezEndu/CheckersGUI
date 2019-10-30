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
        /// Board placement related to start of the scene
        /// </summary>
        public static readonly Vector2 boardPlacement = new Vector2(300, 50);
        /// <summary>
        /// Draw method for pawns and squares
        /// </summary>
        /// <param name="c"></param>
        /// <param name="pos"></param>
        /// <param name="spriteBatch"></param>
        public static void Draw(this DrawableComponent c, Vector2 pos, SpriteBatch spriteBatch, Color color)
        {
            if (c is Pawn)
                (c as Pawn).Draw(pos, spriteBatch, color);
            else
                spriteBatch.Draw(Game1.Textures[c.GetType().Name], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
        }

        public static void Draw(this Pawn c, Vector2 pos, SpriteBatch spriteBatch, Color color)
        {
            if(c is ManPawn)
            {
                if(c.IsWhite)
                    spriteBatch.Draw(Game1.Textures["WhiteMan"], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(Game1.Textures["BlackMan"], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
            }
            else if(c is Dame)
            {
                if (c.IsWhite)
                    spriteBatch.Draw(Game1.Textures["WhiteDame"], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(Game1.Textures["BlackDame"], pos, null, null, null, 0f, new Vector2(1f, 1f), color, SpriteEffects.None, 0f);
            }
        }

        public static void Draw(this State state, Vector2 statePosition, SpriteBatch spriteBatch)
        {
            if(state is GameState)
            {
                (state as GameState).Draw(statePosition, spriteBatch);
            }
            else if(state is MenuState)
            {
                //
            }
        }

        public static void Draw(this GameState gameState, Vector2 statePosition, SpriteBatch spriteBatch)
        {
            //Draw text: what gamemode it is
            spriteBatch.DrawString(Game1.Font, gameState.GetType().Name, new Vector2(statePosition.X, statePosition.Y), Color.Black);
            //Draw text: how to reset move (now only keyboard button)
            spriteBatch.DrawString(Game1.Font, "Reset move [R]", new Vector2(statePosition.X, statePosition.Y + 40), Color.Black);
            //Draw text: how to accept move (now only keyboard button)
            spriteBatch.DrawString(Game1.Font, "Accept move [A]", new Vector2(statePosition.X, statePosition.Y + 80), Color.Black);
            gameState.board.Draw(new Vector2(statePosition.X + boardPlacement.X, statePosition.Y + boardPlacement.Y), spriteBatch);
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
                    //Draw starting square
                    if (board.squares[i][j] == board.GetSelectedSquareAsStart())
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + j * 32, pos.Y + i * 32), spriteBatch, Color.Green);
                    //Draw squares selected by user to the end path
                    else if (board.selectedSquaresToEnd.Contains(board.squares[i][j]))
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + j * 32, pos.Y + i * 32), spriteBatch, Color.Red);
                    //Draw rest of the squares
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
            //Draw text: who turn it is
            spriteBatch.DrawString(Game1.Font, board.TurnMessage, new Vector2(pos.X - 32, pos.Y -32), Color.Black);
        }
    }
}
