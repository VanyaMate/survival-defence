using UnityEngine;
using Utils;

namespace Controllers.Light
{
    public interface IActorFlashLight : IEmitter<bool>
    {
        void Toggle();
        void Power(bool state);
    }

    public class ActorFlashLight : Emitter<bool>, IActorFlashLight
    {
        private bool _state;

        public ActorFlashLight(bool state)
        {
            this._state = state;
        }

        public void Toggle()
        {
            this._state = !this._state;
            this.Invoke(this._state);
        }

        public void Power(bool state)
        {
            this._state = state;
            this.Invoke(this._state);
        }
    }
}