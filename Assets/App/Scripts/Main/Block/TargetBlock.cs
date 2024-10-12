using UnityEngine;
using System.Collections;
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
        private PlayerDatastore playerDatastore;
        [SerializeField] int initialHp;
        [SerializeField] int Id;
        private StageSystem stageSystem;
        private int PoisonStack = 0;
        private bool isPoisoned = false;

        void Start()
        {
            blockDatastore = GetComponent<BlockDatastore>();
            blockDatastore.InitializeBlock(initialHp);
            blockAnimation = GetComponent<BlockAnimation>();
            createCat = GetComponent<CreateCat>();
            playerDatastore = FindObjectOfType<PlayerDatastore>();
            //　findしたくないので、引数で渡したい
        }

        private void FixedUpdate()
        {
            if (PoisonStack > 0 && isPoisoned == false)
            {
                StartCoroutine(TakePoisonDamage(PoisonStack));
                isPoisoned = true;
            }

        }

        //<summary>
        // ブロックが破壊されたときに通達するために取得する
        //</summary>
        public void SetStage(StageSystem stageSystem)
        {
            this.stageSystem = stageSystem;
        }

        //<summary>
        // ダメージを受ける(ボールが呼び出す)
        //</summary>
        public void TakeDamage(int damage)
        {
            BlockHp newBlockHp = new BlockHp(damage);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            blockAnimation.CreateDamageEffect(damage, stageSystem);

            if (blockDatastore.Hp.CurrentValue <= 0)
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
            //ワイヤーケージのパークがある場合、猫が増えずにブロック破壊の処理を行う。
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[1].FloatEffect() == 1)
            {
                stageSystem.DecreaseTargetBlockCountWithoutIncreaseTotalCat();
            }
            else
            {
                //ステージのゲームクリアやゲームオーバー判定を持つクラスに自身が破壊されたことを通達
                stageSystem.DecreaseTargetBlockCount();

                blockAnimation.Break();
                createCat.Create(transform.position, transform.localScale);

                if (playerDatastore.PerkSystem.PerkList.AllPerkList[21].IntEffect() == 1)
                {
                    stageSystem.IncreaseTotalCat();
                    createCat.Create(transform.position + new Vector3(0f, 0.3f, 0f), transform.localScale);
                }
            }
            Destroy(gameObject);
        }

        public IEnumerator TakePoisonDamage(int poisonStack)
        {
            TakeDamage(poisonStack);
            RemovePoisonStack();
            yield return new WaitForSeconds(1);
            isPoisoned = false;
        }

        public void AddPoisonStack(int stack)
        {
            PoisonStack += stack;
        }

        public void RemovePoisonStack()
        {
            PoisonStack--;
        }
    }
}
