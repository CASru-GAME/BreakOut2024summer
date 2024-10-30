using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block
{
    public class PoisonEffectSuicide : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Suicide());

            IEnumerator Suicide()
            {
                yield return new WaitForSeconds(0.42f);
                Destroy(gameObject);
            }
        }
    }
}