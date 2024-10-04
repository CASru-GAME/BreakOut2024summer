using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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