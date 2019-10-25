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
            if (Game1.inputManager.CurrentKeyboardState.IsKeyDown(Keys.R) && Game1.inputManager.PreviousKeyboardState.IsKeyUp(Keys.R))
                gameState.board.ResetMove();
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

                    if(mouseRectangle.Intersects(new Rectangle((int)(boardPosition.X + j * 32), (int)(boardPosition.Y + i * 32), 32, 32)))
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
