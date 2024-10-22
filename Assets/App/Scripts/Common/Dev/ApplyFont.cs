using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Common.Dev
{
    public class ApplyFont : MonoBehaviour
    {
        [SerializeField] private Font _font;

        void Start()
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text text in texts)
            {
                text.font = _font;
            }
        }
    }   
}
