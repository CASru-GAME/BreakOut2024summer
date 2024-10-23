using System.Collections;
using System.Collections.Generic;
using App.Common.Audio;
using UnityEngine;

namespace App.Main.Stage
{
    public class Pose : MonoBehaviour
    {
        [SerializeField] private Canvas _poseCanvas;
        [SerializeField] private WholeSECollector _wholeSeCollector;
        private bool isPosing = false, isInputting = false;
        
        private void Start()
        {
            _poseCanvas.enabled = false;
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (isInputting)
                {
                    return;
                }
                isInputting = true;
                if (isPosing)
                {
                    _wholeSeCollector.PlaySE(3);
                    Close();
                }
                else
                {
                    _wholeSeCollector.PlaySE(3);
                    Open();
                }
            }
            else
            {
                isInputting = false;
            }
        }

        private void Open()
        {
            isPosing = true;
            _poseCanvas.enabled = true;
            Time.timeScale = 0;
        }

        private void Close()
        {
            isPosing = false;
            _poseCanvas.enabled = false;
            Time.timeScale = 1;
        }

        public void OnClickResume()
        {
            isInputting = false;
            Close();
        }
    }
}
