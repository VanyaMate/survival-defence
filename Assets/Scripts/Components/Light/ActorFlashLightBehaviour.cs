using System;
using Controllers.Light;
using UnityEngine;

namespace Components.Light
{
    public class ActorFlashLightBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Light _light;

        private IActorFlashLight _flashlight;

        private void Awake()
        {
            this._flashlight = new ActorFlashLight(this._light.gameObject.activeSelf);
            this._flashlight.Subscribe(this._onChange);
        }

        public void Toggle()
        {
            this._flashlight.Toggle();
        }

        public void Power(bool state)
        {
            this._flashlight.Power(state);
        }

        private void OnDestroy()
        {
            this._flashlight.Unsubscribe(this._onChange);
        }

        private void _onChange(bool state)
        {
            this._light.gameObject.SetActive(state);
        }
    }
}