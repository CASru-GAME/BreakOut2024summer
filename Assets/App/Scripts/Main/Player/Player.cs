using UnityEngine;
using App.Main.Item;
using App.Main.Player.Perk;
using App.Main.Stage;

namespace App.Main.Player
{
    public class Player : MonoBehaviour
    {
        PlayerDatastore playerDatastore;  //データストアがパラメータを持っている
        private Rigidbody2D rb;
        private PlayerMove playerMove;
        [SerializeField] private ProcessSystem _processSystem;
        
        void Awake()
        {
            playerDatastore = GetComponent<PlayerDatastore>();
            playerDatastore.InitializePlayer();

            rb = GetComponent<Rigidbody2D>();
            playerMove = new PlayerMove(rb, playerDatastore);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IItem item = other.gameObject.GetComponent<IItem>();
            if(item != null && _processSystem.StageState.isPlaying())
            {
                item.GetItem(playerDatastore);
            }
            
        }

        void Update()
        {
            playerMove.Move();  //PlayerMoveクラスのMoveメソッドを呼び出す
        }
    }
}