using Unity.Burst.Intrinsics;
using UnityEngine;

namespace App.Main.Block.Ablity
{
    public class DiagonalMove : MonoBehaviour, IBlockAblity
    {
        public enum MoveState
        {
            Outbound,
            ReturnJourney
        }

        Rigidbody2D rb;
        private Vector3 _initialPostion;
        [SerializeField] private float _horizontalRange;
        [SerializeField] private float _verticalRange;
        private MoveState _state = MoveState.Outbound;
        [SerializeField] private  float _speed;
        private bool isHorizon;
        private bool isVerton;
        
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
                rb.velocity = new Vector2(_horizontalRange,_verticalRange).normalized * _speed;

                if(transform.position.x > _initialPostion.x + _horizontalRange)
                {
                    rb.velocity = new Vector2(0f,rb.velocity.y);
                    transform.position = new Vector2(_initialPostion.x + _horizontalRange,transform.position.y);
                    isHorizon = true;
                }
                if(transform.position.y > _initialPostion.y + _verticalRange)
                {                   
                    rb.velocity = new Vector2(rb.velocity.x,0f);
                    transform.position = new Vector2(transform.position.x,_initialPostion.y + _verticalRange);
                    isVerton = true;
                }
                if(isHorizon && isVerton)
                {
                    _state = MoveState.ReturnJourney;
                    isHorizon = false;
                    isVerton = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(_horizontalRange,_verticalRange).normalized * -_speed;

                if(transform.position.x < _initialPostion.x - _horizontalRange)
                {
                    rb.velocity = new Vector2(0f,rb.velocity.y);
                    transform.position = new Vector2(_initialPostion.x - _horizontalRange,transform.position.y);
                    isHorizon = true;
                }
                if(transform.position.y < _initialPostion.y - _verticalRange)
                {                   
                    rb.velocity = new Vector2(rb.velocity.x,0f);
                    transform.position = new Vector2(transform.position.x,_initialPostion.y - _verticalRange);
                    isVerton = true;
                }
                if(isHorizon && isVerton)
                {
                    _state = MoveState.Outbound;
                    isHorizon = false;
                    isVerton = false;
                }
            }
        }    
    }
}
