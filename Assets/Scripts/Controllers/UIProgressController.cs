using UI.Progress;
using System.Timers;
using UnityEngine;

namespace Controllers
{
    public interface IUIProgressController
    {
        void SetText(string text);
        void Progress(float value);
        void Show();
        void Hide();
    }

    public class UIProgressController : IUIProgressController
    {
        private IUIProgress _bar;
        private Timer _timer;

        public UIProgressController(IUIProgress bar)
        {
            this._bar = bar;
        }

        public void Progress(float value)
        {
            this._bar.Show();
            this._bar.Set(value);
        }

        public void SetText(string text)
        {
            this._bar.SetText(text);
        }

        public void Show()
        {
            this._bar.Show();
        }

        public void Hide()
        {
            this._bar.Hide();
        }
    }
}