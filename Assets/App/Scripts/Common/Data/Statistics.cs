using System.Collections.Generic;

namespace App.Common.Data
{
    public class TopValueData
    {
        /// 最大レベル
        public static int _maxPlayerLevel { get; private set; } = -1;
        /// 最大クリアステージ数
        public static int _maxClearedStage { get; private set; } = -1;
        /// 最大救出猫数
        public static int _maxCat { get; private set; } = -1;
    }

    public class StatisticsData
    {
        ///プレイした日付
        public string _playedDate { get; private set; } = "";
        /// クリアしたステージ数
        public int _totalClearedStage { get; private set; } = 0;
        /// 助けた猫の数
        public int _totalCat { get; private set; } = 0;
        /// 集めたパーク
        public List<(int id, int stackCount)> _totalAquiredPerkList { get; private set; } = new List<(int id, int stackCount)>();
    }
}
