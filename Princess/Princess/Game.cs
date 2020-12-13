namespace Princess
{
    public class Game
    {
        Field field;
        public Game()
        {
            field = new Field();
        }
        public void StartGame()
        {
            field.BeginMovement();
        }
    }
}
