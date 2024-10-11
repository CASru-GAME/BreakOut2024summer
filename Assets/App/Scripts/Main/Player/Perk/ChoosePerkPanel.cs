using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.ScriptableObjects;
using UnityEngine.UI;


namespace App.Main.Player.Perk
{
    public class ChoosePerkPanel : MonoBehaviour
    {
        private int PerkId = 0;
        private PerkSystem PerkSystem;
        [SerializeField] private Image _perkIconPanel;
        [SerializeField] private SpriteData _spriteData;
        [SerializeField] private Text _PerkNameText;
        [SerializeField] private Text _PerkExplanationText;
        [SerializeField] private TextData _textData;


        //デバッグ用
        [SerializeField] private int debugPerkId;
        //

        public void Initialize(int perkId, PerkSystem perkSystem)
        {
            this.PerkId = perkId;
            this.PerkSystem = perkSystem;

            //デバッグ用
            if(debugPerkId != 0) this.PerkId = debugPerkId;
            //

            SetPerkIconPanel();
            SetPerkNameText();
            SetPerkExplanationText();
        }

        private void SetPerkIconPanel()
        {
            _perkIconPanel.sprite = _spriteData.GetPerkSprite(PerkId);
        }

        private void SetPerkNameText()
        {
            _PerkNameText.text = _textData.GetPerkName(PerkId);
        }

        private void SetPerkExplanationText()
        {
            _PerkExplanationText.text = _textData.GetPerkExplanation(PerkId);
        }

        public void OnClick()
        {
            PerkSystem.GetPerk(PerkId);
            PerkSystem.SuicideAll();
        }

        public void Suside()
        {
            Destroy(this.gameObject);
        }
    }
}
