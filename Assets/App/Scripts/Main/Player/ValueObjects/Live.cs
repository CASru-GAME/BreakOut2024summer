using System;

namespace App.Main.Player
{
    /// <summary>
    /// プレイヤーの残機を管理する
    /// </summary>
    /// <remarks>
    /// このLiveクラスは、最大残機（_maxValue）と現在の残機（_currentValue）を保持する
    /// MaxValueプロパティとCurrentValueプロパティを通じて、これらの値に安全にアクセスできる
    /// </remarks>
    /// <exception cref="ArgumentException">CurrentValueが0未満の場合、またはCurrentValueがMaxValueを超える場合に発生します。</exception>
    
    public class Live
    {
        private readonly int _currentValue;
        /// <summary>
        /// 現在の残機
        /// </summary>
        public int CurrentValue => _currentValue;
        private readonly int _maxValue;
        /// <summary>
        /// 最大残機
        /// </summary>
        public int MaxValue => _maxValue;

        /// <summary>
        /// このクラス内のみで使用するコンストラクタ。最大残機と現在の残機を指定して残機を初期化する
        /// </summary>
        /// <param name="CurrentValue">設定する現在の残機のint型の値</param>
        /// <param name="MaxValue">設定する最大残機のint型の値</param>
        /// <exception cref="ArgumentException"></exception>
        private Live(int CurrentValue, int MaxValue)
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
        /// 入力された数値を最大残機・現在の残機として残機の初期化を行う
        /// </summary>
        /// <param name="value">最大残機・現在の残機のint型の値</param>
        public Live(int value) : this(value, value) { }

        /// <summary>
        /// 現在の残機の値を増加する
        /// </summary>
        /// <param name="value">追加する現在の残機のインスタンス</param>
        /// <returns>現在の残機を追加した新しい残機インスタンス</returns>
        public Live AddCurrentValue(Live value)
        {
            if (this._currentValue + value.CurrentValue > this._maxValue)
            {
                return new Live(this._maxValue, this._maxValue);
            }
            else
            {
                return new Live(this._currentValue + value.CurrentValue, this._maxValue);
            }
        }

        /// <summary>
        /// 最大残機の値を増加する
        /// </summary>
        /// <param name="value">追加する最大残機のインスタンス</param>
        /// <returns>最大残機を追加した新しい残機インスタンス</returns>
        public Live AddMaxValue(Live value)
        {
            return new Live(this._currentValue, this._maxValue + value.CurrentValue);
        }

        /// <summary>
        /// 現在の残機の値を減少する
        /// </summary>
        /// <param name="value">減少する現在の残機のインスタンス</param>
        /// <returns>最大残機を追加した新しい残機インスタンス</returns>
        public Live SubtractCurrentValue(Live value)
        {
            if (this._currentValue - value.CurrentValue < 0)
            {
                return new Live(0, this._maxValue);
            }
            else
            {
                return new Live(this._currentValue - value.CurrentValue, this._maxValue);
            }
        }

        /// <summary>
        /// 現在の残機と最大残機をログに表示する、デバッグ用メソッド
        /// </summary>
        /// <param name="message">ログに表示させたい文章</param>
        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, CurrentValue : {this._currentValue}, MaxValue : {this._maxValue}, ");
        }
    }
}