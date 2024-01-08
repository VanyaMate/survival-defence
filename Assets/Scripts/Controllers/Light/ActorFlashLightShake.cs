using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Light
{
    public interface IActorFlashLightShake
    {
        void Tick(float deltaTime, float shakePower);
    }

    public class ActorFlashLightShake : IActorFlashLightShake
    {
        private float _shakingPower;
        private Transform _light;
        private float _shakeSpeed;
        private float _rotationSpeed;
        private Vector3 _noiseOffset;
        private float _noiseScale;

        public ActorFlashLightShake(
            Transform light,
            float shakingPower,
            float shakeSpeed,
            float rotationSpeed,
            float noiseScale
        )
        {
            this._light = light;
            this._shakingPower = shakingPower;
            this._shakeSpeed = shakeSpeed;
            this._rotationSpeed = rotationSpeed;
            this._noiseScale = noiseScale;
            this._noiseOffset = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        }

        public void Tick(float deltaTime, float shakingPower)
        {
            this._noiseOffset += new Vector3(1f, 1f, 1f) * _shakeSpeed * deltaTime * shakingPower * this._noiseScale;
            float x = Mathf.PerlinNoise(this._noiseOffset.x, 0f) * (2f * shakingPower) - 1f;
            float y = Mathf.PerlinNoise(0f, this._noiseOffset.y) * (2f * shakingPower) - 1f;
            Vector3 noise = new Vector3(x, y, 0) * this._shakingPower * shakingPower;
            this._light.localPosition = noise;
            this._light.Rotate(Vector3.forward, this._rotationSpeed * deltaTime);
        }
    }
}