using UnityEngine;
using App.Main.Player;
using App.Main.Stage;
using App.ScriptableObjects;

namespace App.Main.Item
{
    public class Item : MonoBehaviour, IItem
    {   
        private StageSystem stageSystem;
        private PlayerDatastore playerDatastore;
        [SerializeField] int Id;
        ItemTable itemTable;
        private readonly float _minY = -5f;
        private readonly float _expSpeed = -2f;
        private readonly float _itemSpeed = -3f;
        [SerializeField] private SpriteData _spriteData;

        void Update() 
        {   
            if(transform.position.y < _minY) Suicide();
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
            GetComponent<SpriteRenderer>().sprite = _spriteData.GetItemSprite(id);
            SetSpeed();
        }

        private void SetSpeed()
        {
            if(Id <= 10) GetComponent<Rigidbody2D>().velocity = new Vector2(0, _expSpeed);
            else GetComponent<Rigidbody2D>().velocity = new Vector2(0, _itemSpeed);
        }

        private void Suicide()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// バーと接触したときにバーが呼び出す
        /// </summary>
        public void GetItem(PlayerDatastore playerDatastore)
        {
            itemTable.items[Id].effect(stageSystem,playerDatastore);
            Suicide();
        }
    }
}
