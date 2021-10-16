using System;
using System.Collections.Generic;

namespace playground
{
    public class Game
    {
        private UInt64 Rounds { get; set; }
        private UInt64 MaxRounds { get; set; }
        private Dice Dice { get; set; }
        private ICollection<Player> Players { get; set; }

        public Game(int numPlayers, UInt64 maxRounds)
        {
            this.Rounds = 0;
            this.MaxRounds = maxRounds;
            this.Dice = new Dice();
            this.Players = new SortedSet<Player>();

            for (int i = 0; i < numPlayers; i++)
            {
                this.Players.Add(new Player());
            }
        }

        public void Play()
        {
            while (Rounds < MaxRounds)
            {
                PlayRound();
                Rounds++;
            }
        }

        private void PlayRound()
        {
            ResetPlayerStatus();
            var enumerator = Players.GetEnumerator();

            while (true)
            {
                if (enumerator.MoveNext() == false)
                {
                    enumerator.Reset();
                    continue;
                }

                var player = enumerator.Current;
                player.RollDice(Dice);

                if (player.Status == PlayerStatus.WIN)
                {
                    break;
                }
            }
        }

        private void ResetPlayerStatus()
        {
            foreach (var player in Players)
            {
                player.Status = PlayerStatus.NONE;
            }
        }

        public void GetStatistics()
        {
            Console.WriteLine($"Rounds: {Rounds}");

            foreach (var player in Players)
            {
                var victoryPercentage = Convert.ToDouble(player.Victories) / Convert.ToDouble(Rounds);

                Console.WriteLine($"Player {player.Id}: {player.Victories} ({victoryPercentage})");
            }
        }
    }
}