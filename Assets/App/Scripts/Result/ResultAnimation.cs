using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Common.Data.Static;
using App.ScriptableObjects;
using App.Common;
using System;
using System.Linq;

namespace App.Main.Result
{
    public class ResultAnimation : MonoBehaviour
    {
        [SerializeField] private float _shuffleTime;
        [SerializeField] private float _intervalTime;
        [SerializeField] private Animator transitionPanel;
        [SerializeField] private Text stageText;
        [SerializeField] private Text catText;
        private string _stageDefaultText, _catDefaultText, _perkDefaultText;
        [SerializeField] private GameObject _ownedPerkPanelPrefab;
        [SerializeField] private GameObject _ownedPerkPanelTextPrefab;
        [SerializeField] private GameObject _ownedPerkPanelPos;
        [SerializeField] private GameObject _ownedPerkPanelTextPos;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private SpriteData _spriteData;
        [SerializeField] private SpriteRenderer back_Image;

        private void Start()
        {
            transitionPanel.SetTrigger("StartTrigger");
            _stageDefaultText = stageText.text;
            stageText.text = string.Format(_stageDefaultText, " ");
            _catDefaultText = catText.text;
            catText.text = string.Format(_catDefaultText, " ");
            StartCoroutine(ShowResult());
            back_Image.sprite = _spriteData.GetResultBackSprite((StatisticsDatastore._totalClearedStage) % 15 / 3 + 1);
        }

        private IEnumerator ShowResult()
        {
            yield return new WaitForSeconds(_intervalTime);

            if(StatisticsDatastore._totalClearedStage == 0)
            {
                stageText.text = string.Format(_stageDefaultText, 0);
            }
            for(int i = 0; i < StatisticsDatastore._totalClearedStage; i++)
            {
                yield return new WaitForSeconds(_shuffleTime / StatisticsDatastore._totalClearedStage);
                stageText.text = string.Format(_stageDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            if(StatisticsDatastore._totalCat == 0)
            {
                catText.text = string.Format(_catDefaultText, 0);
            }
            for(int i = 0; i < StatisticsDatastore._totalCat; i++)
            {
                yield return new WaitForSeconds(_shuffleTime / StatisticsDatastore._totalCat);
                catText.text = string.Format(_catDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            var ownedPerkList = StatisticsDatastore._totalAquiredPerkList;
            for(int i = 0; i < ownedPerkList.Count; i++)
            {
                int id = ownedPerkList[i].id;
                int stackCount = ownedPerkList[i].stackCount;
                var newOwnedPerkPanelPrefab = Instantiate(_ownedPerkPanelPrefab, _ownedPerkPanelPos.transform.position, Quaternion.identity);
                newOwnedPerkPanelPrefab.transform.SetParent(_canvas.transform);
                newOwnedPerkPanelPrefab.transform.localScale = _ownedPerkPanelPos.transform.localScale;
                newOwnedPerkPanelPrefab.transform.localPosition += 
                new Vector3(_ownedPerkPanelPos.transform.localScale.x * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanelPos.transform.localScale.y * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                newOwnedPerkPanelPrefab.GetComponent<Image>().sprite = _spriteData.GetPerkSprite(id);
                newOwnedPerkPanelPrefab.GetComponent<PerkIcon>().Initialize(id);
                var newOwnedPerkPanelTextPrefab = Instantiate(_ownedPerkPanelTextPrefab, _ownedPerkPanelTextPos.transform.position, Quaternion.identity);
                newOwnedPerkPanelTextPrefab.transform.SetParent(_canvas.transform);
                newOwnedPerkPanelTextPrefab.transform.localScale = _ownedPerkPanelTextPos.transform.localScale;
                newOwnedPerkPanelTextPrefab.transform.localPosition += 
                new Vector3(_ownedPerkPanelPos.transform.localScale.x * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanelPos.transform.localScale.y * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                string _ownedPerkPanelDefaultText = newOwnedPerkPanelTextPrefab.GetComponent<Text>().text;
                newOwnedPerkPanelTextPrefab.GetComponent<Text>().text = string.Format(_ownedPerkPanelDefaultText, stackCount);
                if(stackCount == 1) newOwnedPerkPanelTextPrefab.SetActive(false);
            }
        }
    }
}