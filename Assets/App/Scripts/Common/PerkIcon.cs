using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.ScriptableObjects;
using UnityEngine.UI;

namespace App.Common
{
    public class PerkIcon : MonoBehaviour
    {
        private int _perkId;
        [SerializeField] private TextData _textData;
        [SerializeField] private GameObject panel;
        [SerializeField] private Text text_Name;
        [SerializeField] private Text text_Explanation;

        public void Initialize(int perkId)
        {
            _perkId = perkId;
            text_Name.text = _textData.GetPerkName(_perkId);
            text_Explanation.text = _textData.GetPerkExplanation(_perkId);
            panel.SetActive(false);
        }

        public void OnPoint()
        {
            panel.SetActive(true);
        }

        public void OutPoint()
        {
            panel.SetActive(false);
        }
    }
}