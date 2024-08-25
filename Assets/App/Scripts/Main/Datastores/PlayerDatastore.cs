using App.Main.Data;
using UnityEngine;
using System;

namespace App.Main.Datastores 
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