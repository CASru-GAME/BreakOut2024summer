using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Experimental;

namespace App.Main.Player
{
    public class PlayerDatastore : MonoBehaviour
    {
        public PlayerParameter Parameter { get; private set; }

        public void InitializePlayer()
        {
            Parameter = new PlayerParameter(10, 5.0f);
        }
    }
}