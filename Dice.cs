using System;

namespace DiceMonteCarlo
{
    public class Dice
    {
        public DiceSide CurrentSide { get; private set; }
        public void Roll()
        {
            var random = new Random();
            CurrentSide = (DiceSide)Enum.ToObject(typeof(DiceSide), random.Next(1, 7));
        }
    }

    public enum DiceSide
    {
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
    }
}