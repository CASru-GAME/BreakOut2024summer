using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.ScriptableObjects;

namespace App.Title.Intro
{
    public class IntroButton : MonoBehaviour
    {
        [SerializeField] private Canvas canvas_Intro;
        [SerializeField] private SpriteData spriteData;
        private int _currentIntroSpriteId = 0;
        [SerializeField] private Image image_Intro;
        [SerializeField] private Color32 activeColor;
        [SerializeField] private Color32 inactiveColor;
        [SerializeField] private Text nextButtonText;
        [SerializeField] private Text previousButtonText;

        public void Close()
        {
            canvas_Intro.gameObject.SetActive(false);
        }

        public void Open()
        {
            canvas_Intro.gameObject.SetActive(true);
            _currentIntroSpriteId = 0;
            image_Intro.sprite = spriteData.GetIntroSprite(_currentIntroSpriteId);
            previousButtonText.color = inactiveColor;
            nextButtonText.color = activeColor;
        }

        public void GoToNext()
        {
            if(_currentIntroSpriteId == spriteData.GetIntroSpriteCount() - 1) return;
            _currentIntroSpriteId++;
            image_Intro.sprite = spriteData.GetIntroSprite(_currentIntroSpriteId);
            previousButtonText.color = activeColor;
            if(_currentIntroSpriteId != spriteData.GetIntroSpriteCount() - 1) return;
            nextButtonText.color = inactiveColor;
        }

        public void GoToPrevious()
        {
            if(_currentIntroSpriteId == 0) return;
            _currentIntroSpriteId--;
            image_Intro.sprite = spriteData.GetIntroSprite(_currentIntroSpriteId);
            nextButtonText.color = activeColor;
            if(_currentIntroSpriteId != 0) return;
            previousButtonText.color = inactiveColor;
        }
    }
}