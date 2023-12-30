namespace Controllers
{
    public delegate void OnCancelCallback();

    public delegate void OnSuccessCallback();

    public delegate void OnProgressCallback(float progress);

    public interface IProcessController
    {
        public bool InProcess();

        public void Start(
            float timeToProgress,
            OnProgressCallback onProgressCallback,
            OnCancelCallback onCancelCallback,
            OnSuccessCallback onSuccessCallback
        );

        public float Tick(float deltaTime);

        public void Stop();
    }

    public class ProcessController : IProcessController
    {
        private OnProgressCallback _onProgressCallback;
        private OnSuccessCallback _onSuccessCallback;
        private OnCancelCallback _onCancelCallback;
        private bool _finished = true;
        private float _targetProgress = 0f;
        private float _progress = 0f;

        public bool InProcess()
        {
            return !this._finished;
        }

        public float Progress()
        {
            return 100 / this._targetProgress * this._progress;
        }

        public void Start(
            float timeToProgress,
            OnProgressCallback onProgressCallback,
            OnCancelCallback onCancelCallback,
            OnSuccessCallback onSuccessCallback
        )
        {
            this._onProgressCallback = onProgressCallback;
            this._onCancelCallback = onCancelCallback;
            this._onSuccessCallback = onSuccessCallback;
            this._targetProgress = timeToProgress;
            this._progress = 0;
            this._finished = false;
        }

        public float Tick(float deltaTime)
        {
            this._progress += deltaTime;
            if ((this._progress >= this._targetProgress) && !this._finished)
            {
                this._finished = true;
                this._onProgressCallback(100);
                this._onSuccessCallback();
                return 100;
            }
            else if (!this._finished)
            {
                float progress = this.Progress();
                this._onProgressCallback(progress);
                return progress;
            }

            return 0;
        }

        public void Stop()
        {
            if (!this._finished)
            {
                this._finished = true;
                this._onCancelCallback();   
            }
        }
    }
}