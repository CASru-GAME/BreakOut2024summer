using UnityEngine;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Item;
using App.Main.Block.Ablity;

namespace App.Main.Block
{   //ターゲット以外のブロック
    public class Block : MonoBehaviour, IBlock
    {   
        private BlockDataStore blockDatastore;
        [SerializeField] int initialHp;
        [SerializeField] int Id;
        private StageSystem stage;

        void Start()
        {
            blockDatastore = GetComponent<BlockDataStore>();
            blockDatastore.InitializeBlock(initialHp);
        }

        void Update()
        {
           
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

            if (blockDatastore.Hp.CurrentValue <= 0) Break();
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
