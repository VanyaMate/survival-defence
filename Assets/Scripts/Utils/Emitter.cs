using System.Collections.Generic;
using Debug = UnityEngine.Debug;

namespace Utils
{
    public delegate void EmitterCallback<TData>(TData data);

    public interface IEmitter<TData>
    {
        void Subscribe(EmitterCallback<TData> callback);
        void Unsubscribe(EmitterCallback<TData> callback);
        void Invoke(TData data);
    }

    public class Emitter<TData> : IEmitter<TData>
    {
        private List<EmitterCallback<TData>> _callbacks = new List<EmitterCallback<TData>>();


        public void Subscribe(EmitterCallback<TData> callback)
        {
            this._callbacks.Add(callback);
        }

        public void Unsubscribe(EmitterCallback<TData> callback)
        {
            this._callbacks.Remove(callback);
        }

        public void Invoke(TData data)
        {
            this._callbacks.ForEach((callback) => callback(data));
        }
    }
}