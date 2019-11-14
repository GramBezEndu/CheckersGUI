using CheckersGUI.Controls.Buttons;
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
        public static readonly Vector2 boardPlacement = new Vector2(416, 104);
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
                spriteBatch.Draw(Game1.Textures[c.GetType().Name], pos, null, color, 0f, new Vector2(0, 0), new Vector2(0.25f, 0.25f), SpriteEffects.None, 0f);
        }

        public static void Draw(this Pawn c, Vector2 pos, SpriteBatch spriteBatch, Color color)
        {
            if(c is ManPawn)
            {
                if(c.IsWhite)
                    spriteBatch.Draw(Game1.Textures["WhiteMan"], pos, null, color, 0f, new Vector2(0, 0), new Vector2(0.25f, 0.25f), SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(Game1.Textures["BlackMan"], pos, null, color, 0f, new Vector2(0, 0), new Vector2(0.25f, 0.25f), SpriteEffects.None, 0f);
            }
            else if(c is Dame)
            {
                if (c.IsWhite)
                    spriteBatch.Draw(Game1.Textures["WhiteDame"], pos, null, color, 0f, new Vector2(0, 0), new Vector2(0.25f, 0.25f), SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(Game1.Textures["BlackDame"], pos, null, color, 0f, new Vector2(0, 0), new Vector2(0.25f, 0.25f), SpriteEffects.None, 0f);
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
                (state as MenuState).Draw(statePosition, spriteBatch);
            }
        }

        public static void Init(this State state)
        {
            if (state is MenuState menuState)
                menuState.Init();
            else if (state is GameState gameState)
                gameState.Init();
        }

        public static void Init(this GameState gameState)
        {
            acceptMove = new TextButton(Game1.inputManager, Game1.Font, "Accept move")
            {
                Position = new Vector2(20, 300),
                Color = Color.White,
                OnClick = (o, e) => gameState.board.AcceptMove()
            };
            resetMove = new TextButton(Game1.inputManager, Game1.Font, "Reset move")
            {
                Position = new Vector2(acceptMove.Position.X, acceptMove.Position.Y + acceptMove.Size.Y),
                Color = Color.White,
                OnClick = (o, e) => gameState.board.ResetMove()
        };
            backButton = new TextButton(Game1.inputManager, Game1.Font, "Back")
            {
                Position = new Vector2(20, 600),
                Color = Color.White,
                OnClick = (o, e) => Game1.GameReference.ChangeState(new MenuState())
            };
        }

        public static IButton playVsPlayer;
        public static IButton playVsComputer;
        public static IButton backButton;
        public static IButton acceptMove;
        public static IButton resetMove;
        public static void Init(this MenuState menu)
        {
            playVsPlayer = new TextButton(Game1.inputManager, Game1.Font, "Play Vs Player")
            {
                Position = new Vector2(20, 300),
                Color = Color.White,
                OnClick = (o, e) => Game1.GameReference.ChangeState(new PlayerVsPlayer())
            };
            playVsComputer = new TextButton(Game1.inputManager, Game1.Font, "Play Vs Computer")
            {
                Position = new Vector2(playVsPlayer.Position.X, 300 + playVsPlayer.Size.Y),
                Color = Color.White,
                OnClick = (o, e) => Game1.GameReference.ChangeState(new PlayerVsComputer())
            };
        }

        public static void Draw(this MenuState menuState, Vector2 statePosition, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Textures["DarkWood"], new Vector2(0, 0), null, Color.OrangeRed, 0f, new Vector2(0, 0), new Vector2(1f, 1f), SpriteEffects.None, 0f);
            playVsPlayer.Draw(spriteBatch);
            playVsComputer.Draw(spriteBatch);
        }

        public static void Draw(this GameState gameState, Vector2 statePosition, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Textures["DarkWood"], new Vector2(0, 0), null, Color.OrangeRed, 0f, new Vector2(0, 0), new Vector2(1f, 1f), SpriteEffects.None, 0f);
            //Draw text: what gamemode it is
            spriteBatch.DrawString(Game1.Font, gameState.GetType().Name, new Vector2(statePosition.X, statePosition.Y), Color.White);
            gameState.board.Draw(new Vector2(statePosition.X + boardPlacement.X, statePosition.Y + boardPlacement.Y), spriteBatch);
            acceptMove.Draw(spriteBatch);
            resetMove.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
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
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + i * 64, pos.Y + j * 64), spriteBatch, Color.Green * 0.9f);
                    //Draw squares selected by user to the end path
                    else if (board.selectedSquaresToEnd.Contains(board.squares[i][j]))
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + i * 64, pos.Y + j * 64), spriteBatch, Color.Red);
                    //Draw rest of the squares
                    else
                        (board.squares[i][j] as DrawableComponent).Draw(new Vector2(pos.X + i * 64, pos.Y + j * 64), spriteBatch, Color.White);
                    if(board.squares[i][j] is BrownSquare)
                    {
                        var pawn = (board.squares[i][j] as BrownSquare).Pawn;
                        if (pawn != null)
                            (pawn as DrawableComponent).Draw(new Vector2(pos.X + i * 64, pos.Y + j * 64), spriteBatch, Color.White);
                    }
                }
            }
            //Draw text: who turn it is
            spriteBatch.DrawString(Game1.Font, board.TurnMessage, new Vector2(pos.X + 70, pos.Y -32), Color.White);
        }
    }
}
