using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Title.Setting
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private Canvas canvas_Setting;
        
        public void Close()
        {
            canvas_Setting.gameObject.SetActive(false);
        }

        public void Open()
        {
            canvas_Setting.gameObject.SetActive(true);
        }
    }
}