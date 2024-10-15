using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Stage
{
    public class HoleWall : MonoBehaviour
    {
        /*[SerializeField] private GameObject _rightCelling;
        [SerializeField] private GameObject _leftCelling;
        
        [SerializeField] private GameObject _UpLeftWall;
        [SerializeField] private GameObject _DownLeftWall;*/

        [SerializeField] private GameObject _hole;
        [SerializeField] private StageSystem _stageSystem;


        /// <summary>
        /// World5の壁に穴をつくる
        /// </summary>
        /*public void CreateHole(int _stageId)
        {
            if(_stageId == 13)
            {
                _rightCelling.transform.position += new Vector3(0.2f, 0,0);
                _leftCelling.transform.position += new Vector3(-0.2f, 0,0);
            }
            else if(_stageId == 14)
            {
                _rightCelling.transform.position += new Vector3(0.4f, 0,0);
                _leftCelling.transform.position += new Vector3(-0.4f, 0,0);
            }
            else if(_stageId == 15)
            {
                _rightCelling.transform.position += new Vector3(0.3f, 0,0);
                _leftCelling.transform.position += new Vector3(-0.3f, 0,0);
                _DownLeftWall.transform.position += new Vector3(0f,-0.8f,0f);
            }
        }*/

        public void CreateHole(int _stageId)
        {
            if(_stageId == 13)
            {
                var hole = Instantiate(_hole,new Vector3(-2.25f,5f,0f),Quaternion.identity);
                hole.transform.localScale = new Vector3(0.8f,1f,1f);
                hole.GetComponent<Hole>().Initialized(_stageSystem);
            }

            if(_stageId == 14)
            {
                var hole = Instantiate(_hole,new Vector3(-2.25f,5f,0f),Quaternion.identity);
                hole.transform.localScale = new Vector3(1.2f,1f,1f);
                hole.GetComponent<Hole>().Initialized(_stageSystem);
            }

            if(_stageId == 15)
            {
                var hole = Instantiate(_hole,new Vector3(-2.25f,5f,0f),Quaternion.identity);
                hole.transform.localScale = new Vector3(0.8f,1f,1f);
                hole.GetComponent<Hole>().Initialized(_stageSystem);

                hole = Instantiate(_hole,new Vector3(-5f,1.5f,0f),Quaternion.identity);
                hole.transform.localScale = new Vector3(1f,1.2f,1f);
                hole.GetComponent<Hole>().Initialized(_stageSystem);
            }
        }
    }
}
