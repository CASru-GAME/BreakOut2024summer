using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Main.Stage;

namespace App.Main.Block
{
    public class MoveBlockFall : MonoBehaviour,IFall
    {
        [SerializeField] private float[] _fallingSpeed;
        public List<IFall> HitBlockList = new List<IFall>();
        public StageSystem stageSystem{ get; private set; }
        private Vector3 initialPosition;
        private GameObject _block;

        void Update()
        {   
            Fall();
        }

        public void Initialized(GameObject Block,StageSystem stageSystem)
        {
            initialPosition = transform.position;
            _block = Block;
            this.stageSystem = stageSystem;
        }
        
        public void Fall()
        {   
            if(_block == null)
            Destroy(this.gameObject);

            if(transform.position.y > -2.8f && HitBlockList.Count == 0 && _block != null)
            {
                transform.position += new Vector3(0f,-_fallingSpeed[stageSystem.CurrentStageNumberID - 10],0f);
                _block.transform.position += new Vector3(0f,-_fallingSpeed[stageSystem.CurrentStageNumberID - 10],0f);

            }
        }

        void OnTriggerEnter2D(Collider2D other) 
        {   
            IFall block = other.GetComponent<IFall>();
            if(block != null && !HitBlockList.Contains(block))
            HitBlockList.Add(block);
        }

        void OnTriggerExit2D(Collider2D other) 
        {
            IFall block = other.GetComponent<IFall>();
            if(block != null && HitBlockList.Contains(block))
            HitBlockList.Remove(block);
        }
    }
}