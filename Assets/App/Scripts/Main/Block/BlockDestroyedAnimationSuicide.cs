using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block
{
    public class BlockDestroyedAnimationSuicide : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Suicide());

            IEnumerator Suicide()
            {
                yield return new WaitForSeconds(1f);
                Destroy(gameObject);
            }
        }
    }
}
