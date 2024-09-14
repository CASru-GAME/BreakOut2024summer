using UnityEngine;
using App.Main.Player;

namespace App.Main.Item
{
    public class Item : MonoBehaviour, IItem
    {   
        [SerializeField] int Id;
        ItemTable itemTable;
        private readonly float _minY = -5f;
        private readonly float _speed = -2f;

        void Start()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed);
        }

        void Update() 
        {   
            if(transform.position.y < _minY) Suside();
        }
        
        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Initialized(ItemTable itemTable)
        {   
            this.itemTable = itemTable;
            
        }

        private void Suside()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// バーと接触したときにバーが呼び出す
        /// </summary>
        public void GetItem(PlayerDatastore playerDatastore)
        {
            itemTable.items[Id].effect(playerDatastore);
            Suside();
        }
    }
}
