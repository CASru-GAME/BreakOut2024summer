using System;

namespace App.Main.Player
{
    public class DumbbellPower
    {
        /// <summary>
        /// The power of Dummbell
        /// </summary>
        private readonly int _power;
        public int Power => _power;

        public DumbbellPower(int power)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power), "Power cannot be negative");
            }

            _power = power;
        }

        public DumbbellPower AddCurrentValue(DumbbellPower value, int stackCount)
        {
            return new DumbbellPower(Math.Min(_power + value.Power, stackCount * 3));
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, DumbbellPower : {this._power}.");
        }
    }
}