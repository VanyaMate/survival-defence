using System;
using Controllers.Light;
using UnityEngine;

namespace Components.Light
{
    public class ActorFlashLightShakeBehaviour : MonoBehaviour
    {
        [SerializeField] private float _shakingPower = .1f;
        [SerializeField] private float _shakingSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _noiseScale = 1f;

        private IActorFlashLightShake _actorFlashLightShake;

        private void Awake()
        {
            this._actorFlashLightShake = new ActorFlashLightShake(
                transform,
                this._shakingPower,
                this._shakingSpeed,
                this._rotationSpeed,
                this._noiseScale
            );
        }

        private void Update()
        {
            this._actorFlashLightShake.Tick(Time.deltaTime, 2);
        }
    }
}