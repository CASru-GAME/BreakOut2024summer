using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block
{
    public class PoisonEffectSuicide : MonoBehaviour
    {
        IBlock _block;
        private bool ischeck = false;

        public void initialize(IBlock block)
        {
            _block = block;
        }
        private void FixedUpdate()
        {
            if(ischeck == false)
            {
                StartCoroutine(check());
            }

            IEnumerator check()
            {
                ischeck = true;
                yield return new WaitForSeconds(0.3f);
                if(_block.PoisonStack == 0 || _block == null)
                {
                    Destroy(gameObject);
                }
                ischeck = false;
                
            }
        }

        public void Suicide()
        {
            Destroy(gameObject);
        }
    }
}