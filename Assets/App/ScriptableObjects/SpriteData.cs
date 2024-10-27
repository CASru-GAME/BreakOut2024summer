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
        [SerializeField] private List<Sprite> _result_Back_SpriteList;
        [SerializeField] private Sprite _ExpSprite;


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

        public Sprite GetResultBackSprite(int id)
        {
            return _result_Back_SpriteList[id - 1];
        }

        public Sprite GetItemSprite(int id)
        {
            if(id <= 10) return _ExpSprite;
            if(id <= 13) return GetStaticItemSprite(id - 11);
            return GetUseItemSprite(id - 14);
        }
    }
}