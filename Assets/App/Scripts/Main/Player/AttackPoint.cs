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

        public AttackPoint MultiplyCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue * value.CurrentValue);
        }

        public AttackPoint DivideCurrentValue(AttackPoint value)
        {
            return new AttackPoint(_currentValue / value.CurrentValue);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, AttackPoint : {this._currentValue}.");
        }
    }
}