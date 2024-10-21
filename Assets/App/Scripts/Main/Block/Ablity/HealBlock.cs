using System.Collections.Generic;
using App.Main.Stage;
using UnityEngine;

namespace App.Main.Block.Ablity
{
    public class HealBlock : MonoBehaviour, IBlockAblity
    {   
        List<IBlock> HitBlockList = new List<IBlock>();
        [SerializeField] int[] _healAmount;
        [SerializeField] float[] _coolDown;
        private float _currentCoolDonw;
        int _worldNumberID;
    
        void Start()
        {
            _worldNumberID = transform.parent.GetComponent<IBlock>().StageSystem.CurrentWorldNumberID;
        }
        void Update()
        {
            _currentCoolDonw += Time.deltaTime;
            if( _currentCoolDonw > _coolDown[_worldNumberID - 1] )
            {   
                ActivateAblity();
                _currentCoolDonw = 0f;
            }
        }

        public void ActivateAblity()
        {   
            for(int i = 0; i < HitBlockList.Count; i++)
            HitBlockList[i].Healed(_healAmount[_worldNumberID - 1]);
        }

        void OnTriggerEnter2D(Collider2D other) 
        {   
            IBlock block = other.GetComponent<IBlock>();
            if(block != null && !HitBlockList.Contains(block))
            HitBlockList.Add(block);
        }

        void OnTriggerExit2D(Collider2D other) 
        {
            IBlock block = other.GetComponent<IBlock>();
            if(block != null && HitBlockList.Contains(block))
            HitBlockList.Remove(block);
        }
    }
}