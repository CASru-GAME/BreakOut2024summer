using UnityEngine;
using System.Collections;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Item;
using App.Main.Block.Ablity;
using App.Main.Cat;
using App.Common.Audio;

namespace App.Main.Block
{   
    //ターゲット以外のブロック
    public class Block : MonoBehaviour, IBlock
    {   
        private BlockDatastore blockDatastore;
        private BlockAnimation blockAnimation;
        private CreateCat createCat;
        private PlayerDatastore playerDatastore;
        private float WaitTime = 1.0f;
        [SerializeField] int[] initialHp;//ワールドごとに個別に設定する
        [SerializeField] int Id;
        public StageSystem StageSystem { get; set; }
        public float PoisonStack { get; set; }
        public int WeaknessPoint { get; set; }
        private bool isPoisoned = false;
        [SerializeField] private WholeSECollector _wholeSeCollector;
        [SerializeField]private CreateDebuffEffect _createDebuffEffect;
        private bool isPoisonEffect = false;
        private bool isWeaknessEffect = false;

        void Start()
        {
            blockAnimation = GetComponent<BlockAnimation>();
            createCat = GetComponent<CreateCat>();
            playerDatastore = FindObjectOfType<PlayerDatastore>();
            //　findしたくないので、引数で渡したい
            PoisonStack = 0;
            WeaknessPoint = 0;
        }

        private void FixedUpdate() {
            
            if (PoisonStack > 0 && isPoisoned == false) {
                StartCoroutine(TakePoisonDamage());
                isPoisoned = true;
            }
            StartCoroutine(RemoveWeaknessPoint());

            if(isPoisonEffect == false && PoisonStack > 0)
            {
                _createDebuffEffect.CreatePoisonEffect(transform.position);
                isPoisonEffect = true;
                StartCoroutine(waitForPoisonEffect());
            }

            if(isWeaknessEffect == false && WeaknessPoint > 0)
            {
                _createDebuffEffect.CreateWeaknessEffect(transform.position);
                isWeaknessEffect = true;
                StartCoroutine(waitForWeaknessEffect());
            }
        }

        //<summary>
        //ブロックにSE用のコンポーネントを追加する
        //</summary>
        public void AddWholeSeCollector(WholeSECollector wholeSeCollector)
        {
            _wholeSeCollector = wholeSeCollector;
        }

        //<summary>
        // ブロックが破壊されたときに通達するために取得する
        //</summary>
        public void SetStage(StageSystem stageSystem)
        {   
            this.StageSystem = stageSystem;
            blockDatastore = GetComponent<BlockDatastore>();
            blockDatastore.InitializeBlock((int)((CaluculateRoopHP())*initialHp[stageSystem.CurrentWorldNumberID - 1]));
        }

        private float CaluculateRoopHP()
        {
            float value = 0;
            for(int i = 0; i < StageSystem.RoopCount; i++)
            {
                value += 2;
            }
            return value;
        }

        //<summary>
        // ダメージを受ける(ボールが呼び出す)
        //</summary>
        public void TakeDamage(int damage)
        {
            _wholeSeCollector.PlaySE(2);
            if(WeaknessPoint > 0) damage *= 2;
            BlockHp newBlockHp = new BlockHp(damage);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            blockAnimation.CreateDamageEffect(damage, StageSystem);

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
            StageSystem.DecreaseNormalBlockCount();
            PoisonStack = 0;
            WeaknessPoint = 0;

            blockAnimation.Break();

            if (playerDatastore.PerkSystem.PerkList.AllPerkList[12].GetStackCount() > 0)
            {
                playerDatastore.AddDumbbellPower(1);
            }

            StageSystem.CreateItem(transform.position); //デバッグ用
            StageSystem.CreateExpBall(transform.position); //デバッグ用
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[3].IntEffect() == 1)
            {
                StageSystem.IncreaseTotalCat();
                createCat.Create(transform.position, transform.localScale);
                if(playerDatastore.PerkSystem.PerkList.AllPerkList[21].IntEffect() == 1)
                {
                    StartCoroutine(CreateCat());
                }
            }
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[13].IntEffect() == 1)
            {
                StageSystem.CreateBall(transform.position);
            }
            Destroy(gameObject);
        }

        private IEnumerator CreateCat()
        {
            StageSystem.IncreaseTotalCat();
            yield return new WaitForSeconds(0.5f);
            createCat.Create(transform.position, transform.localScale);
        }

        public IEnumerator TakePoisonDamage()
        {
            TakeDamage((int)PoisonStack);
            RemovePoisonStack();
            yield return new WaitForSeconds(1);
            isPoisoned = false;
        }

        public void AddPoisonStack(int stack)
        {
            
            PoisonStack += (float)stack;
            if(playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) PoisonStack += (float)stack;
        }

        public void RemovePoisonStack()
        {
            PoisonStack -= (float)1/playerDatastore.PerkSystem.PerkList.AllPerkList[15].IntEffect();
            if(PoisonStack <= 0) 
            {
                PoisonStack = 0;
                isPoisonEffect = false;
            }
        }

        public void AddWeaknessPoint(int point)
        {
            
            WeaknessPoint += point;
            if(playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) WeaknessPoint += point;
        }

        public IEnumerator RemoveWeaknessPoint()
        {
            WaitTime = playerDatastore.PerkSystem.PerkList.AllPerkList[15].FloatEffect();
            if (WeaknessPoint > 0)
            {
                WeaknessPoint--;
            }
            if (WeaknessPoint <= 0)
            {
                WeaknessPoint = 0;
                isWeaknessEffect = false;
            }
            yield return new WaitForSeconds(WaitTime);
        }

        IEnumerator waitForPoisonEffect()
        {
            yield return new WaitForSeconds(0.42f);
            isPoisonEffect = false;
        }

        IEnumerator waitForWeaknessEffect()
        {
            yield return new WaitForSeconds(1.15f);
            isWeaknessEffect = false;
        }

    }
}