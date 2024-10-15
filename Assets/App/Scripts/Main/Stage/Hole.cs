using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Ball;

namespace App.Main.Stage
{
    public class Hole : MonoBehaviour
    {   
        private StageSystem _stageSystem;

        public void Initialized(StageSystem _stageSystem)
        {
            this._stageSystem = _stageSystem;
        }

        void OnTriggerEnter2D(Collider2D other) 
        {   
            var _ball = other.GetComponent<Ball.Ball>();
            if(_ball)
            {
                _stageSystem.DecreaseBallCountonStage();
                Destroy(other.gameObject);
            }
        }
    }
}