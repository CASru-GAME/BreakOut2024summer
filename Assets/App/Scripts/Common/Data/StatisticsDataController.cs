using UnityEngine;
using System.IO;
using App.Common.Data;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace App.Common.Data
{
    public class StatisticsDataController : MonoBehaviour
    {
        public TopValueData topValueData = new TopValueData();
        public List<StatisticsData> statisticsDataList = new List<StatisticsData>();
        public string _currentSceneName = "";
        void Awake()
        {
            _currentSceneName = SceneManager.GetActiveScene().name;
            //JSONファイルの読み込み
            if (_currentSceneName == "MainScene")
            {
                
            }
        }
        void OnDestroy()
        {
            //JSONファイルの書き込み
            if (_currentSceneName == "ResultScene")
            {

            }
        }
    }
}