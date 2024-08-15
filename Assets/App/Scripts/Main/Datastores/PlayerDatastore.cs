using App.Main.Data;
using UnityEngine;
using System;

namespace App.Main.Datastores
{
    public class PlayerDatastore : MonoBehaviour
    {
        public PlayerParameter playerParameter;

        void Start()
        {
            //仮に10と5.0fを設定
            playerParameter = new PlayerParameter(10, 5.0f);
        }

        void ReconfigurePlayerParameter(int atk, float speed)
        {
            playerParameter = new PlayerParameter(atk, speed);
        }

        void ShowPlayerParameter()
        {
            Debug.Log($"AttackPoint: {playerParameter.attackPoint}");
            Debug.Log($"PlayerMoveSpeed: {playerParameter.moveSpeed}");
        }
    }
}