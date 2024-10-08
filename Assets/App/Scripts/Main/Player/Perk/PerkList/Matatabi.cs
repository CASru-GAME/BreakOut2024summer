using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Matatabi : IPerk
    {
        private int id = 3;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Matatabi(PlayerDatastore playerDatastore)
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

        private int CaluculateProbability()
        {
            return (10*StackCount);
        }

        public int IntEffect()
        {
            if (StackCount == 0) return 0;
            int number = Random.Range(1,100);
            if (number <= CaluculateProbability())
            {
                return 1;
            }
            return 0;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}