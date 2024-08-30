using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Player
{
    public class Player : MonoBehaviour
    {
        PlayerDatastore playerDatastore;  //データストアがパラメータを持っている
        Rigidbody2D rb;
        PlayerMove playerMove;
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