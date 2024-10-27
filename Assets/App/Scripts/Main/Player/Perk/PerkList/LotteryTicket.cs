using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class LotteryTicket : IPerk
    {
        private int id = 9;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public LotteryTicket(PlayerDatastore playerDatastore)
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
            return;   
        }


        public int GetId()
        {
            return id;
        }

        public int IntEffect()
        {
            int probability = Random.Range(0, 100);
            if (probability < CalculateProbability())
            {
                return 1;
            }
            return 0;
        }

        private int CalculateProbability()
        {
            if (StackCount == 0) return 0;
            return 20;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}
