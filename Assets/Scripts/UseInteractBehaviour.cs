using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseInteractBehaviour : InteractBehaviour
{
    public override void Interact(PlayerBehaviour playerBehaviour)
    {
        Debug.Log("USE " + this._name);
    }
}