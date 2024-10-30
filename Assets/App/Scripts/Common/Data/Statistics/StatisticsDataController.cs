using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using App.Common.Data.Statistics;
using App.Common.Data.Statistics.Static;
using Unity.VisualScripting;
using System;

namespace App.Common.Data.Statistics
{
    public class StatisticsDataController : MonoBehaviour
    {
        [SerializeField] private string debugscene = "DebugScene";
        /// <summary>
        /// 統計データリスト
        /// </summary>
        /// <remark>
        /// 降順に並べる
        /// 日付最新はリストの最後尾
        /// </remark>
        [NonSerialized] public List<StatisticsData> statisticsDataList = new List<StatisticsData>();
        private string _currentSceneName = "";
        void Awake()
        {
            _currentSceneName = SceneManager.GetActiveScene().name;
            //JSONファイルの読み込み
            if ((_currentSceneName == "TitleScene") || (_currentSceneName == debugscene))
            {
                LoadStatisticsData();
            }
        }
        void OnDestroy()
        {
            //JSONファイルの書き込み
            if (_currentSceneName == "ResultScene")
            {
                //新しいデータを追加
                StatisticsData newStatisticsData = new StatisticsData(System.DateTime.Now.ToString(), StatisticsDatastore._totalClearedStage, StatisticsDatastore._totalCat, StatisticsDatastore._totalAquiredPerkList);
                statisticsDataList = new List<StatisticsData>();
                LoadStatisticsData();
                statisticsDataList.Add(newStatisticsData);
                //JSONファイルに保存
                SaveStatisticsData();
            }
        }

        //List<StatisticsData>→JSONの変換
        public void LoadStatisticsData()
        {
            string filePath = Application.persistentDataPath + "/Resources/StatisticsDataList.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                statisticsDataList = JsonUtility.FromJson<StatisticsDataListWrapper>(json).statisticsDataList;
            }
        }

        //JSON→List<StatisticsData>の変換
        public void SaveStatisticsData()
        {
            string filePath = Application.persistentDataPath + "/Resources/StatisticsDataList.json";
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            StatisticsDataListWrapper statisticsDataListWrapper = new StatisticsDataListWrapper { statisticsDataList = statisticsDataList };
            string json = JsonUtility.ToJson(statisticsDataListWrapper, true);
            File.WriteAllText(filePath, json);
        }

        //統計データのリセット
        public void ResetStatisticsData()
        {
            string filePath = Application.persistentDataPath + "/Resources/StatisticsDataList.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            statisticsDataList = new List<StatisticsData>();
        }
    }

    [System.Serializable]
    public class StatisticsDataListWrapper
    {
        public List<StatisticsData> statisticsDataList;
    }
}
