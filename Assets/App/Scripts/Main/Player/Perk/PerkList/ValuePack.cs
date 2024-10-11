using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class ValuePack : IPerk
    {
        private int id = 10;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public ValuePack(PlayerDatastore playerDatastore)
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
            return StackCount;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}