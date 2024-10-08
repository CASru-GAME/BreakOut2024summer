using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class ManekiCat : IPerk
    {
        private int id = 1;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public ManekiCat(PlayerDatastore playerDatastore)
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
            if (StackCount == 0) return 0;
            return (float)(1+0.125*StackCount);
        }
    }
}