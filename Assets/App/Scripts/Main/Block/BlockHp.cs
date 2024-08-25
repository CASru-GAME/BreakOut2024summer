using System;

namespace App.Main.Block
{
    /// <summary>
    /// ゲーム内ブロックのヘルスポイント（HP）を管理する
    /// </summary>
    /// <remarks>
    /// このHpクラスは、エンティティの最大HP（_maxValue）と現在のHP（_currentValue）を保持する
    /// MaxValueプロパティとCurrentValueプロパティを通じて、これらの値に安全にアクセスできる
    /// </remarks>
    /// <exception cref="ArgumentException">CurrentValueが0未満の場合、またはCurrentValueがMaxValueを超える場合に発生します。</exception>

    public class BlockHp
    {
        private readonly int _currentValue;
        /// <summary>
        /// 現在のHp
        /// </summary>
        public int CurrentValue => _currentValue;
        private readonly int _maxValue;
        /// <summary>
        /// 最大Hp
        /// </summary>
        public int MaxValue => _maxValue;

        /// <summary>
        /// このクラス内のみで使用するコンストラクタ。最大HPと現在のHPを指定してHPを初期化する
        /// </summary>
        /// <param name="CurrentValue">設定する現在のHPのint型の値</param>
        /// <param name="MaxValue">設定するHPのint型の値</param>
        /// <exception cref="ArgumentException"></exception>
        private BlockHp(int CurrentValue, int MaxValue)
        {
            // 0より小さい時には例外を発生させる
            if (CurrentValue < 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            // MaxValueを超える時には例外を発生させる
            else if (CurrentValue > MaxValue)
            {
                throw new ArgumentException("Value cannot over MaxValue");
            }
            this._currentValue = CurrentValue;
            this._maxValue = MaxValue;
        }

        /// <summary>
        /// 入力された数値を最大HP・現在のHPとしてHPの初期化を行う
        /// </summary>
        /// <param name="value">最大HP・現在のHPのint型の値</param>
        public BlockHp(int value) : this(value, value) { }

        /// <summary>
        /// 現在のHPの値を増加する
        /// </summary>
        /// <param name="value">追加する現在のHPのインスタンス</param>
        /// <returns>現在のHPを追加した新しいHpインスタンス</returns>
        public BlockHp AddCurrentValue(BlockHp value)
        {
            if (this._currentValue + value.CurrentValue > this._maxValue)
            {
                return new BlockHp(this._maxValue, this._maxValue);
            }
            else
            {
                return new BlockHp(this._currentValue + value.CurrentValue, this._maxValue);
            }
        }

        /// <summary>
        /// 最大HPの値を増加する
        /// </summary>
        /// <param name="value">追加する最大HPのインスタンス</param>
        /// <returns>最大HPを追加した新しいHpインスタンス</returns>
        public BlockHp AddMaxValue(BlockHp value)
        {
            return new BlockHp(this._currentValue, this._maxValue + value.CurrentValue);
        }

        /// <summary>
        /// 現在のHPの値を減少する
        /// </summary>
        /// <param name="value">減少する現在のHPのインスタンス</param>
        /// <returns>最大HPを追加した新しいHpインスタンス</returns>
        public BlockHp SubtractCurrentValue(BlockHp value)
        {
            if (this._currentValue - value.CurrentValue < 0)
            {
                return new BlockHp(0, this._maxValue);
            }
            else
            {
                return new BlockHp(this._currentValue - value.CurrentValue, this._maxValue);
            }
        }

        /// <summary>
        /// 現在のHPと最大HPをログに表示する、デバッグ用メソッド
        /// </summary>
        /// <param name="message">ログに表示させたい文章</param>
        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, CurrentValue : {this._currentValue}, MaxValue : {this._maxValue}, ");
        }
    }
}
