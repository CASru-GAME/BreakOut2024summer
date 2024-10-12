using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using App.Static;

namespace App.Common
{
    /// <summary>
    /// シーンの読み込みを行うクラス
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// 指定されたシーンを非同期で読み込みます。
        /// </summary>
        /// <param name="sceneName">読み込むシーンの名前</param>
        public void LoadSceneAsyncByName(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        /// <summary>
        /// メインゲームを最初から読み込みます。
        /// </summary>
        public void LoadMainGamefromBegining()
        {
            StatisticsDatastore.ResetAllStatisticsData();
            StartCoroutine(LoadSceneAsync("MainScene"));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // シーンの読み込みが完了するまで待機
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}