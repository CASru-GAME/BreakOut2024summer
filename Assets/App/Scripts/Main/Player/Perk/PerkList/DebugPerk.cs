using UnityEngine;

namespace App.Main.Player.Perk
{
    public class DebugPerk : IPerk
    {
        private int id = 0;
        private int StackCount = 0;
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
            if(StackCount == 0) return;
            Debug.Log("Hello,Perk!" + StackCount);
        }

        public int GetId()
        {
            return id;
        }

        public int AttackEffect()
        {
            return 0;
        }
    }
}