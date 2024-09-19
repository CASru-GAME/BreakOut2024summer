using System.Collections;
using System.Collections.Generic;
using App.Main.Block.Ablity;
using UnityEngine;

public class SquareMoveBlock : MonoBehaviour, IBlockAblity
{   
    public enum MoveState
    {
        Outbound,
        ReturnJourney,
    }
    public enum AxisState
    {
         X,
        Y,
    }

    MoveState _moveState = MoveState.Outbound;
    [SerializeField] AxisState _axisState;
    Rigidbody2D rb;
    private Vector3 _initialPostion;
    [SerializeField] private float _horizontalRange;
    [SerializeField] private float _verticalRange;
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
        if(_moveState == MoveState.Outbound)
        {   
            if(_axisState == AxisState.X)
            {
                rb.velocity = new Vector2(_speed,0f);
                if(transform.position.x > _initialPostion.x + _horizontalRange)
                {   
                    _axisState = AxisState.Y;
                    transform.position = new Vector2(_initialPostion.x + _horizontalRange,transform.position.y);
                    rb.velocity = new Vector2(0f,0f);
                }
            }
            else
            {
                rb.velocity = new Vector2(0f,_speed);
                if(transform.position.y > _initialPostion.y + _verticalRange)
                {   
                    _axisState = AxisState.X;
                    transform.position = new Vector2(transform.position.x,_initialPostion.y + _verticalRange);
                    rb.velocity = new Vector2(0f,0f);
                }
            }
                
            if(transform.position.x == _initialPostion.x + _horizontalRange && transform.position.y == _initialPostion.y + _verticalRange)
            {
                _moveState = MoveState.ReturnJourney;
            }
        }
        else
        {
            if(_axisState == AxisState.X)
            {
                rb.velocity = new Vector2(-_speed,0f);
                if(transform.position.x < _initialPostion.x)
                {   
                    _axisState = AxisState.Y;
                    transform.position = new Vector2(_initialPostion.x,transform.position.y);
                    rb.velocity = new Vector2(0f,0f);
                }
            }
            else
            {
                rb.velocity = new Vector2(0f,-_speed);
                if(transform.position.y < _initialPostion.y)
                {   
                    _axisState = AxisState.X;
                    transform.position = new Vector2(transform.position.x,_initialPostion.y);
                    rb.velocity = new Vector2(0f,0f);
                }
            }
                
            if(transform.position.x == _initialPostion.x && transform.position.y == _initialPostion.y)
            {
                _moveState = MoveState.Outbound;
            }
        }
    }
}
