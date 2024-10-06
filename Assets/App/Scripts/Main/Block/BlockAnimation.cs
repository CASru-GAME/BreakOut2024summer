using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Main.Effects;
using App.Main.Stage;

namespace App.Main.Block
{
    public class BlockAnimation : MonoBehaviour
    {
        private BlockDatastore blockDatastore;
        [SerializeField] private Sprite normal;
        [SerializeField] private Sprite damaged;
        [SerializeField] private RuntimeAnimatorController destroyedAnimatorController;
        [SerializeField] private GameObject destroyedAnimationPrefab;
        [SerializeField] private Color32 damageColor;
        [SerializeField] private GameObject damageEffect;

        public void Start()
        {
            blockDatastore = GetComponent<BlockDatastore>();
        }

        public void FixedUpdate()
        {
            if(blockDatastore.Hp == null)
            {
                return;
            }

            if(blockDatastore.Hp.CurrentValue > blockDatastore.Hp.MaxValue / 2)
            {
                GetComponent<SpriteRenderer>().sprite = normal;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = damaged;
            }
        }

        public void Break()
        {
            var newDestroyedAnimationPrefab = Instantiate(destroyedAnimationPrefab, transform.position, Quaternion.identity);
            newDestroyedAnimationPrefab.transform.localScale = transform.localScale;
            newDestroyedAnimationPrefab.GetComponent<Animator>().runtimeAnimatorController = destroyedAnimatorController;
        }

        public void CreateDamageEffect(int damageValue, StageSystem stage)
        {
            var newDamageEffect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            newDamageEffect.GetComponent<DamageEffect>().Initialize(damageValue, stage.Canvas);
            newDamageEffect.GetComponent<DamageEffect>().GetComponent<Text>().color = damageColor;
        }
    }
}