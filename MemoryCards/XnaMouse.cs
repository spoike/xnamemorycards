using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MemoryCards
{
    public class XnaMouse
    {
        private MemoryBoard board;
        private bool isPressed;

        public XnaMouse(MemoryBoard board)
        {
            this.board = board;
        }

        public void UpdateMouse()
        {
            MouseState currentState = Mouse.GetState();
            if (isPressed == false && currentState.LeftButton == ButtonState.Pressed)
            {
                int mouseX = currentState.X;
                int mouseY = currentState.Y;
                board.FlipCard(mouseX, mouseY);
                isPressed = true;
            }
            else if (isPressed == true && currentState.LeftButton == ButtonState.Released)
            {
                isPressed = false;
            }
        }
    }
}
