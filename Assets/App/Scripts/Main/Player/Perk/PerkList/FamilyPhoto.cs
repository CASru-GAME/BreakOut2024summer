using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class FamilyPhoto : IPerk
    {
        private int id = 11;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public FamilyPhoto(PlayerDatastore playerDatastore)
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
            return (int)((1 - 1 / (StackCount + 1)) * 60);
        }

        public int IntEffect()
        {
            int number = Random.Range(1, 100);
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