using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class YellowSubmarine : IPerk
    {
        private int id = 14;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public YellowSubmarine(PlayerDatastore playerDatastore)
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
        
        //効果のオンオフを返す
        public int IntEffect()
        {
            return 0;
        }

        public float FloatEffect()
        {
            if (StackCount == 0) return 0;
            return 1;
        }
    }
}