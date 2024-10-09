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
        private PlayerDatastore playerDatastore;
        private StageSystem stageSystem;
        private Rigidbody2D rb;
        private float BallSpeed;

        /// <summary>
        /// デバッグ用．ボールに初速度を与える
        /// </summary>
        private void Start()
        {
            BallSpeed = playerDatastore.GetBallSpeedValue();
            rb = GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().velocity = new Vector2(BallSpeed, BallSpeed);
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Initialize(PlayerDatastore playerDatastore, StageSystem stageSystem)
        {
            this.playerDatastore = playerDatastore;
            this.stageSystem = stageSystem;
        }

        private void FixedUpdate()
        {
            //ボールが画面外に出たら自分自身を削除
            if (transform.position.y < _minY)
            {
                Suicide();
            }

            //速度を一定に保つ
            rb.velocity = rb.velocity.normalized * playerDatastore.GetBallSpeedValue();
        }

        /// <summary>
        /// ボール数を1つ減らし，ゲームオブジェクト(ボール自身)を削除する
        /// </summary>
        private void Suicide()
        {
            //ボール数を1引く
            stageSystem.DecreaseBallCountonStage();

            //gameObjectを削除
            Destroy(gameObject);
        }

        /// <summary>
        /// 他のオブジェクトに接触した瞬間に実行する
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            IBlock block = collision2D.gameObject.GetComponent<IBlock>();
            if(block != null)
            {
                //ダメージ計算
                block.TakeDamage(CalcDamage());
                //IBlockをいじる許可が出たらここに状態異常付与の関数を書く
            }
        }

        /// <summary>
        /// ブロックに与えるダメージを計算する
        /// </summary>
        private int CalcDamage()
        {
            int damage = playerDatastore.GetAttackPointValue();
            damage += CalculateComboDamage();
            CaluculatePerkDamage(damage);
            return damage;
        }

        private int CalculateComboDamage()
        {
            return (int)(playerDatastore.GetComboCount()*0.25);
        }

        private int CaluculatePerkDamage(int damage)
        {
            damage += playerDatastore.PerkSystem.PerkList.AllPerkList[3].IntEffect();
            damage *= playerDatastore.PerkSystem.PerkList.AllPerkList[4].IntEffect();
            return damage;
        }

        private int CalculatePoisonStack()
        {
            return playerDatastore.PerkSystem.PerkList.AllPerkList[5].IntEffect();
        }

        private int CalculateWeaknessStack()
        {
            return playerDatastore.PerkSystem.PerkList.AllPerkList[6].IntEffect();
        }
    }
}
