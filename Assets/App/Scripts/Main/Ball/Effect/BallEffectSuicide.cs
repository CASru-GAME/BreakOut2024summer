using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Ball
{
    public class BallEffectSuicide : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Suicide());

            IEnumerator Suicide()
            {
                yield return new WaitForSeconds(1.1f);
                Destroy(gameObject);
            }
        }
    }
}