using UnityEngine;
using App.Main.Block;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace App.Main.Ball
{
    public class CollisionDetector_forYellowSubmarine : MonoBehaviour
    {
        int damage = 0;

        public void SetDamage(int StackCount, int base_damage)
        {
            this.damage = base_damage * (1 - 1 / StackCount + 1);
        }

        private async void OnCollisionEnter2D(Collision2D collision)
        {
            List<Task> tasks = new List<Task>();

            // ここに衝突時の処理を記述
            IBlock block = collision.gameObject.GetComponent<IBlock>();
            if (block != null)
            {
                tasks.Add(Task.Run(() => block.TakeDamage(damage)));
            }
            await Task.WhenAll(tasks);

            Destroy(gameObject);
        }
    }
}
