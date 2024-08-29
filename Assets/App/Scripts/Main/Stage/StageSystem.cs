namespace App.Main.Stage{
    public class StageSystem{
        private readonly int _ballCountonStage  = 0;
        public int BallCountonStage => _ballCountonStage;
        ///<summary>
        ///ステージシステム上のボールの数を一つ増やす。
        ///</summary>
        public IncreaseBallCountonStage(){
            ++_ballCountonStage;
        }
        ///<summary>
        ///ステージシステム上のボールの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ボールの数が0未満になる場合に発生します。</exception>
        public DecreaseBallCountonStage(){
            if(_ballCountonStage <= 0){
                throw new ArgumentException("Value cannot be negative");
            }
            --_ballCountonStage;
        }
    }
}