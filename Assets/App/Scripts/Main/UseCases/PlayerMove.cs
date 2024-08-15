using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Data;
using App.Main.Datastores;

namespace App.Main.UseCases
{
    public class PlayerMove : MonoBehaviour
    {
        Rigidbody2D rb;
        public float speed;

        void Start()
        {
            // シングルトンインスタンスから速度を取得
            speed = PlayerParameter.Instance.moveSpeed;

            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}