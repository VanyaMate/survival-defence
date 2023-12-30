using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Progress
{
    public interface IUIProgress
    {
        void SetText(string text);
        void Set(float value);
        void Show();
        void Hide();
    }

    public class UIProgress : MonoBehaviour, IUIProgress
    {
        [SerializeField] private GameObject _progressFiller;
        [SerializeField] private TMP_Text _text;

        private RectTransform _rt;
        private float _x;

        private void Awake()
        {
            this._rt = this._progressFiller.GetComponent<RectTransform>();
            this._x = this._progressFiller.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
            Debug.Log(this._x);
        }

        public void SetText(string text)
        {
            this._text.text = text;
        }

        public void Set(float value)
        {
            this._rt.sizeDelta = new Vector2(this._x / 100 * value, this._rt.sizeDelta.y);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}