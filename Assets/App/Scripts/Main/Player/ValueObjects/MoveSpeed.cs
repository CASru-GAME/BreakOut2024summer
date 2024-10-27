using System;

namespace App.Main.Player
{
    public class MoveSpeed
    {
        /// <summary>
        /// The speed of the player
        /// </summary>
        private readonly float _speed;
        public float Speed => _speed;

        public MoveSpeed(float speed)
        {
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed cannot be negative");
            }

            _speed = speed;
        }

        public MoveSpeed AddCurrentValue(MoveSpeed value)
        {
            return new MoveSpeed(_speed + value.Speed);
        }

        public MoveSpeed SubtractCurrentValue(MoveSpeed value)
        {
            return new MoveSpeed(_speed - value.Speed);
        }

        public MoveSpeed MultiplyCurrentValue(MoveSpeed value)
        {
            return new MoveSpeed(_speed * value.Speed);
        }

        public MoveSpeed DivideCurrentValue(MoveSpeed value)
        {
            return new MoveSpeed(_speed / value.Speed);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, MoveSpeed : {this._speed}.");
        }
    }
}