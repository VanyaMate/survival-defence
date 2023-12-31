using System;
using Controllers;
using Controllers.Actor;
using Controllers.Character;
using Controllers.Interact;
using Controllers.Inventory;
using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ActorBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private SO_Inventory _inventoryType;

    private IActorController _actorController;
    public IActorController ActorController => this._actorController;
    public Transform Head => this._head;

    private void Awake()
    {
        this._actorController = new ActorController(
            new UnityCharacterController(
                this.GetComponent<CharacterController>(),
                new OrientationController(
                    this._head,
                    this.transform
                ),
                9.81f
            ),
            new InventoryController(
                this._inventoryType,
                new Inventory<SO_InventoryItem>()
            ),
            new InteractController()
        );
    }

    private void Update()
    {
        this._actorController.CharacterController.Update(Time.deltaTime);
    }
}