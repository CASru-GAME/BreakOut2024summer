using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Main.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        [SerializeField] private Animator transitionPanel = default;
        [SerializeField] private float loopTime = 0f;
        [SerializeField] private float moveWidth = 0f;
        [SerializeField] private Image  image = default;
        void Start()
        {
           transitionPanel.SetTrigger("StartTrigger");
           StartCoroutine(MoveBack());
        }

        private IEnumerator MoveBack()
        {
            Vector2 defaultPos = image.rectTransform.anchoredPosition;
            while(true)
            {
                float time = 0;
                while(time < loopTime)
                {
                    time += Time.deltaTime;
                    float x = Mathf.Sin(time * 2 * Mathf.PI / loopTime);
                    image.rectTransform.anchoredPosition = defaultPos + new Vector2(x * moveWidth, 0);
                    yield return null;
                }
            }
        }
    }
}
