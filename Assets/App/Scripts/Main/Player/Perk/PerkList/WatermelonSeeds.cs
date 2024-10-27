using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class WatermelonSeeds : IPerk
    {
        private int id = 16;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public WatermelonSeeds(PlayerDatastore playerDatastore)
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
            return (int)((1-1/(StackCount+1))*playerDatastore.GetComboCount()*2);
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}