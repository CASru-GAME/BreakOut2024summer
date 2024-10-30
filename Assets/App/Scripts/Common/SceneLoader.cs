using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using App.Common.Data.Statistics.Static;

namespace App.Common
{
    /// <summary>
    /// シーンの読み込みを行うクラス
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject transitionPanel;
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
            Time.timeScale = 1;
            transitionPanel.GetComponent<Animator>().SetTrigger("EndTrigger");
            yield return new WaitUntil(() => transitionPanel.GetComponent<Transition>().IsOver == true);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
