using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSuicide : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Suicide());

        IEnumerator Suicide()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}
