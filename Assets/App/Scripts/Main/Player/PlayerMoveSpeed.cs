using System;

namespace App.Main.Player
{
    public class PlayerMoveSpeed
    {
        /// <summary>
        /// The speed of the player
        /// </summary>
        private readonly float _speed;
        public float Speed => _speed;

        public PlayerMoveSpeed(float speed)
        {
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed cannot be negative");
            }

            _speed = speed;
        }

        public PlayerMoveSpeed AddCurrentValue(PlayerMoveSpeed value)
        {
            return new PlayerMoveSpeed(_speed + value.Speed);
        }

        public PlayerMoveSpeed SubtractCurrentValue(PlayerMoveSpeed value)
        {
            return new PlayerMoveSpeed(_speed - value.Speed);
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, PlayerMoveSpeed : {this._speed}.");
        }
    }


}