﻿using CheckersGUI.Draw;
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

            var mouseRectangle = new Rectangle(Game1.inputManager.CurrentMouseState.X, Game1.inputManager.CurrentMouseState.Y, 1, 1);
            if (Game1.inputManager.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && Game1.inputManager.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (mouseRectangle.Intersects(new Rectangle(5, 104, 214, 512)))
                {
                    playerVsPlayer.board.AcceptMove();
                }
                if (mouseRectangle.Intersects(new Rectangle(741, 104, 214, 512)))
                {
                    playerVsPlayer.board.ResetMove();
                }
                if (mouseRectangle.Intersects(new Rectangle(5, 621, 950, 58)))
                {
                    Game1.GameReference.ChangeState(new MenuState());
                }
            }

            //DrawMethods.acceptMove.Update(gameTime);
            //DrawMethods.resetMove.Update(gameTime);
            //DrawMethods.backButton.Update(gameTime);
            playerVsPlayer.board.Update(new Vector2(statePosition.X + DrawMethods.boardPlacement.X, statePosition.Y + DrawMethods.boardPlacement.Y), gameTime);
        }

        public static void Update(this PlayerVsComputer playerVsComp, Vector2 statePosition, GameTime gameTime)
        {
            var mouseRectangle = new Rectangle(Game1.inputManager.CurrentMouseState.X, Game1.inputManager.CurrentMouseState.Y, 1, 1);
            if (Game1.inputManager.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && Game1.inputManager.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (mouseRectangle.Intersects(new Rectangle(5, 104, 214, 512)))
                {
                    playerVsComp.board.AcceptMove();
                }
                if (mouseRectangle.Intersects(new Rectangle(741, 104, 214, 512)))
                {
                    playerVsComp.board.ResetMove();
                }
                if (mouseRectangle.Intersects(new Rectangle(5, 621, 950, 58)))
                {
                    Game1.GameReference.ChangeState(new MenuState());
                }
            }
            //DrawMethods.acceptMove.Update(gameTime);
            //DrawMethods.resetMove.Update(gameTime);
            //DrawMethods.backButton.Update(gameTime);
            if (playerVsComp.board.IsWhiteTurn)
            {
                playerVsComp.board.Update(new Vector2(statePosition.X + DrawMethods.boardPlacement.X, statePosition.Y + DrawMethods.boardPlacement.Y), gameTime);
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

        public static void Update(this MenuState menu, Vector2 statePosition, GameTime gameTime, Game1 game1)
        {
            DrawMethods.playVsPlayer.Update(gameTime);
            DrawMethods.playVsComputer.Update(gameTime);
            if (DrawMethods.currentPlacement.X >= DrawMethods.boardPlacement.X && DrawMethods.animationState != DrawMethods.AnimationState.None)
            {
                Update(gameTime);
            }
            else if (DrawMethods.currentPlacement.X < DrawMethods.boardPlacement.X)
            {
                switch (DrawMethods.animationState)
                {
                    case DrawMethods.AnimationState.PlayerVsPlayer:
                        game1.ChangeState(new PlayerVsPlayer());
                        break;
                    case DrawMethods.AnimationState.PlayerVsComputer:
                        game1.ChangeState(new PlayerVsComputer());
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            float positionX = (DrawMethods.boardPlacement.X - DrawMethods.boardPlacementBeg.X) / 50;
            float positionY = (DrawMethods.boardPlacement.Y - DrawMethods.boardPlacementBeg.Y) / 50;
            Vector2 current = new Vector2(DrawMethods.currentPlacement.X + positionX, DrawMethods.currentPlacement.Y + positionY);
            DrawMethods.currentPlacement = current;
        }


        public static void Update(this State state, Vector2 statePosition, GameTime gameTime, Game1 game1)
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
                (state as MenuState).Update(statePosition, gameTime, game1);
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
