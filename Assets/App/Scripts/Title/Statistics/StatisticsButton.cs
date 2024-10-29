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
        private int _maxCardCountonOnePage = 5;
        private int _currentPageId = 0;
        [SerializeField] private Color32 activeColor;
        [SerializeField] private Color32 inactiveColor;
        [SerializeField] private Text nextButtonText;
        [SerializeField] private Text previousButtonText;

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
            if (((_currentPageId + 1) * _maxCardCountonOnePage + statisticsDataList.Count % _maxCardCountonOnePage) >= statisticsDataList.Count) return;
            _currentPageId++;
            SetCard();
            previousButtonText.color = activeColor;
            if (((_currentPageId + 2) * _maxCardCountonOnePage + statisticsDataList.Count % _maxCardCountonOnePage) == statisticsDataList.Count) return;
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
            for (int i = 0; i < _maxCardCountonOnePage; i++)
            {
                if (i + _currentPageId * _maxCardCountonOnePage >= statisticsDataList.Count) break;
                Debug.Log(i + _currentPageId * _maxCardCountonOnePage);
                var card = Instantiate(cardPrefab, transform);
                card.transform.SetParent(backGround_Statistics.transform);

                float yOffset = -i * 200;
                card.transform.localPosition = new Vector3(0, yOffset, 0);

                card.GetComponent<Card.CardInitializer>().Initialize(DataController, i + _currentPageId * _maxCardCountonOnePage);
            }
        }
    }
}