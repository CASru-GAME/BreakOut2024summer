using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Static;

namespace App.Main.Result
{
    public class ResultAnimation : MonoBehaviour
    {
        [SerializeField] private Text stageText;
        [SerializeField] private Text catText;
        private string _stageDefaultText, _catDefaultText, _perkDefaultText;
        [SerializeField] private float _shuffleTime;
        [SerializeField] private float _intervalTime;

        private void Start()
        {
            _stageDefaultText = stageText.text;
            stageText.text = string.Format(_stageDefaultText, " ");
            _catDefaultText = catText.text;
            catText.text = string.Format(_catDefaultText, " ");
            StartCoroutine(ShowResult());
        }

        private IEnumerator ShowResult()
        {
            yield return new WaitForSeconds(_intervalTime);

            if(StatisticsDatastore._totalClearedStage == 0)
            {
                stageText.text = string.Format(_stageDefaultText, 0);
            }
            for(int i = 0; i < StatisticsDatastore._totalClearedStage; i++)
            {
                yield return new WaitForSeconds(_shuffleTime);
                stageText.text = string.Format(_stageDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            if(StatisticsDatastore._totalCat == 0)
            {
                catText.text = string.Format(_catDefaultText, 0);
            }
            for(int i = 0; i < StatisticsDatastore._totalCat; i++)
            {
                yield return new WaitForSeconds(_shuffleTime);
                catText.text = string.Format(_catDefaultText, i + 1);
            }

            yield return new WaitForSeconds(_intervalTime);

            //取得パークを表示(未実装)
            
        }
    }
}