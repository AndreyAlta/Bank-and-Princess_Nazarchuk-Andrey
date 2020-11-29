namespace Princess
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
