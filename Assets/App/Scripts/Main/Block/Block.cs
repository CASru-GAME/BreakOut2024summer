using UnityEngine;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Item;
using App.Main.Block.Ablity;
using App.Main.Effects;

namespace App.Main.Block
{   //ターゲット以外のブロック
    public class Block : MonoBehaviour, IBlock
    {   
        private BlockDataStore blockDatastore;
        [SerializeField] int initialHp;
        [SerializeField] int Id;
        [SerializeField] GameObject DamageEffect;
        private StageSystem stage;

        void Start()
        {
            blockDatastore = GetComponent<BlockDataStore>();
            blockDatastore.InitializeBlock(initialHp);
        }

        //<summary>
        // ブロックが破壊されたときに通達するために取得する
        //</summary>
        public void SetStage(StageSystem stage)
        {
            this.stage = stage;
        }

        //<summary>
        // ダメージを受ける(ボールが呼び出す)
        //</summary>
        public void TakeDamage(AttackPoint damage)
        {
            BlockHp newBlockHp = new BlockHp(damage.CurrentValue);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            CreateDamageEffect(damage.CurrentValue - blockDatastore.Hp.CurrentValue);

            if (blockDatastore.Hp.CurrentValue <= 0) Break();
        }

        private void CreateDamageEffect(int damageValue)
        {
            var newDamageEffect = Instantiate(DamageEffect, transform.position, Quaternion.identity);
            newDamageEffect.GetComponent<DamageEffect>().Initialize(damageValue, stage.Canvas);
        }

        public void Healed(int healAmount)
        {   
            BlockHp newBlockHp = new BlockHp(healAmount);
            blockDatastore.SetHp(blockDatastore.Hp.AddCurrentValue(newBlockHp));
        }

        //<summary>
        // 破壊されたことを通達する(TakeDamage内で呼び出される)
        //</summary>
        private void Break()
        {
            //ステージのゲームクリアやゲームオーバー判定を持つクラスに自身が破壊されたことを通達
            stage.DecreaseNormalBlockCount();

            stage.CreateItem(transform.position);//デバッグ用
            stage.CreateExpBall(transform.position);//デバッグ用
            Destroy(gameObject);
        }
    }
}
