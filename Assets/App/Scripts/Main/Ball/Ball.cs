using UnityEngine;
using App.Main.Block;
using App.Main.Player;
using App.Main.Stage;
using Unity.Mathematics;

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
        [SerializeField] private GameObject invisibleBall_forYellowSubmarine;
        private bool isFireworks = false;

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

            if(playerDatastore.PerkSystem.PerkList.AllPerkList[19].IntEffect() == 1 && isFireworks == false)
            {
                isFireworks = true;
                StartCoroutine(Burning());
            }
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

        private IEnumerator Burning()
        {
            float lifeTime = playerDatastore.PerkSystem.PerkList.AllPerkList[19].FloatEffect() * 30;
            while(lifeTime > 0)
            {
                yield return new WaitForSeconds(0.1f);
                lifeTime -= 0.1f;
            }
            Suicide();
        }

        /// <summary>
        /// 他のオブジェクトに接触した瞬間に実行する
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            IBlock block = collision2D.gameObject.GetComponent<IBlock>();
            if (block != null)
            {
                //ダメージ計算
                block.TakeDamage(CalcDamage());
                playerDatastore.AddComboCount();
                //IBlockをいじる許可が出たらここに状態異常付与の関数を書く

                //黄色い潜水艦の効果
                //透明な丸を作り、ブロックとの衝突判定を取得し、ブロックのtakeDamageを呼び出す。
                //透明な丸は処理が終わると消える
                if (playerDatastore.PerkSystem.PerkList.AllPerkList[14].FloatEffect() == 1)
                {
                    GameObject collision_detector = Instantiate(invisibleBall_forYellowSubmarine, collision2D.transform.position, quaternion.identity);
                    collision_detector.GetComponent<CollisionDetector_forYellowSubmarine>().SetDamage(playerDatastore.PerkSystem.PerkList.AllPerkList[14].GetStackCount(), CalcDamage());
                }
            }
        }

        /// <summary>
        /// ブロックに与えるダメージを計算する
        /// </summary>
        private int CalcDamage()
        {
            int damage = playerDatastore.GetAttackPointValue();
            damage += (int)(playerDatastore.PerkSystem.PerkList.AllPerkList[19].FloatEffect() * 10);
            damage += playerDatastore.PerkSystem.PerkList.AllPerkList[12].IntEffect();

            damage += CalculateComboDamage();
            CaluculatePerkDamage(damage);
            return damage;
        }

        private int CalculateComboDamage()
        {
            return (int)(playerDatastore.GetComboCount() * 0.25);
        }

        private int CaluculatePerkDamage(int damage)
        {
            damage += playerDatastore.PerkSystem.PerkList.AllPerkList[2].IntEffect();
            damage += playerDatastore.PerkSystem.PerkList.AllPerkList[1].IntEffect();
            damage *= playerDatastore.PerkSystem.PerkList.AllPerkList[22].IntEffect();
            return damage;
        }

        private int CalculatePoisonStack()
        {
            return playerDatastore.PerkSystem.PerkList.AllPerkList[6].IntEffect();
        }

        private int CalculateWeaknessStack()
        {
            return playerDatastore.PerkSystem.PerkList.AllPerkList[6].IntEffect();
        }
    }
}
