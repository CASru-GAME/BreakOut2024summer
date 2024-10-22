using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Common
{
    public class Transition : MonoBehaviour
    {
        public bool IsOver { get; private set; } = false;
        public void OnTransitionEnd()
        {
            IsOver = true;
        }
    }
}
