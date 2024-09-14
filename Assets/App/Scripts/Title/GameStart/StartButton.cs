using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Title.GameStart
{
    public class StartButton : MonoBehaviour
    {
        public void ButtonClick()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}