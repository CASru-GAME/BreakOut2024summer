using UnityEngine;

namespace App.Main.Player
{
    public class Player : MonoBehaviour
    {
        PlayerDatastore playerDatastore;  //データストアがパラメータを持っている
        private Rigidbody2D rb;
        private PlayerMove playerMove;
        private LevelSystem levelSystem;
        void Start()
        {
            playerDatastore = GetComponent<PlayerDatastore>();
            playerDatastore.InitializePlayer();

            rb = GetComponent<Rigidbody2D>();
            playerMove = new PlayerMove(rb, playerDatastore);
            levelSystem = new LevelSystem(playerDatastore);
        }

        void Update()
        {
            playerMove.Move();  //PlayerMoveクラスのMoveメソッドを呼び出す
        }
    }
}