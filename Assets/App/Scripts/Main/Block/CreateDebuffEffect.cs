using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Main.Block
{
    public class CreateDebuffEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _PoisonEffectPrefab;
        [SerializeField] private GameObject _WeaknessEffectPrefab;
        GameObject PoisonEffect;
        GameObject WeaknessEffect;
        public void CreatePoisonEffect(Vector2 pos)
        {
            PoisonEffect = Instantiate(_PoisonEffectPrefab, pos, Quaternion.identity);
        }
        public void CreateWeaknessEffect(Vector2 pos)
        {
            WeaknessEffect = Instantiate(_WeaknessEffectPrefab, pos, Quaternion.identity);
        }
    }
}