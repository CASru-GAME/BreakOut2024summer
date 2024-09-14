using UnityEngine;
using App.Main.Item;
using App.Main.Player.Park;

namespace App.Main.Player
{
    public class Player : MonoBehaviour
    {
        PlayerDatastore playerDatastore;  //データストアがパラメータを持っている
        private Rigidbody2D rb;
        private PlayerMove playerMove;
        private LevelSystem levelSystem;
        private ParkSystem parkSystem;
        
        
        void Start()
        {
            playerDatastore = GetComponent<PlayerDatastore>();
            playerDatastore.InitializePlayer();
            
            rb = GetComponent<Rigidbody2D>();
            playerMove = new PlayerMove(rb, playerDatastore);
            levelSystem = new LevelSystem(playerDatastore);
            parkSystem = new ParkSystem(playerDatastore);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IItem item = other.gameObject.GetComponent<IItem>();
            if(item != null)
            {
                //ダメージ計算
                item.GetItem(playerDatastore);
            }
            
        }

        void Update()
        {
            playerMove.Move();  //PlayerMoveクラスのMoveメソッドを呼び出す
        }
    }
}