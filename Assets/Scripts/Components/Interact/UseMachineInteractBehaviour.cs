using System;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Components.Interact
{
    [Serializable]
    public class ReciepList
    {
        public SO_InventoryItem Item;
        public int Amount;
    }

    [Serializable]
    public class Reciep
    {
        public List<ReciepList> From;
        public List<ReciepList> To;
    }

    public class UseMachineInteractBehaviour : MonoBehaviour
    {
        [SerializeField] private List<Reciep> _recieps;
    }
}