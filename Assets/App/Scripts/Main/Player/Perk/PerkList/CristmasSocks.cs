using UnityEngine;
using System.Collections;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class CristmasSocks : IPerk
    {
        private int id = 17;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public CristmasSocks(PlayerDatastore playerDatastore)
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
            return 0;
        }

        public float FloatEffect()
        {
            if(StackCount == 0) return 1;
            return (float)(1-1/(StackCount+1))*3;
        }
    }
}