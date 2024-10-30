using UnityEngine;
using App.Main.Block;
using System.Collections.Generic;
using System.Collections;

namespace App.Main.Ball
{
    public class CollisionDetector_forYellowSubmarine : MonoBehaviour
    {
        int damage = 0;

        private List<GameObject> _damagedBlocks = new List<GameObject>();
        [SerializeField] private CreateYellowSubmarineEffect2 _createYellowSubmarineEffect2;

        private void Start()
        {
            StartCoroutine(Suicide());
        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }

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
            _createYellowSubmarineEffect2.Create(collision.transform.position);
            TakeDamage(collision.gameObject);
        }
    }
}
