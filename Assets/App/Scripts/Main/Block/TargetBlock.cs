using UnityEngine;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Effects;
using App.Main.Cat;

namespace App.Main.Block
{   //ターゲットのブロック
    public class TargetBlock : MonoBehaviour, IBlock
    {   
        private BlockDatastore blockDatastore;
        private BlockAnimation blockAnimation;
        private CreateCat createCat;
        [SerializeField] int initialHp;
        [SerializeField] int Id;
        private StageSystem stage;

        void Start()
        {
            blockDatastore = GetComponent<BlockDatastore>();
            blockDatastore.InitializeBlock(initialHp);
            blockAnimation = GetComponent<BlockAnimation>();
            createCat = GetComponent<CreateCat>();
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

            blockAnimation.CreateDamageEffect(damage.CurrentValue - blockDatastore.Hp.CurrentValue, stage);

            if(blockDatastore.Hp.CurrentValue <= 0)
            Break();
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
            stage.DecreaseTargetBlockCount();

            blockAnimation.Break();
            createCat.Create(transform.position, transform.localScale);

            Destroy(gameObject);
        }
    }  
}
