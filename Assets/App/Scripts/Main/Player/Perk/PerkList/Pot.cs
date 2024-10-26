using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Pot : IPerk
    {
        private int id = 13;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Pot(PlayerDatastore playerDatastore)
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

        private int CalculateProbability(int value)
        {
            return 5 * value;
        }


        public int GetId()
        {
            return id;
        }

        public int IntEffect()
        {
            if (StackCount == 0) return 0;
            Random.InitState(System.DateTime.Now.Millisecond); 
            if(Random.Range(0, 100) < CalculateProbability(StackCount)) return 1;
            return 0;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}