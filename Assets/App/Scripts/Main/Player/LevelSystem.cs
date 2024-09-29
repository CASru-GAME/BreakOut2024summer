namespace App.Main.Player
{
    public class LevelSystem
    {
        PlayerDatastore playerDatastore;

        public LevelSystem(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
        }

        /// <summary>
        /// レベルアップに必要な経験値
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int NeedExp(int level)
        {
            //次のレベルに上がるのに必要な経験値
            return (int)(10*System.Math.Pow(1.1,(level-1)));
        }

        /// <summary>
        /// 現在の経験値
        /// </summary>
        /// <param name="exp"></param>
        public int CurrentExperiencePoint(int exp)
        {
            int level = 1;
            while(exp >= NeedExp(level))
            {
                exp -= NeedExp(level);
                level++;
            }
            return exp;
        }




        private int CalculateLevel(int exp)
        {
            //経験値から現在のレベルを計算
            int level = 1;
            while(exp >= NeedExp(level))
            {
                exp -= NeedExp(level);
                level++;
            }
            return level;
        }

        private void LevelUp()
        {
            playerDatastore.ReplaceLevel(playerDatastore.GetLevelValue() + 1);
            playerDatastore.ChoosePerk();
            // LevelUpEffect(); // 未実装
        }


        /// <summary>
        /// レベルの再計算
        /// </summary>
        /// <param name="exp"></param>
        public void ReloadLevel()
        {
            //総経験値から現在のレベル計算しそのレベルまでレベルアップする
            while(CalculateLevel(playerDatastore.GetExperiencePointValue()) > playerDatastore.GetLevelValue())
            {
                LevelUp();
            }
        }
    }
}