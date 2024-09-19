using UnityEngine;

namespace App.Main.Block.Ablity
{
    public class HorizontalMoveBlock : MonoBehaviour, IBlockAblity
    {
        public enum MoveState
        {
            Outbound,
            ReturnJourney
        }

        Rigidbody2D rb;
        private Vector3 _initialPostion;
        [SerializeField] private float _horizontalRange;
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
                rb.velocity = new Vector2(_speed,0f);

                if(transform.position.x > _initialPostion.x + _horizontalRange)
                {
                    _state = MoveState.ReturnJourney;
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(_initialPostion.x + _horizontalRange,transform.position.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(-_speed,0f);

                if(transform.position.x < _initialPostion.x - _horizontalRange)
                {
                    _state = MoveState.Outbound;
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(_initialPostion.x - _horizontalRange,transform.position.y);
                }             
            }    
        }
    }
}