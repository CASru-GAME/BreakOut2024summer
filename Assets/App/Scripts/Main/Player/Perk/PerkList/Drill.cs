using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class Drill : IPerk
    {
        private int id = 8;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Drill(PlayerDatastore playerDatastore)
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
