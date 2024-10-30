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
        IBlock _block;
        public void initialize(IBlock block)
        {
            _block = block;
        }
        public void CreatePoisonEffect(Vector2 pos)
        {
            PoisonEffect = Instantiate(_PoisonEffectPrefab, pos, Quaternion.identity);
            PoisonEffect.GetComponent<PoisonEffectSuicide>().initialize(_block);
        }
        public void CreateWeaknessEffect(Vector2 pos)
        {
            WeaknessEffect = Instantiate(_WeaknessEffectPrefab, pos, Quaternion.identity);
            WeaknessEffect.GetComponent<WeaknessEffectSuicide>().initialize(_block);
        }

        public void DestroyEffect()
        {
            if(PoisonEffect != null)
            {
                PoisonEffect.GetComponent<PoisonEffectSuicide>().Suicide();
            }
            if(WeaknessEffect != null)
            {
                WeaknessEffect.GetComponent<WeaknessEffectSuicide>().Suicide();
            }
        }
    }
}