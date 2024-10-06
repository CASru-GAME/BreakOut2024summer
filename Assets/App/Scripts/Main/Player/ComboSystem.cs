using System.Diagnostics;

namespace App.Main.Player
{

    public class ComboSystem
    {
        private PlayerDatastore playerDatastore;
        public int ComboResetCount = 0;

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
            if(ComboResetCount >= 300) playerDatastore.ResetComboCount();
        }

        
    }
}