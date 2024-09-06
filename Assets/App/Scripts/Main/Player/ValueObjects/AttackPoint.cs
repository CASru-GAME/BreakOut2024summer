using System;

namespace App.Main.Player
{
    public class AttackPoint
    {
        private readonly int _currentValue;
        public int CurrentValue => _currentValue;


        public AttackPoint(int CurrentValue)
        {
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }

            this._currentValue = CurrentValue;
        }

        public AttackPoint AddCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue + value.CurrentValue);
        }

        public AttackPoint SubtractCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue - value.CurrentValue);
        }

        public AttackPoint MultiplyCurrentValue(Double value)
        {
            if (value == 0)
            {
                throw new ArgumentException("Value cannot be zero");
            }
            else if (value < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            return new AttackPoint((int)(_currentValue * value));
        }

        public AttackPoint DivideCurrentValue(Double value)
        {
            if (value == 0)
            {
                throw new ArgumentException("Value cannot be zero");
            }
            else if (value < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            return new AttackPoint((int)(_currentValue / value));
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, AttackPoint : {this._currentValue}.");
        }
    }
}