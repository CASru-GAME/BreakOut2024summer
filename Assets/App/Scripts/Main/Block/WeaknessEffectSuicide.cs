using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block
{
    public class WeaknessEffectSuicide : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Suicide());

            IEnumerator Suicide()
            {
                yield return new WaitForSeconds(1.15f);
                Destroy(gameObject);
            }
        }
    }
}