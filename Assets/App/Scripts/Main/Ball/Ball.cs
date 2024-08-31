using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Block;
using App.Main.Player;
using App.Main.Stage;

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
            //ボール数を1引く
            stage.GetComponent<StageSystem>().DecreaseBallCountonStage();

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
                //ダメージ計算
                AttackPoint damage = CalcDamage();
                //AttackPoint -> BlockHpに変換
                BlockHp BlockDamage = new BlockHp(damage.CurrentValue);
                //blockHpのHPを減らす
                blockHp.SubtractCurrentValue(BlockDamage);
            }
        }

        /// <summary>
        /// ブロックに与えるダメージを計算する
        /// </summary>
        private AttackPoint CalcDamage()
        {
            AttackPoint newAttackPoint = new AttackPoint(0);
            newAttackPoint = player.GetComponent<PlayerDatastore>().Parameter.AttackPoint;
            //パークのダメージも計算する(実装待ち)

            return newAttackPoint;
        }
    }
}
