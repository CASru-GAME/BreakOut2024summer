using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Main.Cat
{
    public class CreateCat : MonoBehaviour
    {
        [SerializeField] private GameObject _catPrefab;
        [SerializeField] private List<RuntimeAnimatorController> _catAnimatorController;

        public void Create(Vector2 pos, Vector2 scale)
        {
            var cat = Instantiate(_catPrefab, pos, Quaternion.identity);
            cat.transform.localScale = Math.Min(scale.x, scale.y) * Vector2.one;
            cat.GetComponent<Animator>().runtimeAnimatorController = _catAnimatorController[UnityEngine.Random.Range(0, _catAnimatorController.Count)];
        }
    }
}