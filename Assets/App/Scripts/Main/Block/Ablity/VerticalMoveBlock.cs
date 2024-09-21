using UnityEngine;

namespace App.Main.Block.Ablity
{
    public class VerticalMoveBlock : MonoBehaviour, IBlockAblity
    {   
        public enum MoveState
        {
            Outbound,
            ReturnJourney
        }

        Rigidbody2D rb;
        private  Vector3 _initialPostion;
        [SerializeField] private float _verticalRange;
        private MoveState _state = MoveState.Outbound;
        [SerializeField] private float _speed;

        void Start()
        {
            _initialPostion = transform.position;
            rb = GetComponent<Rigidbody2D>();
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
                    transform.position = new Vector2(transform.position.x,_initialPostion.y + _verticalRange) ;
                }
            }
            else
            {
                rb.velocity = new Vector2(0f,-_speed);

                if(transform.position.y < _initialPostion.y - _verticalRange)
                {
                    _state = MoveState.Outbound;
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x,_initialPostion.y - _verticalRange);
                }             
            }
        }
    }
}