using System;

namespace App.Main.Player
{
    public class ExperiencePoint
    {
        private readonly int _currentValue;
        public int CurrentValue => _currentValue;

        public ExperiencePoint(int CurrentValue)
        {
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }

            this._currentValue = CurrentValue;
        }


        public ExperiencePoint AddCurrentValue(ExperiencePoint value)
        {
            return new ExperiencePoint(_currentValue + value.CurrentValue);
        }


        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, ExperiencePoint : {this._currentValue}.");
        }
    }
}
