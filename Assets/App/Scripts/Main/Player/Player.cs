using UnityEngine;

namespace App.Main.Player
{
    public class Player : MonoBehaviour
    {
        PlayerDatastore playerDatastore;  //データストアがパラメータを持っている
        private Rigidbody2D rb;
        private PlayerMove playerMove;
        void Start()
        {
            playerDatastore = GetComponent<PlayerDatastore>();
            playerDatastore.InitializePlayer();

            rb = GetComponent<Rigidbody2D>();
            playerMove = new PlayerMove(rb, playerDatastore);
        }

        void Update()
        {
            playerMove.Move();  //PlayerMoveクラスのMoveメソッドを呼び出す
        }
    }
}