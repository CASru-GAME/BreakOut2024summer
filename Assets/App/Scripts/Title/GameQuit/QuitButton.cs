using UnityEngine;

namespace App.Title.GameQuit
{
    public class QuitButton : MonoBehaviour
    {
        public void ButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
