using UnityEngine;
using System.Collections;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Tepra : IPerk
    {
        private int id = 15;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Tepra(PlayerDatastore playerDatastore)
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
            int RandomNumber = Random.Range(0, 100);
            if(RandomNumber < CalculateProbability())
            {
                return 1;
            }
            return 0;
        }

        public float FloatEffect()
        {
            return 0;
        }

        private int CalculateProbability()
        {
            return (int)(1-1/(StackCount+1)*40);
        }
    }
}