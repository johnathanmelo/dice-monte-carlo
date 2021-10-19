using System;
using System.Collections.Generic;

namespace DiceMonteCarlo
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
            Rounds = 0;
            ResetVictoriesPlayers();

            while (Rounds < MaxRounds)
            {
                PlayRound();
                DisplayProgress();

                Rounds++;
            }
        }

        private void PlayRound()
        {
            ResetStatusPlayers();
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

        private void ResetVictoriesPlayers()
        {
            foreach (var player in Players)
            {
                player.ResetVictories();
            }
        }

        private void ResetStatusPlayers()
        {
            foreach (var player in Players)
            {
                player.ResetStatus();
            }
        }

        private void DisplayProgress()
        {
            if (Rounds % 100 != 0)
            {
                return;
            }

            var progress = Convert.ToDouble(Rounds) * 100 / Convert.ToDouble(MaxRounds);

            Console.Clear();
            Console.WriteLine($"Progress: {Math.Round(progress)}%");
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