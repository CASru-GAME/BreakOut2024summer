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
        private BlockDatastore _blockDatastore;
        [SerializeField] private Sprite _normal;
        [SerializeField] private Sprite _damaged;
        [SerializeField] private RuntimeAnimatorController _destroyedAnimatorController;
        [SerializeField] private GameObject _destroyedAnimationPrefab;
        [SerializeField] private Color32 _damageColor;
        [SerializeField] private GameObject _damageEffect;

        public void Start()
        {
            _blockDatastore = GetComponent<BlockDatastore>();
        }

        public void FixedUpdate()
        {
            if(_blockDatastore.Hp == null)
            {
                return;
            }

            if(_blockDatastore.Hp.CurrentValue > _blockDatastore.Hp.MaxValue / 2)
            {
                GetComponent<SpriteRenderer>().sprite = _normal;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _damaged;
            }
        }

        public void Break()
        {
            var newDestroyedAnimationPrefab = Instantiate(_destroyedAnimationPrefab, transform.position, Quaternion.identity);
            newDestroyedAnimationPrefab.transform.localScale = transform.localScale;
            newDestroyedAnimationPrefab.GetComponent<Animator>().runtimeAnimatorController = _destroyedAnimatorController;
        }

        public void CreateDamageEffect(int damageValue, StageSystem stage)
        {
            var newDamageEffect = Instantiate(_damageEffect, transform.position, Quaternion.identity);
            newDamageEffect.GetComponent<DamageEffect>().Initialize(damageValue, stage.Canvas);
            newDamageEffect.GetComponent<DamageEffect>().GetComponent<Text>().color = _damageColor;
        }
    }
}