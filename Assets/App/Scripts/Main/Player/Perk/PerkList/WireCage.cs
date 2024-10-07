using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class WireCage : IPerk
    {
        private int id = 1;
        private int StackCount = 0;
        private int CurrentStackCount = 0;
        private PlayerDatastore playerDatastore;

        public WireCage(PlayerDatastore playerDatastore)
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
            if (StackCount == 0) return;
            //確率で猫が逃げ出す
            
        }

        private int CalculateProbability(int value)
        {
            return (1-1/(value+1))*5;
        }


        public int GetId()
        {
            return id;
        }

        public int AttackEffect()
        {
            if (StackCount == 0) return;
            return (int)((1-1/(value+1))*15);
        }
    }
}