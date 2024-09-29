using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Strength : IPerk
    {
        private int StackCount = 0;
        private int CurrentStackCount = 0;
        private PlayerDatastore playerDatastore;

        public Strength(PlayerDatastore playerDatastore)
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
            playerDatastore.SubtractAttackPoint(CalculateValue(CurrentStackCount));
            playerDatastore.AddAttackPoint(CalculateValue(StackCount));
            CurrentStackCount = StackCount;
            Debug.Log("Park: increase attack point x " + StackCount);
        }

        private int CalculateValue(int value)
        {
            return 2+3*value;
        }
    }
}