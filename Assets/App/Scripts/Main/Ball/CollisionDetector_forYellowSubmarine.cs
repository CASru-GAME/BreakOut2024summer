using UnityEngine;
using App.Main.Block;
using System.Collections.Generic;

namespace App.Main.Ball
{
    public class CollisionDetector_forYellowSubmarine : MonoBehaviour
    {
        int damage = 0;

        private List<GameObject> _damagedBlocks = new List<GameObject>();

        public void SetDamage(int StackCount, int base_damage)
        {
            this.damage = base_damage * (1 - 1 / StackCount + 1);
        }

        private void TakeDamage(GameObject block)
        {
            block.GetComponent<IBlock>().TakeDamage(damage);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Block.IBlock>() == null) return;
            if (_damagedBlocks.Contains(collision.gameObject)) Destroy(gameObject);
            _damagedBlocks.Add(collision.gameObject);
            TakeDamage(collision.gameObject);
        }
    }
}
