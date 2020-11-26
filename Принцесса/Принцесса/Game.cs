using System;
using System.Collections.Generic;
using System.Text;

namespace Принцесса
{
    public class Game
    {
        Field field = new Field();
        public void StartGame()
        {
            field.GetMovement();
        }
    }
}
