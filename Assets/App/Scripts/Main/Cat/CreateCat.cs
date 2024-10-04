using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Main.Cat
{
    public class CreateCat : MonoBehaviour
    {
        [SerializeField] private GameObject catPrefab;
        [SerializeField] private List<RuntimeAnimatorController> catAnimatorController;

        public void Create(Vector2 pos, Vector2 scale)
        {
            var cat = Instantiate(catPrefab, pos, Quaternion.identity);
            cat.transform.localScale = Math.Min(scale.x, scale.y) * Vector2.one;
            cat.GetComponent<Animator>().runtimeAnimatorController = catAnimatorController[UnityEngine.Random.Range(0, catAnimatorController.Count)];
        }
    }
}