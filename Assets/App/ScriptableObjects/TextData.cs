using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/TextData")]
    public class TextData : ScriptableObject
    {
        [SerializeField] private List<string> _perkNameList;
        [SerializeField] private List<string> _perkExplanationList;

        public string GetPerkName(int id)
        {
            if(id == 0) return "デバッグ";
            return _perkNameList[id - 1];
        }

        public string GetPerkExplanation(int id)
        {
            if(id == 0) return "デバッグ";
            return _perkExplanationList[id - 1];
        }
    }
}