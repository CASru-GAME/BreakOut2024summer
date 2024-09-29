using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class IncreaseMoveSpeed : IPerk
    {
        private int StackCount = 0;
        private int CurrentStackCount = 0;
        private PlayerDatastore playerDatastore;

        public IncreaseMoveSpeed(PlayerDatastore playerDatastore)
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
            playerDatastore.SubtractMoveSpeed(CalculateValue(CurrentStackCount));//前回の効果を取り消す
            playerDatastore.AddMoveSpeed(CalculateValue(StackCount));
            CurrentStackCount = StackCount;
        }

        private float CalculateValue(int value)
        {
            return 0.5f+0.5f*(float)value;
        }
    }
}