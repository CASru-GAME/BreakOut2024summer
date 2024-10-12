using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block.Ablity
{
    public class ScreenOffVerticalMove : MonoBehaviour
    {
        public enum MoveState
        {
            Outbound,
            ReturnJourney
        }

        Rigidbody2D rb;
        private Vector3 _initialPostion;
        [SerializeField] private float _verticalRange;
        private MoveState _state = MoveState.Outbound;
        [SerializeField] private  float _speed;
            
        void Start()
        {   
            rb = GetComponent<Rigidbody2D>();
            _initialPostion = transform.position;
        }

        void Update()
        {
            ActivateAblity();
        }

        public void ActivateAblity()
        {   
            if(_state == MoveState.Outbound)
            {
                rb.velocity = new Vector2(0f,_speed);

                if(transform.position.y > _initialPostion.y + _verticalRange)
                {
                    _state = MoveState.ReturnJourney;
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x,_initialPostion.y + _verticalRange);
                }
            }
            else if(_state == MoveState.ReturnJourney)
            {
                rb.velocity = new Vector2(0f,-_speed);

                if(transform.position.y < _initialPostion.y)
                {
                    _state = MoveState.Outbound;
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x,_initialPostion.y);
                }
            }
        }
    }
}