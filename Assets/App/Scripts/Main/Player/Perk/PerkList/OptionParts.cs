using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class OptionParts : IPerk
    {
        private int id = 5;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;

        public OptionParts(PlayerDatastore playerDatastore)
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
            playerDatastore.gameObject.transform.localScale += new Vector3(0.2f, 0,0);
        }



        public int GetId()
        {
            return id;
        }

        public int IntEffect()
        {
            if (StackCount == 0) return 0;
            for (int i = 0; i < StackCount; i++)
            {
                playerDatastore.gameObject.transform.localScale += new Vector3(0.2f, 0,0);
            }
            return 0;
            
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}