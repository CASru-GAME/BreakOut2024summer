using UnityEngine;
using System.Collections;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class CristmasSocks : IPerk
    {
        private int id = 20;
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
            return (int)(1-1/(StackCount+1))*40;
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}