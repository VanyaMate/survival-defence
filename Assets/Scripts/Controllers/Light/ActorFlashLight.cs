using UnityEngine;

namespace Controllers.Light
{
    public class ActorFlashLight
    {
        private float _shakingPower;
        private Transform _light;
        private float _shakeSpeed;
        private float _rotationSpeed;
        private Vector3 _noiseOffset;
        private float _noiseScale;

        public ActorFlashLight(Transform light, float shakingPower, float shakeSpeed, float rotationSpeed,
            float noiseScale)
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
            _noiseOffset += new Vector3(1f, 1f, 1f) * _shakeSpeed * deltaTime * shakingPower;
            float x = Mathf.PerlinNoise(_noiseOffset.x, 0f) * (2f * shakingPower) - 1f;
            float y = Mathf.PerlinNoise(0f, _noiseOffset.y) * (2f * shakingPower) - 1f;
            Vector3 noise = new Vector3(x, y, 0) * _shakingPower * shakingPower;
            this._light.localPosition = noise;
            this._light.Rotate(Vector3.forward, _rotationSpeed * deltaTime);
        }
    }
}