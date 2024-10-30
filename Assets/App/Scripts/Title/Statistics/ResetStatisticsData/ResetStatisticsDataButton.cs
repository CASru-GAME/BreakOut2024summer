using UnityEngine;
using UnityEngine.UI;
using App.ScriptableObjects;
using App.Common.Data.Statistics;
using System.Collections.Generic;
using UnityEditor;

namespace App.Title.Statistics.ResetStatisticsData
{
    public class ResetStatisticsDataButton : MonoBehaviour
    {
        [SerializeField] private StatisticsDataController DataController;
        [SerializeField] private Canvas canvas_resetStatisticsData;
        [SerializeField] private StatisticsButton statisticsButton;

        public void Close()
        {
            canvas_resetStatisticsData.gameObject.SetActive(false);
        }

        public void Open()
        {
            canvas_resetStatisticsData.gameObject.SetActive(true);
        }

        public void ResetData()
        {
            DataController.ResetStatisticsData();
            statisticsButton.FetchData();
            Close();
            statisticsButton.Close();
        }
    }
}