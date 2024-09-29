using UnityEngine;
using App.Main.Player.Perk;

namespace App.Main.Player
{
    public class PlayerDatastore : MonoBehaviour
    {
        public Parameter Parameter { get; private set; }
        private LevelSystem levelSystem;
        private PerkSystem PerkSystem;
        [SerializeField] GameObject perkPanelPrefab;

        /// <summary>
        /// プレイヤーの初期化
        /// </summary>
        public void InitializePlayer()
        {
            Parameter = new Parameter(3, 1, 5.0f, 5.0f, 1, 0);  //Parameter(int live, int attackPoint, float ballSpeed, float moveSpeed, int level , int experiencePoint)のコンストラクタを呼び出す
            levelSystem = new LevelSystem(this);
            PerkSystem = new PerkSystem(this, perkPanelPrefab); 
        }

        public void ChoosePerk()
        {
            PerkSystem.ChoosePerk();
        }

        /// <summary>
        /// 現在の残機を追加する
        /// </summary>
        /// <param name="value"></param>
        public void AddLive(int value)
        {
            Parameter.AddLive(value);
        }

        /// <summary>
        /// 現在の残機を減らす
        /// </summary>
        /// <param name="value"></param>
        public void SubtractLive(int value)
        {
            Parameter.SubtractLive(value);
        }

        /// <summary>
        /// 現在の残機が指定した値と等しいかどうかを返す
        /// </summary>
        /// <param name="value"></param>
        public bool IsLiveValue(int value)
        {
            return Parameter.IsLiveValue(value);
        }

        /// <summary>
        /// 最大残機を追加する
        /// </summary>
        /// <param name="value"></param>
        public void AddMaxLive(int value)
        {
            Parameter.AddMaxLive(value);
        }

        /// <summary>
        /// 最大残機を減らす
        /// </summary>
        /// <param name="value">減らす値</param>
        public void SubtractMaxLive(int value)
        {
            Parameter.SubtractMaxLive(value);
        }

        /// <summary>
        /// 現在の残機を返す
        /// </summary>
        /// <returns></returns>

        public int GetLiveValue()
        {
            return Parameter.GetLiveValue();
        }

        /// <summary>
        /// 攻撃力を追加する
        /// </summary>
        /// <param name="value"></param>

        public void AddAttackPoint(int value)
        {
            Parameter.AddAttackPoint(value);
        }

        /// <summary>
        /// 攻撃力を減らす
        /// </summary>
        /// <param name="value"></param>

        public void SubtractAttackPoint(int value)
        {
            Parameter.SubtractAttackPoint(value);
        }

        /// <summary>
        /// 攻撃力を乗算する
        /// </summary>
        /// <param name="value"></param>

        public void MultiplyAttackPoint(double value)
        {
            Parameter.MultiplyAttackPoint(value);
        }

        /// <summary>
        /// 攻撃力を除算する
        /// </summary>
        /// <param name="value"></param>

        public void DivideAttackPoint(double value)
        {
            Parameter.DivideAttackPoint(value);
        }

        /// <summary>
        /// 攻撃力を返す
        /// </summary>
        /// <returns></returns>

        public int GetAttackPointValue()
        {
            return Parameter.GetAttackPointValue();
        }

        /// <summary>
        /// ボールの速度を追加する
        /// </summary>
        /// <param name="value"></param>
        
        public void AddBallSpeed(float value)
        {
            Parameter.AddBallSpeed(value);
        }

        /// <summary>
        /// ボールの速度を減らす
        /// </summary>
        /// <param name="value"></param>

        public void SubtractBallSpeed(float value)
        {
            Parameter.SubtractBallSpeed(value);
        }

        /// <summary>
        /// ボールの速度を返す
        /// </summary>
        /// <returns></returns>

        public float GetBallSpeedValue()
        {
            return Parameter.GetBallSpeedValue();
        }

        /// <summary>
        /// 経験値を追加する＆レベルを更新する
        /// </summary>
        /// <param name="value"></param>

        public void AddExperiencePoint(int value)
        {
            Parameter.AddExperiencePoint(value);
            levelSystem.ReloadLevel();
        }

        /// <summary>
        /// トータルの経験値を返す
        /// </summary>
        public int GetExperiencePointValue()
        {
            return Parameter.GetExperiencePointValue();
        }

        /// <summary>
        /// レベルを更新する
        /// </summary>
        /// <param name="value"></param>

        public void ReplaceLevel(int value)
        {
            Parameter.ReplaceLevel(value);
        }

        /// <summary>
        /// レベルを返す
        /// </summary>
        /// <returns></returns>

        public int GetLevelValue()
        {
            return Parameter.GetLevelValue();
        }

        /// <summary>
        /// 移動速度を追加する
        /// </summary>
        /// <param name="value"></param>

        public void AddMoveSpeed(float value)
        {
            Parameter.AddMoveSpeed(value);
        }

        /// <summary>
        /// 移動速度を減らす
        /// </summary>
        /// <param name="value"></param>

        public void SubtractMoveSpeed(float value)
        {
            Parameter.SubtractMoveSpeed(value);
        }

        /// <summary>
        /// 移動速度を返す
        /// </summary>
        /// <returns></returns>

        public float GetMoveSpeedValue()
        {
            return Parameter.GetMoveSpeedValue();
        }


        /// <summary>
        /// レベルアップに必要な経験値
        /// </summary>
        /// <param name="level"></param>
        public int NeedExp(int level)
        {
            return levelSystem.NeedExp(level);
        }

        /// <summary>
        /// 現在の経験値
        /// </summary>
        /// <param name="exp"></param>
        public int CurrentExperiencePoint(int exp)
        {
            return levelSystem.CurrentExperiencePoint(exp);
        }

    }
}