using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public abstract class InteractBehaviour : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _preInteractText;

    public abstract void Interact(PlayerBehaviour playerBehaviour);
}