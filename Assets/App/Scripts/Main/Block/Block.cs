using UnityEngine;
using System.Collections;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Item;
using App.Main.Block.Ablity;
using App.Main.Cat;

namespace App.Main.Block
{   
    //ターゲット以外のブロック
    public class Block : MonoBehaviour, IBlock
    {   
        private BlockDatastore blockDatastore;
        private BlockAnimation blockAnimation;
        private CreateCat createCat;
        private PlayerDatastore playerDatastore;
        [SerializeField] int initialHp;
        [SerializeField] int Id;
        public StageSystem stage{ get; set; }
        private int PoisonStack = 0;
        private int WeaknessPoint = 0;

        void Start()
        {
            blockDatastore = GetComponent<BlockDatastore>();
            blockDatastore.InitializeBlock(initialHp);
            blockAnimation = GetComponent<BlockAnimation>();
            createCat = GetComponent<CreateCat>();
            playerDatastore = FindObjectOfType<PlayerDatastore>();
            //　findしたくないので、引数で渡したい
        }

        private void FixedUpdate() {
            
            StartCoroutine(TakePoisonDamage());
            StartCoroutine(RemoveWeaknessPoint());
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
        public void TakeDamage(int damage)
        {
            BlockHp newBlockHp = new BlockHp(damage);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            if(WeaknessPoint > 0) damage *= 2;

            blockAnimation.CreateDamageEffect(damage, stage);

            if (blockDatastore.Hp.CurrentValue <= 0) Break();
        }

        public void Healed(int healAmount)
        {  
            Debug.Log("Heal");
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

            blockAnimation.Break();

            stage.CreateItem(transform.position); //デバッグ用
            stage.CreateExpBall(transform.position); //デバッグ用
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[3].IntEffect() == 1)
            {
                createCat.Create(transform.position, transform.localScale);
            }
            Destroy(gameObject);
        }

        public IEnumerator TakePoisonDamage()
        {
            if (PoisonStack > 0) {
                TakeDamage(PoisonStack);
                RemovePoisonStack();
                yield return new WaitForSeconds(1);
            }
        }

        public void AddPoisonStack(int stack)
        {
            PoisonStack += stack;
        }

        public void RemovePoisonStack()
        {
            PoisonStack--;
        }

        public IEnumerator RemoveWeaknessPoint()
        {
            if (WeaknessPoint > 0)
            {
                WeaknessPoint--;
            }
            yield return new WaitForSeconds(1);
        }
    }
}