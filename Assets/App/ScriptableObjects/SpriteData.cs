using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        [SerializeField] private List<Sprite> _useItemSpriteList;
        [SerializeField] private List<Sprite> _staticItemSpriteList;
        [SerializeField] private List<Sprite> _perkSpriteList;
        [SerializeField] private List<Sprite> _back_Wall_SpriteList;


        public Sprite GetUseItemSprite(int number)
        {
            return _useItemSpriteList[number];
        }

        public Sprite GetStaticItemSprite(int number)
        {
            return _staticItemSpriteList[number];
        }

        public Sprite GetPerkSprite(int id)
        {
            if(id == 0) return null;
            return _perkSpriteList[id - 1];
        }

        public Sprite GetBackWallSprite(int id)
        {
            return _back_Wall_SpriteList[id - 1];
        }
    }
}