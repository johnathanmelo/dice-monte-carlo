using System;
using System.Diagnostics.CodeAnalysis;

namespace playground
{
    public class Player : IComparable<Player>
    {
        public Guid Id { get; set; }
        public PlayerStatus Status { get; set; }
        public UInt64 Victories { get; set; }

        public Player()
        {
            this.Id = Guid.NewGuid();
            this.Status = PlayerStatus.NONE;
            this.Victories = 0;
        }

        public void RollDice(Dice dice)
        {
            dice.Roll();

            if (dice.CurrentSide == DiceSide.SIX)
            {
                Status = PlayerStatus.WIN;
                Victories++;
            }
        }

        public int CompareTo([AllowNull] Player other)
        {
            if (other == null)
            {
                return 1;
            }

            return Id.CompareTo(other.Id);
        }
    }

    public enum PlayerStatus
    {
        WIN,
        LOSE,
        NONE,
    }
}