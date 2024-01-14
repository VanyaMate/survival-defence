using System;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Progress
{
    [RequireComponent(typeof(Button))]
    public class UIProduceItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _timeToProduce;
        [SerializeField] private Button _produceButton;
        [SerializeField] private UIProducePreview _preview;

        private Button _item;

        private void Start()
        {
            this._item = this.GetComponent<Button>();
        }

        public void Set(SO_Reciep reciep)
        {
            this._title.text = reciep.Title;
            this._timeToProduce.text = reciep.TimeToFinish.ToString(CultureInfo.CurrentCulture);
            /*this._item.onClick.AddListener(
                () => { Debug.Log(reciep.Title); }
            );*/
        }
    }
}