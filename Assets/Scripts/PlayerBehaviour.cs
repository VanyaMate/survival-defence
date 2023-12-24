using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance { get; private set; }

    [SerializeField] private ActorBehaviour _actor;

    [Header("Sens")] [SerializeField] [Range(100, 600)]
    private float _x_sens;

    [SerializeField] [Range(100, 600)] private float _y_sens;

    public ActorBehaviour CurrentActor => this._actor;

    private void Awake()
    {
        PlayerBehaviour.instance = this;
    }

    private void Update()
    {
        Vector2 mouseMoveDirection = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        Vector2 moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        bool jump = Input.GetKeyDown(KeyCode.Space);

        if (jump)
        {
            this._actor.Jump();
        }

        this._actor.MoveDirection(moveDirection);
        this._actor.Orientation(mouseMoveDirection);
    }
}