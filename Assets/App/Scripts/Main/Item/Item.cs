using UnityEngine;
using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Item
{
    public class Item : MonoBehaviour, IItem
    {   
        private StageSystem stageSystem;
        private PlayerDatastore playerDatastore;
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
        public void Initialized(ItemTable itemTable,StageSystem stageSystem,PlayerDatastore playerDatastore,int id)
        {   
            this.itemTable = itemTable;
            this.stageSystem = stageSystem;
            this.playerDatastore = playerDatastore;
            Id = id;
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
            itemTable.items[Id].effect(stageSystem,playerDatastore);
            Suside();
        }
    }
}
