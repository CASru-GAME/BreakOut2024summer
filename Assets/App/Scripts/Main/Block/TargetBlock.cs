using UnityEngine;
using App.Main.Player;

namespace App.Main.Block
{   //ターゲットのブロック
    public class TargetBlock : MonoBehaviour, IBlock
    {   
        private BlockDataStore blockDatastore;
        [SerializeField] int initialHp;
        private GameObject stage;

        void Start()
        {
            blockDatastore = GetComponent<BlockDataStore>();
            blockDatastore.InitializeBlock(initialHp);
        }

        //<summary>
        // ブロックが破壊されたときに通達するために取得する
        //</summary>
        public void SetStage(GameObject stage)
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

            if(blockDatastore.Hp.CurrentValue < 0)
            Break();
        }

        //<summary>
        // 破壊されたことを通達する(TakeDamage内で呼び出される)
        //</summary>
        private void Break()
        {   
            //ステージのゲームクリアやゲームオーバー判定を持つクラスに自身が破壊されたことを通達(未実装)

            Destroy(gameObject);
        }
    }  
}
