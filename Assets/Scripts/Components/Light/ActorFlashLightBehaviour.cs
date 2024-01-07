using System;
using Controllers.Light;
using UnityEngine;

namespace Components.Light
{
    public class ActorFlashLightBehaviour : MonoBehaviour
    {
        [SerializeField] private float _shakingPower = .1f;
        [SerializeField] private float _shakingSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _noiseScale = 1f;

        private ActorFlashLight _actorFlashLight;

        private void Awake()
        {
            this._actorFlashLight = new ActorFlashLight(
                transform,
                this._shakingPower,
                this._shakingSpeed,
                this._rotationSpeed,
                this._noiseScale
            );
        }

        private void Update()
        {
            this._actorFlashLight.Tick(Time.deltaTime, 1);
        }
    }
}