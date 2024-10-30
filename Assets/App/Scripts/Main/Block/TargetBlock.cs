using UnityEngine;
using System.Collections;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Effects;
using App.Main.Cat;
using App.Common.Audio;

namespace App.Main.Block
{   //ターゲットのブロック
    public class TargetBlock : MonoBehaviour, IBlock
    {
        private BlockDatastore blockDatastore;
        private BlockAnimation blockAnimation;
        private CreateCat createCat;
        private PlayerDatastore playerDatastore;
        [SerializeField] int[] initialHp;//ワールドごとに個別に設定する
        [SerializeField] int Id;
        public StageSystem StageSystem { get; set; }
        private float WaitTime = 1.0f;
        public float PoisonStack { get; set; }
        public int WeaknessPoint { get; set; }
        private bool isPoisoned = false;
        private bool isBroke = false;
        [SerializeField] private WholeSECollector _wholeSeCollector;
        [SerializeField] private CreateDebuffEffect _createDebuffEffect;
        private bool isPoisonEffect = false;
        private bool isWeaknessEffect = false;
        void Start()
        {
            blockAnimation = GetComponent<BlockAnimation>();
            createCat = GetComponent<CreateCat>();
            playerDatastore = FindObjectOfType<PlayerDatastore>();
            //　findしたくないので、引数で渡したい
            _createDebuffEffect.initialize(this);
            PoisonStack = 0;
            WeaknessPoint = 0;
        }

        private void FixedUpdate()
        {
            if (PoisonStack > 0 && isPoisoned == false)
            {
                StartCoroutine(TakePoisonDamage());
                isPoisoned = true;
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
            blockDatastore.InitializeBlock(initialHp[stageSystem.CurrentWorldNumberID - 1]);
        }

        //<summary>
        // ダメージを受ける(ボールが呼び出す)
        //</summary>
        public void TakeDamage(int damage)
        {
            _wholeSeCollector.PlaySE(2);
            if (WeaknessPoint > 0) damage *= 2;
            BlockHp newBlockHp = new BlockHp(damage);
            blockDatastore.SetHp(blockDatastore.Hp.SubtractCurrentValue(newBlockHp));

            blockAnimation.CreateDamageEffect(damage, StageSystem);

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
            _wholeSeCollector.PlaySE(1);
            PoisonStack = 0;
            WeaknessPoint = 0;
            if (isBroke) return;
            //ワイヤーケージのパークがある場合、猫が増えずにブロック破壊の処理を行う。
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[1].FloatEffect() == 1)
            {
                StageSystem.DecreaseTargetBlockCountWithoutIncreaseTotalCat();
            }
            else
            {
                //ステージのゲームクリアやゲームオーバー判定を持つクラスに自身が破壊されたことを通達
                StageSystem.DecreaseTargetBlockCount();

                blockAnimation.Break();
                createCat.Create(transform.position, transform.localScale);

                if (playerDatastore.PerkSystem.PerkList.AllPerkList[13].IntEffect() == 1)
                {
                    StageSystem.CreateBall(transform.position);
                }
                if (playerDatastore.PerkSystem.PerkList.AllPerkList[21].IntEffect() == 1)
                {
                    StageSystem.IncreaseTotalCat();
                    createCat.Create(transform.position + new Vector3(0f, 0.3f, 0f), transform.localScale);
                }
            }
            isBroke = true;
            _createDebuffEffect.DestroyEffect();
            Destroy(gameObject);
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
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) PoisonStack += (float)stack;
            if (isPoisonEffect == false && PoisonStack > 0)
            {
                _createDebuffEffect.CreatePoisonEffect(transform.position);
                isPoisonEffect = true;
            }
        }

        public void RemovePoisonStack()
        {
            PoisonStack -= (float)1 / playerDatastore.PerkSystem.PerkList.AllPerkList[15].IntEffect();
            if (PoisonStack < 0) PoisonStack = 0;
        }

        public void AddWeaknessPoint(int point)
        {
            WeaknessPoint += point;
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[20].IntEffect() == 1) WeaknessPoint += point;
            if (isWeaknessEffect == false && WeaknessPoint > 0)
            {
                _createDebuffEffect.CreateWeaknessEffect(transform.position);
                isWeaknessEffect = true;
            }
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
    }
}
