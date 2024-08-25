using System;

namespace App.Main.Player
{
    public class AttackPoint
    {
        private readonly int _currentValue;
        public int CurrentValue => _currentValue;
        private readonly int _maxValue;
        public int MaxValue => _maxValue;

        private AttackPoint(int CurrentValue, int MaxValue)
        {
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            else if (CurrentValue > MaxValue)
            {
                throw new ArgumentException("Value cannot over MaxValue");
            }
            this._currentValue = CurrentValue;
            this._maxValue = MaxValue;
        }

        public AttackPoint(int value) : this(value, value) { }

        public AttackPoint AddCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue + value.CurrentValue, _maxValue + value.MaxValue);
        }

        public AttackPoint SubtractCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue - value.CurrentValue, _maxValue - value.MaxValue);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, AttackPoint : {this._currentValue}.");
        }
    }
}