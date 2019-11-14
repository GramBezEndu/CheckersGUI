using CheckersGUI.Draw;
using CheckersLogic;
using CheckersLogic.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGUI.Update
{
    public static class UpdateMethods
    {
        public static void Update(this PlayerVsPlayer playerVsPlayer, Vector2 statePosition, GameTime gameTime)
        {
            DrawMethods.backButton.Update(gameTime);
            playerVsPlayer.board.Update(new Vector2(statePosition.X + DrawMethods.boardPlacement.X, statePosition.Y + DrawMethods.boardPlacement.Y), gameTime);
            playerVsPlayer.AcceptResetMove();
        }

        public static void AcceptResetMove(this GameState gameState)
        {
            if (Game1.inputManager.CurrentKeyboardState.IsKeyDown(Keys.R) && Game1.inputManager.PreviousKeyboardState.IsKeyUp(Keys.R))
                gameState.board.ResetMove();
            //Accept move is now set to "A" button
            else if (Game1.inputManager.CurrentKeyboardState.IsKeyDown(Keys.A) && Game1.inputManager.PreviousKeyboardState.IsKeyUp(Keys.A))
            {
                // TODO: Uzupełnić metodę
                bool moveAccepted = gameState.board.AcceptMove();
                if (!moveAccepted)
                {
                    // TODO: wypisać komunikat użytkownikowi
                    throw new NotImplementedException();
                }
            }
        }

        public static void Update(this PlayerVsComputer playerVsComp, Vector2 statePosition, GameTime gameTime)
        {
            DrawMethods.backButton.Update(gameTime);
            if(playerVsComp.board.IsWhiteTurn)
            {
                playerVsComp.board.Update(new Vector2(statePosition.X + DrawMethods.boardPlacement.X, statePosition.Y + DrawMethods.boardPlacement.Y), gameTime);
                playerVsComp.AcceptResetMove();
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                //Ruch komputera
                RandomComputerAgent agent = new RandomComputerAgent(playerVsComp.board);
                // Wyszukanie najlepszego rozwiązania
                (Pawn, List<BrownSquare>) move = agent.SearchForBestMove();
                playerVsComp.board.SetSelectedSquareAsStart((BrownSquare)move.Item1.Position);
                playerVsComp.board.selectedSquaresToEnd = move.Item2;

                playerVsComp.board.AcceptMove();
            }
        }

        public static void Update(this MenuState menu, Vector2 statePosition, GameTime gameTime)
        {
            DrawMethods.playVsPlayer.Update(gameTime);
            DrawMethods.playVsComputer.Update(gameTime);
        }

        public static void Update(this State state, Vector2 statePosition, GameTime gameTime)
        {
            if(state is PlayerVsPlayer)
            {
                (state as PlayerVsPlayer).Update(statePosition, gameTime);
            }
            else if (state is PlayerVsComputer)
            {
                (state as PlayerVsComputer).Update(statePosition, gameTime);
            }
            else if(state is MenuState)
            {
                (state as MenuState).Update(statePosition, gameTime);
            }
        }

        public static void Update(this Board board, Vector2 boardPosition, GameTime gameTime)
        {
            //Check for mouse input (hard coded texture width and height)
            for (int i = 0; i < board.squares.Length; i++)
            {
                for (int j = 0; j < board.squares[i].Length; j++)
                {
                    var mouseRectangle = new Rectangle(Game1.inputManager.CurrentMouseState.X, Game1.inputManager.CurrentMouseState.Y, 1, 1);

                    //bool isHovering = false;

                    if(mouseRectangle.Intersects(new Rectangle((int)(boardPosition.X + i * 64), (int)(boardPosition.Y + j * 64), 64, 64)))
                    {
                        if(Game1.inputManager.CurrentMouseState.LeftButton == ButtonState.Pressed && Game1.inputManager.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            if(board.squares[i][j] is BrownSquare)
                            {
                                //Call OnInteraction method
                                board.OnInteraction(board.squares[i][j] as BrownSquare);
                            }
                        }

                    }
                }
            }
        }
    }
}
