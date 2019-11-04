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
        public static void Update(this GameState gameState, Vector2 statePosition, GameTime gameTime)
        {
            gameState.board.Update(new Vector2(statePosition.X + DrawMethods.boardPlacement.X, statePosition.Y + DrawMethods.boardPlacement.Y), gameTime);
            //Reset is now set to "R" button
            if (gameState is PlayerVsComputer && !gameState.board.IsWhiteTurn)
            {
                System.Threading.Thread.Sleep(500);
                //Ruch komputera
                RandomComputerAgent agent = new RandomComputerAgent(gameState.board);
                // Wyszukanie najlepszego rozwiązania
                (Pawn, List<BrownSquare>) move = agent.SearchForBestMove();
                gameState.board.SetSelectedSquareAsStart((BrownSquare)move.Item1.Position);
                gameState.board.selectedSquaresToEnd = move.Item2;

                gameState.board.AcceptMove();
            }
            else
            {
                if (Game1.inputManager.CurrentKeyboardState.IsKeyDown(Keys.R) && Game1.inputManager.PreviousKeyboardState.IsKeyUp(Keys.R))
                    gameState.board.ResetMove();
                //Accept move is now set to "A" button
                if (Game1.inputManager.CurrentKeyboardState.IsKeyDown(Keys.A) && Game1.inputManager.PreviousKeyboardState.IsKeyUp(Keys.A))
                {
                    // TODO: Uzupełnić metodę
                    bool moveAccepted = gameState.board.AcceptMove();
                    if (!moveAccepted)
                    {
                        // TODO: wypisać komunikat użytkownikowi
                        throw new NotImplementedException();
                    }
                    //// TODO: Wyodrębnić to jako metodę update na state PlayerVsComputer może?
                    //else if (moveAccepted && gameState is PlayerVsComputer && !gameState.board.IsWhiteTurn)
                    //{
                    //    System.Threading.Thread.Sleep(500);
                    //    //Ruch komputera
                    //    RandomComputerAgent agent = new RandomComputerAgent(gameState.board);
                    //    // Wyszukanie najlepszego rozwiązania
                    //    (Pawn, List<BrownSquare>) move = agent.SearchForBestMove();
                    //    gameState.board.SetSelectedSquareAsStart((BrownSquare)move.Item1.Position);
                    //    gameState.board.selectedSquaresToEnd = move.Item2;

                    //    gameState.board.AcceptMove();
                    //    //if(move.Item2.Count == 1)
                    //    //{
                    //    //    int xDistance = move.Item2[0].xIndex - move.Item1.Position.xIndex;
                    //    //    int yDistance = move.Item2[0].yIndex - move.Item1.Position.yIndex;
                    //    //    if (Math.Abs(xDistance) > 1 && Math.Abs(yDistance) > 1)
                    //    //    {
                    //    //        //Ruch jest biciem
                    //    //        throw new NotImplementedException();

                    //    //    }
                    //    //    gameState.board.MovePawn((BrownSquare)move.Item1.Position, move.Item2[0]);
                    //    //}
                    //    //else
                    //    //{
                    //    //    // Wielokrotny ruch
                    //    //    gameState.board.MovePawn((BrownSquare)move.Item1.Position, move.Item2.Last());
                    //    //    throw new NotImplementedException();
                    //    //}
                    //}

                }
            }
               
        }

        public static void Update(this State state, Vector2 statePosition, GameTime gameTime)
        {
            if(state is GameState)
            {
                (state as GameState).Update(statePosition, gameTime);
            }
            else if(state is MenuState)
            {
                //
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

                    if(mouseRectangle.Intersects(new Rectangle((int)(boardPosition.X + i * 32), (int)(boardPosition.Y + j * 32), 32, 32)))
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
