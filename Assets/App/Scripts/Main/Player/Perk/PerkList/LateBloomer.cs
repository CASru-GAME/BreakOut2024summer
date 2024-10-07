using UnityEngine;
using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Player.Perk
{
    public class LateBloomer : IPerk
    {
        private int Id = 3;
        private int StackCount = 0;
        private PlayerDatastore playerDatastore;
        private ProcessSystem ProcessSystem;

        public LateBloomer(PlayerDatastore playerDatastore, ProcessSystem processSystem)
        {
            this.playerDatastore = playerDatastore;
            this.ProcessSystem = processSystem;
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
            // ここには何も書かない
        }

        private int CalculateValue(int value, float time)
        {
            
            return (int)((1-1/(value+1))*time*0.5);
        }

        public int AttackEffect()
        {
            if(StackCount == 0) return 0;
            return CalculateValue(StackCount, ProcessSystem.GetTime_afterCurrentStageStarted());
        }

        public int GetId()
        {
            return Id;
        }
    }
}