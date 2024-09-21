using System.Collections;
using System.Collections.Generic;
using App.Main.Block.Ablity;
using UnityEngine;

public class CircleMoveBlock : MonoBehaviour, IBlockAblity
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void Update()
    {
        ActivateAblity();
    }

    public void ActivateAblity()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(_radius * _speed * Mathf.Sin(Time.time * _speed),_radius * _speed * Mathf.Cos(Time.time * _speed));
    }
}
