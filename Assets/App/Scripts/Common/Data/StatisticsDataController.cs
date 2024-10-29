using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using App.Common.Data.Static;

namespace App.Common.Data
{
    public class StatisticsDataController : MonoBehaviour
    {
        /// <summary>
        /// 統計データリスト
        /// </summary>
        /// <remark>
        /// 降順に並べる
        /// 日付最新はリストの最後尾
        /// </remark>
        public List<StatisticsData> statisticsDataList = new List<StatisticsData>();
        public string _currentSceneName = "";
        void Awake()
        {
            _currentSceneName = SceneManager.GetActiveScene().name;
            //JSONファイルの読み込み
            if (_currentSceneName == "TitleScene")
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
                Debug.Log("Loaded : \n" + json);
                statisticsDataList = JsonUtility.FromJson<StatisticsDataListWrapper>(json).statisticsDataList;
            }
        }

        //JSON→List<StatisticsData>の変換
        public void SaveStatisticsData()
        {
            string filePath = Application.persistentDataPath + "/Resources/StatisticsDataList.json";
            string json = JsonUtility.ToJson(new StatisticsDataListWrapper { statisticsDataList = statisticsDataList });
            Debug.Log("Saved : \n" + json);
            Debug.Log(json);
            File.WriteAllText(filePath, json);
        }
    }

    [System.Serializable]
    public class StatisticsDataListWrapper
    {
        public List<StatisticsData> statisticsDataList;
    }
}
