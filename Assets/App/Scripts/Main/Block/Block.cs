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
        public StageSystem StageSystem { get; set; }
        private int PoisonStack = 0;
        private int WeaknessPoint = 0;
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

        private void FixedUpdate() {
            
            if (PoisonStack > 0 && isPoisoned == false) {
                StartCoroutine(TakePoisonDamage());
                isPoisoned = true;
            }
            StartCoroutine(RemoveWeaknessPoint());
        }



        //<summary>
        // ブロックが破壊されたときに通達するために取得する
        //</summary>
        public void SetStage(StageSystem stageSystem)
        {
            this.StageSystem = stageSystem;
        }

        //<summary>
        // ダメージを受ける(ボールが呼び出す)
        //</summary>
        public void TakeDamage(int damage)
        {
            if(WeaknessPoint > 0) damage *= 2;
            BlockHp newBlockHp = new BlockHp(damage);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            blockAnimation.CreateDamageEffect(damage, StageSystem);

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
            StageSystem.DecreaseNormalBlockCount();

            blockAnimation.Break();

            StageSystem.CreateItem(transform.position); //デバッグ用
            StageSystem.CreateExpBall(transform.position); //デバッグ用
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[3].IntEffect() == 1)
            {
                StageSystem.IncreaseTotalCat();
                createCat.Create(transform.position, transform.localScale);
            }
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[13].IntEffect() == 1)
            {
                StageSystem.CreateBall(transform.position);
            }
            Destroy(gameObject);
        }

        public IEnumerator TakePoisonDamage()
        {
            TakeDamage(PoisonStack);
            RemovePoisonStack();
            yield return new WaitForSeconds(1);
            isPoisoned = false;
        }

        public void AddPoisonStack(int stack)
        {
            PoisonStack += stack;
            if(playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) PoisonStack += stack;
        }

        public void RemovePoisonStack()
        {
            PoisonStack--;
        }

        public void AddWeaknessPoint(int point)
        {
            WeaknessPoint += point;
            if(playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) WeaknessPoint += point;
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