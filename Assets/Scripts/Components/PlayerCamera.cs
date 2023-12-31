using System;
using UnityEngine;

namespace Components
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerBehaviour _player;

        private void Update()
        {
            this._camera.transform.position = this._player.CurrentActor.Head.transform.position;
            this._camera.transform.rotation = this._player.CurrentActor.Head.transform.rotation;
        }
    }
}