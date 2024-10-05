using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class LastStand : IPerk
    {
        private int Id = 4;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public LastStand(PlayerDatastore playerDatastore)
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
            while(playerDatastore.GetMaxLiveValue() != 1)
            {
                playerDatastore.SubtractMaxLive(1);
            }
        }

        private int CalculateValue(int value)
        {
            return 1+value;
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