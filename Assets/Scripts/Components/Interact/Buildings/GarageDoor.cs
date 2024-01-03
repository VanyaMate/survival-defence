using System;
using System.Collections;
using System.Collections.Generic;
using Components.Interact;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    [SerializeField] private bool _opened;
    [SerializeField] private float _speed;
    [SerializeField] private List<UseInteractableItemComponent> _interact;
    private Renderer _rc;
    private float _openedY;
    private float _closedY;
    private Coroutine _coroutine;

    private void Awake()
    {
        this._rc = GetComponent<Renderer>();
        if (this._opened)
        {
            this._openedY = transform.position.y;
            this._closedY = this._openedY - this._rc.bounds.size.y;
        }
        else
        {
            this._closedY = transform.position.y;
            this._openedY = this._closedY + this._rc.bounds.size.y;
        }

        this._ChangeState();
        this._interact.ForEach((item) => item.Emitter.Subscribe(this._Toggle));
    }

    private void _Toggle(bool state)
    {
        this._opened = !this._opened;

        if (this._coroutine != null)
        {
            StopCoroutine(this._coroutine);
        }

        this._coroutine = StartCoroutine(this._ChangeState());
    }

    private IEnumerator _ChangeState()
    {
        if (this._opened)
        {
            while (transform.position.y < this._openedY)
            {
                float translateY = Mathf.Min(this._openedY - transform.position.y, this._speed * Time.deltaTime);
                transform.Translate(new Vector3(0, translateY, 0));
                yield return 0;
            }
        }
        else
        {
            while (transform.position.y > this._closedY)
            {
                float translateY = Mathf.Min(transform.position.y - this._closedY, this._speed * Time.deltaTime);
                transform.Translate(new Vector3(0, -translateY, 0));
                yield return 0;
            }
        }
    }
}