using UnityEngine;

namespace App.Main.Player
{
    public class PlayerDatastore : MonoBehaviour
    {
        public Parameter Parameter { get; private set; }

        /// <summary>
        /// プレイヤーの初期化
        /// </summary>
        public void InitializePlayer()
        {
            Parameter = new Parameter(3, 1, 5.0f, 1, 0);  //Parameter(int live,int attackPoint, float moveSpeed, int level , int experiencePoint)のコンストラクタを呼び出す
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

        public int LiveValue()
        {
            return Parameter.LiveValue();
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

        public int AttackPointValue()
        {
            return Parameter.AttackPointValue();
        }

        /// <summary>
        /// 経験値を追加する
        /// </summary>
        /// <param name="value"></param>

        public void AddExperiencePoint(int value)
        {
            Parameter.AddExperiencePoint(value);
        }

        /// <summary>
        /// 経験値を返す
        /// </summary>
        public int ExperiencePointValue()
        {
            return Parameter.ExperiencePointValue();
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

        public int LevelValue()
        {
            return Parameter.LevelValue();
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

        public float MoveSpeedValue()
        {
            return Parameter.MoveSpeedValue();
        }

    }
}