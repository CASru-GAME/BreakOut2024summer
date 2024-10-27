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
            this.damage = (int)((float)base_damage * (1f - 1f / (float)((float)StackCount + 1f)) * 0.8f);
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
