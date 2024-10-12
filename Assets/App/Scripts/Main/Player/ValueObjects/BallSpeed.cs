using System;

namespace App.Main.Player
{
    public class BallSpeed
    {
        /// <summary>
        /// The speed of the player
        /// </summary>
        private readonly float _speed;
        public float Speed => _speed;

        public BallSpeed(float speed)
        {
            if (speed == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed cannot be zero");
            }

            _speed = speed;
        }

        public BallSpeed AddCurrentValue(BallSpeed value)
        {
            return new BallSpeed(_speed + value.Speed);
        }

        public BallSpeed SubtractCurrentValue(BallSpeed value)
        {
            return new BallSpeed(_speed - value.Speed);
        }

        public BallSpeed MultiplyCurrentValue(BallSpeed value)
        {
            return new BallSpeed(_speed * value.Speed);
        }

        public BallSpeed DivideCurrentValue(BallSpeed value)
        {
            return new BallSpeed(_speed / value.Speed);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, BallSpeed : {this._speed}.");
        }
    }


}