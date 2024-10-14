using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Block;
using App.Main.Player;

namespace App.Main.Ball
{
    public class BallTrigger : MonoBehaviour
    {
        [SerializeField] private Ball Ball;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IBlock block = other.gameObject.GetComponent<IBlock>();
            if (block != null)
            {
                Ball.AttackHandling(block);
                Ball.DecreasePathThroughCount();
            }
        }
    }
}