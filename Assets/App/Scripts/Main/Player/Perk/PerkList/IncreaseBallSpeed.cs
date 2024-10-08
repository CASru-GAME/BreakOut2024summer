using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class IncreaseBallSpeed : IPerk
    {
        private int id = 2;
        private int StackCount = 0;
        private int CurrentStackCount = 0;
        private PlayerDatastore playerDatastore;

        public IncreaseBallSpeed(PlayerDatastore playerDatastore)
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
            playerDatastore.AddBallSpeed(CalculateValue(CurrentStackCount));//前回の効果を取り消す
            playerDatastore.SubtractBallSpeed(CalculateValue(StackCount));
            CurrentStackCount = StackCount;
            Debug.Log("Park: increase move speed x " + StackCount);
        }

        private float CalculateValue(int value)
        {
            return 0.5f+0.5f*(float)value;
        }

        public int GetId()
        {
            return id;
        }

        public int IntEffect()
        {
            return 0;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}