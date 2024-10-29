using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Main.Ball
{
    public class CreateBallOutEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        public void Create(Vector2 pos)
        {
            if(pos.y < -1f)
            {
                var effect = Instantiate(_effectPrefab, new Vector2(pos.x,-4.9f), Quaternion.identity);
            }
            else if(pos.y > 4f)
            {
                var effect = Instantiate(_effectPrefab, new Vector2(pos.x,4.2f), Quaternion.Euler(180, 0, 0));
            }
            else if(pos.x < -4f)
            {
                var effect = Instantiate(_effectPrefab, new Vector2(-4.2f,pos.y), Quaternion.Euler(0, 0, 90));
            }
            else if(pos.x > -0.2f)
            {
                var effect = Instantiate(_effectPrefab, new Vector2(0,pos.y), Quaternion.Euler(0, 0, -90));
            }
            else
            {
                var effect = Instantiate(_effectPrefab, pos, Quaternion.identity);
            }
            
            
        }
    }
}