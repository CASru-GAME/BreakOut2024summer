using UnityEngine;
using System.Collections;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class TenchiMuyo : IPerk
    {
        private int id = 15;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public TenchiMuyo(PlayerDatastore playerDatastore)
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
            return 1+StackCount;
        }

        public float FloatEffect()
        {
            if(StackCount == 0) return 1;
            return Calculate();
        }

        private float Calculate()
        {
            return (float)1+(1-1/(StackCount+1));
        }

        private IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}