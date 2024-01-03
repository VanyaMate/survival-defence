using System.Collections;
using Components.Interact;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    [SerializeField] private ToggleInteractableItemComponent _toggleInteractable;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _angle = 90f;
    private bool _opened;
    private Vector3 _openDoorAngle;
    private Vector3 _closedDoorAngle;
    private Coroutine _coroutine;

    private void Awake()
    {
        this._toggleInteractable = this.GetComponent<ToggleInteractableItemComponent>();
        this._opened = this._toggleInteractable.State;
        if (this._opened)
        {
            this._openDoorAngle = transform.rotation.eulerAngles;
            this._closedDoorAngle = transform.rotation.eulerAngles - new Vector3(0, this._angle, 0);
        }
        else
        {
            this._closedDoorAngle = transform.rotation.eulerAngles;
            this._openDoorAngle = transform.rotation.eulerAngles + new Vector3(0, this._angle, 0);
        }

        this._Toggle(this._opened);
        this._toggleInteractable.Emitter.Subscribe(this._Toggle);
    }

    private void _Toggle(bool state)
    {
        this._opened = state;
        if (this._coroutine != null)
        {
            StopCoroutine(this._coroutine);
        }

        this._coroutine = StartCoroutine(this._Rotate());
    }

    private IEnumerator _Rotate()
    {
        float rotateAngle = 0f;
        float currentAngle = transform.rotation.eulerAngles.y;
        float targetAngle = this._opened ? this._openDoorAngle.y : this._closedDoorAngle.y;
        float maxAngle = Mathf.Max(currentAngle, targetAngle);
        float minAngle = Mathf.Min(currentAngle, targetAngle);
        rotateAngle = maxAngle - minAngle;

        while (rotateAngle > 0)
        {
            float angle = Mathf.Max(10f * Time.deltaTime, rotateAngle * Time.deltaTime);
            rotateAngle = Mathf.Max(0, rotateAngle - angle);

            transform.RotateAround(this._pivot.position, Vector3.up, this._opened ? -angle : angle);
            yield return 0;
        }
    }
}