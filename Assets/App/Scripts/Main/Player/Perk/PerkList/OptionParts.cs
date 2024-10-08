using UnityEngine;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class OptionParts : IPerk
    {
        private int id = 5;
        private int StackCount = 0;
        private int CurrentStackCount = 0;
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
            double scale = 0.4*(1 - 1 / (StackCount + 1));
            double currentScale = 0.4*(1 - 1 / (CurrentStackCount + 1));
            // プレイヤーのバーの長さを変更する
            if(CurrentStackCount != 0){
                playerDatastore.gameObject.transform.localScale -= new Vector3((float)((playerDatastore.gameObject.transform.localScale.x)*currentScale), 0, 0);
            }
            playerDatastore.gameObject.transform.localScale += new Vector3((float)((playerDatastore.gameObject.transform.localScale.x)*scale), 0, 0);
            CurrentStackCount = StackCount;

        }



        public int GetId()
        {
            return id;
        }

        public int IntEffect()
        {
            if (StackCount == 0) return 0;
            return (int)((1-1/(StackCount+1))*15);
        }

        public float FloatEffect()
        {
            return 0;
        }
    }
}