using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Stage
{
    public class HoleWall : MonoBehaviour
    {
        [SerializeField] private GameObject _hole;
        [SerializeField] private StageSystem _stageSystem;

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
