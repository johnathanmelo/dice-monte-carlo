namespace playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(numPlayers: 2, maxRounds: 5000000);

            game.Play();
            game.GetStatistics();
        }
    }
}
