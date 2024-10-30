using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;
using App.Common.Data;
using App.ScriptableObjects;
using App.Common;

namespace App.Title.Statistics.Card
{
    public class CardInitializer : MonoBehaviour
    {
        [SerializeField] private Text _dateText;
        [SerializeField] private Text _stageText;
        [SerializeField] private Text _catText;
        [SerializeField] private GameObject _perkListInitialPos;
        [SerializeField] private GameObject _perkListTextInitialPos;
        [SerializeField] private GameObject _ownedPerkPanel;
        [SerializeField] private GameObject _ownedPerkPanelText;
        [SerializeField] private SpriteData _spriteData;
        [NonSerialized] public StatisticsDataController _dataController;
        private StatisticsData _statisticsData;
        private string _clearedDate;
        private int _clearedStage;
        private int _clearedCat;
        private List<(int id, int stackCount)> _ownedPerkList;

        public void Initialize(StatisticsDataController statisticsDataController, int cardId)
        {
            FetchValue(statisticsDataController, cardId);
            SetText();
            SetPerkList();
        }

        private void FetchValue(StatisticsDataController statisticsDataController, int cardId)
        {
            _ownedPerkList = new List<(int id, int stackCount)>();
            this._dataController = statisticsDataController;
            _statisticsData = _dataController.statisticsDataList[cardId];
            _clearedDate = _statisticsData._clearedDate;
            _clearedStage = _statisticsData._totalClearedStage;
            _clearedCat = _statisticsData._totalCat;
            foreach (var perk in _statisticsData._totalAquiredPerkList)
            {
                _ownedPerkList.Add((perk._id, perk._stackCount));
            }
        }

        private void SetText()
        {
            //クリアした日付の初期化
            string dateDefaultText = _dateText.text;
            DateTime parsedDate = DateTime.Parse(_clearedDate);
            _dateText.text = string.Format(dateDefaultText, parsedDate.Year, parsedDate.Month, parsedDate.Day);
            //クリアステージ数の初期化
            string stageDefaultText = _stageText.text;
            _stageText.text = string.Format(stageDefaultText, _clearedStage);
            //クリアした猫の数の初期化
            string catDefaultText = _catText.text;
            _catText.text = string.Format(catDefaultText, _clearedCat);
        }

        private void SetPerkList()
        {
            for (int i = 0; i < _ownedPerkList.Count; i++)
            {
                GameObject ownedPerkPanel = Instantiate(_ownedPerkPanel, _perkListInitialPos.transform);
                SetIndividualPerkPanel(ownedPerkPanel, _ownedPerkList[i].id, _ownedPerkList[i].stackCount, i);
                ownedPerkPanel.transform.SetParent(_perkListInitialPos.transform);
                ownedPerkPanel.transform.localScale = _perkListInitialPos.transform.localScale * 24;
                //パネルの位置の初期化
                ownedPerkPanel.transform.localPosition +=
                new Vector3(
                _ownedPerkPanel.transform.localScale.x * _perkListInitialPos.GetComponent<RectTransform>().sizeDelta.x * (i % 11) / 60,
                -_perkListInitialPos.transform.localScale.y * _perkListInitialPos.GetComponent<RectTransform>().sizeDelta.y * (i / 11), 0.0f
                );
            }
        }

        private void SetIndividualPerkPanel(GameObject ownedPerkPanel, int id, int stackCount, int i)
        {
            //スタックカウントの表示の初期化
            GameObject ownedPerkPanelText = Instantiate(_ownedPerkPanelText, _perkListTextInitialPos.transform);
            ownedPerkPanelText.transform.SetParent(ownedPerkPanel.transform);
            ownedPerkPanelText.transform.localScale = _perkListTextInitialPos.transform.localScale / (16f * 20f);
            //スタックカウントの表示の初期化
            string ownedPerkPanelTextDefaultText = ownedPerkPanelText.GetComponent<Text>().text;
            ownedPerkPanelText.GetComponent<Text>().text = string.Format(ownedPerkPanelTextDefaultText, stackCount);
            if (stackCount == 1) ownedPerkPanelText.SetActive(false);
            //パネルのスプライトの初期化
            Sprite perkSprite = _spriteData.GetPerkSprite(id);
            ownedPerkPanel.GetComponent<Image>().sprite = perkSprite;
            ownedPerkPanel.GetComponent<PerkIcon>().Initialize(id);
        }
    }
}