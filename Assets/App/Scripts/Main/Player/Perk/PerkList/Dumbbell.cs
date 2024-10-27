using UnityEngine;
using App.Main.Player;
using System;

namespace App.Main.Player.Perk
{
    public class Dumbbell : IPerk
    {
        private int id = 12;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public Dumbbell(PlayerDatastore playerDatastore)
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
            return playerDatastore.Parameter.GetDumbbellPower();
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}