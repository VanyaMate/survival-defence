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

    public class UseHandMachineInteractBehaviour : InteractBehaviour
    {
        [SerializeField] private List<SO_Reciep> _recieps;

        public override InteractBehaviour Interact(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("OPEN MENU");
            return this;
        }

        public override InteractBehaviour ProcessInteract(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("nothing");
            return this;
        }

        public override void CancelInteract(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("cancel");
        }

        public override void OnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("USE_HAND_MACHINE_:ON_HOVER");
        }

        public override void OnUnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("USE_HAND_MACHINE_:ON_UN_HOVER");
        }
    }
}