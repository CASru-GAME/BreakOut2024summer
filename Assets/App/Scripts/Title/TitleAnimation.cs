using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        [SerializeField] private Animator transitionPanel = default;
        void Start()
        {
           transitionPanel.SetTrigger("StartTrigger");
        }
}
}
