using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Fireworks : IPerk
    {
        private int id = 19;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Fireworks(PlayerDatastore playerDatastore)
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
            if(StackCount == 0) return 0;
            return 1;
        }

        public float FloatEffect()
        {
            return 1f - 1f / (StackCount + 1f);
        }
    }
}