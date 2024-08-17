using App.Main.Data;
using UnityEngine;
using System;

namespace App.Main.Datastores 
{
    public class PlayerDatastore : MonoBehaviour
    {
        public PlayerParameter playerParameter;

        public void InitializePlayer()
        {
            playerParameter = new PlayerParameter(10, 5.0f);
        }

    }
}