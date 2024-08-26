using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Block;
using App.Main.Player;

namespace App.Main.Ball
{
    public class Ball : MonoBehaviour
    {
        //ボールを削除するY座標の限界値
        private readonly float _minY = -5f;
        private GameObject player;
        private GameObject stage;

        /// <summary>
        /// デバッグ用．ボールに初速度を与える
        /// </summary>
        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, -2f);
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Initialize(GameObject player, GameObject stage)
        {
            this.player = player;
            this.stage = stage;
        }

        private void FixedUpdate()
        {
            //ボールが画面外に出たら自分自身を削除
            if (transform.position.y < _minY)
            {
                Suicide();
            }
        }

        /// <summary>
        /// ボール数を1つ減らし，ゲームオブジェクト(ボール自身)を削除する
        /// </summary>
        private void Suicide()
        {
            //ボール数を1引く(実装待ち)

            //gameObjectを削除
            Destroy(gameObject);
        }

        /// <summary>
        /// 他のオブジェクトに接触した瞬間に実行する
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            BlockHp blockHp = collider2D.GetComponent<BlockHp>();
            if(blockHp != null)
            {
                AttackPoint damage = CalcDamage();
                //blockHpのHPを減らす(実装待ち)
            }
        }

        /// <summary>
        /// ブロックに与えるダメージを計算する
        /// </summary>
        private AttackPoint CalcDamage()
        {
            AttackPoint newAttackPoint = new AttackPoint(0);
            newAttackPoint = player.GetComponent<PlayerParameter>().AttackPoint;
            //パークのダメージも計算する(実装待ち)

            return newAttackPoint;
        }
    }
}
