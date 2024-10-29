using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Ball
{
    public class FireWorksEffectSuicide : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Suicide());

            IEnumerator Suicide()
            {
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
            }
        }
    }
}