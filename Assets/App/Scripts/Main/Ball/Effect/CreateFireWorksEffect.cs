using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Main.Ball
{
    public class CreateFireWorksEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        public void Create(Vector2 pos)
        {
                var effect = Instantiate(_effectPrefab, pos, Quaternion.identity);
        }
    }
}