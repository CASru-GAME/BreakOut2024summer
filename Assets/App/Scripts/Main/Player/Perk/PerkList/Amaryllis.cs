using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Amaryllis : IPerk
    {
        private int id = 6;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Amaryllis(PlayerDatastore playerDatastore)
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
            if (StackCount == 0) return 0;
            return (int)(1+3*StackCount);
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}