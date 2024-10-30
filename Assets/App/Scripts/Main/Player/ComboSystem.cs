using System.Diagnostics;

namespace App.Main.Player
{

    public class ComboSystem
    {
        private PlayerDatastore playerDatastore;
        public int ComboResetCount = 0;
        public int MaxComboCount = 150;

        public ComboSystem(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
        }

        public void AddComboResetCount()
        {
            ComboResetCount++;
            ResetCombo();
        }

        private void ResetCombo()
        {
            if(ComboResetCount >= MaxComboCount)
            {
                playerDatastore.ResetComboCount();
                ComboResetCount = 0;
            }
        }      

        public void ResetComboResetCount()
        {
            ComboResetCount = 0;
        }  
    }
}