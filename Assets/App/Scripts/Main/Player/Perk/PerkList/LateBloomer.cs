using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class LateBloomer : IPerk
    {
        private int Id = 3;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public LateBloomer(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
        }
        public void AddStackCount()
        {
            StackCount++;
        }

        public int GetStackCount()
        {
            return StackCount;
        }

        public void Effect()
        {
            if(StackCount == 0) return;
            CalculateValue(StackCount);
            // ステージが始まってからの時間を取得し適当な処理をする（未完）
            // BlockのtakeDamageのところにこの関数を含め、ダメージ増加系のパークを呼び出す処理を追加する
        }

        private int CalculateValue(int value)
        {
            return 1+(int)(0.2*value);
        }

        public int AttackEffect()
        {
            if(StackCount == 0) return 0;
            return CalculateValue(StackCount);
        }

        public int GetId()
        {
            return Id;
        }
    }
}