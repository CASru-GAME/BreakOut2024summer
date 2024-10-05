using System;

namespace App.Main.Player
{
    public class ComboCount
    {
        private readonly int _currentValue;
        public int CurrentValue => _currentValue;

        public ComboCount(int CurrentValue)
        {
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }

            this._currentValue = CurrentValue;
        }


        public ComboCount AddCurrentValue(ComboCount value)
        {
            return new ComboCount(_currentValue + value.CurrentValue);
        }

        public ComboCount ResetCurrentValue()
        {
            return new ComboCount(0);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, ComboCount : {this._currentValue}.");
        }
    }
}
