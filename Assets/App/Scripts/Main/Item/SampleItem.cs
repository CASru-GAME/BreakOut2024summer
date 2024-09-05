using UnityEngine;
using App.Main.Player;

namespace App.Main.Item
{
    public class SampleItem : MonoBehaviour, IItem
    {   
        private readonly float _minY = -5f;
        private readonly float _speed = -2f;

        void Start()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed);
        }
        void Update() 
        {   
            if(transform.position.y < _minY) Destroy(gameObject);
        }

        ///<summary>
        ///アイテムの効果を発動する(ボールからこの関数を実行する)
        ///</summary>
        public void ActivateItem(PlayerDatastore playerDatastore)
        {
            //処理を書く(未実装)
        }
    }
}
