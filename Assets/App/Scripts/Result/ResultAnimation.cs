using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Static;
using App.ScriptableObjects;
using App.Common;

namespace App.Main.Result
{
    public class ResultAnimation : MonoBehaviour
    {
        [SerializeField] private float _shuffleTime;
        [SerializeField] private float _intervalTime;
        [SerializeField] private Text stageText;
        [SerializeField] private Text catText;
        private string _stageDefaultText, _catDefaultText, _perkDefaultText;
        [SerializeField] private GameObject _ownedPerkPanelPrefab;
        [SerializeField] private GameObject _ownedPerkPanelTextPrefab;
        [SerializeField] private GameObject _ownedPerkPanelPos;
        [SerializeField] private GameObject _ownedPerkPanelTextPos;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private SpriteData _spriteData;

        private void Start()
        {
            _stageDefaultText = stageText.text;
            stageText.text = string.Format(_stageDefaultText, " ");
            _catDefaultText = catText.text;
            catText.text = string.Format(_catDefaultText, " ");
            StartCoroutine(ShowResult());
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
                yield return new WaitForSeconds(_shuffleTime);
                stageText.text = string.Format(_stageDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            if(StatisticsDatastore._totalCat == 0)
            {
                catText.text = string.Format(_catDefaultText, 0);
            }
            for(int i = 0; i < StatisticsDatastore._totalCat; i++)
            {
                yield return new WaitForSeconds(_shuffleTime);
                catText.text = string.Format(_catDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            var ownedPerkList = StatisticsDatastore._totalAquiredPerkList;
            for(int i = 0; i < ownedPerkList.GetLength(0); i++)
            {
                if(ownedPerkList[i, 1] == -1) continue;
                int id = ownedPerkList[i, 0];
                int stackCount = ownedPerkList[i, 1];
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
                if(stackCount == 0) newOwnedPerkPanelTextPrefab.SetActive(false);
            }
        }
    }
}