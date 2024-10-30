using UnityEngine;
using UnityEngine.UI;
using App.ScriptableObjects;
using App.Common.Data;
using System.Collections.Generic;
using UnityEditor;

namespace App.Title.Statistics
{
    public class StatisticsButton : MonoBehaviour
    {
        [SerializeField] private StatisticsDataController DataController;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject backGround_Statistics;
        private List<StatisticsData> statisticsDataList;
        [SerializeField] private Canvas canvas_statistics;
        private int _maxCardCountonOnePage = 6;
        private int _currentPageId = 0;
        [SerializeField] private Color32 activeColor;
        [SerializeField] private Color32 inactiveColor;
        [SerializeField] private Text nextButtonText;
        [SerializeField] private Text previousButtonText;
        private List<GameObject> _currentDisplayedCardList;

        void Start()
        {
            statisticsDataList = DataController.statisticsDataList;
            Debug.Log(statisticsDataList.Count);
        }

        public void Close()
        {
            canvas_statistics.gameObject.SetActive(false);
        }

        public void Open()
        {
            canvas_statistics.gameObject.SetActive(true);
            _currentPageId = 0;
            SetCard();
            previousButtonText.color = inactiveColor;
            nextButtonText.color = inactiveColor;
            if (statisticsDataList.Count > _maxCardCountonOnePage) nextButtonText.color = activeColor;
        }

        public void GoToNext()
        {
            if ((_currentPageId * _maxCardCountonOnePage + statisticsDataList.Count % _maxCardCountonOnePage) >= statisticsDataList.Count) return;
            _currentPageId++;
            SetCard();
            previousButtonText.color = activeColor;
            if (((_currentPageId + 1) * _maxCardCountonOnePage + statisticsDataList.Count % _maxCardCountonOnePage) == statisticsDataList.Count) return;
            nextButtonText.color = inactiveColor;
        }

        public void GoToPrevious()
        {
            if (_currentPageId == 0) return;
            _currentPageId--;
            SetCard();
            nextButtonText.color = activeColor;
            if (_currentPageId != 0) return;
            previousButtonText.color = inactiveColor;
        }

        private void SetCard()
        {
            if (_currentDisplayedCardList != null) ClearCard();
            _currentDisplayedCardList = new List<GameObject>();
            for (int i = 0; i < _maxCardCountonOnePage; i++)
            {
                if (i + _currentPageId * _maxCardCountonOnePage >= statisticsDataList.Count) break;
                var card = Instantiate(cardPrefab, transform);
                _currentDisplayedCardList.Add(card);
                card.transform.SetParent(backGround_Statistics.transform);

                card.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                float yOffset = -i * 60 + 150;
                card.transform.localPosition = new Vector3(0, yOffset, 0);

                card.GetComponent<Card.CardInitializer>().Initialize(DataController, i + _currentPageId * _maxCardCountonOnePage);
            }
        }

        private void ClearCard()
        {
            foreach (var card in _currentDisplayedCardList)
            {
                Destroy(card);
            }
        }
    }
}