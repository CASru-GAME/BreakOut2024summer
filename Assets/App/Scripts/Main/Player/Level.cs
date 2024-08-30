using System;

namespace App.Main.Player
{
    public class Level
    {
        private readonly int _currentValue;
        public int CurrentValue => _currentValue;

        public Level(int CurrentValue)
        {
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }

            this._currentValue = CurrentValue;
        }

    }
}