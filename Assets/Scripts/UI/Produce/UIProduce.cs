using System.Collections.Generic;
using Controllers.UI;
using ScriptableObjects;
using TMPro;
using UI.Progress;
using UnityEngine;

namespace UI.Produce
{
    public class UIProduce : UIElementBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Transform _container;
        [SerializeField] private UIProduceItem _prefab;

        public void Set(SO_Produce produce)
        {
            this._Clear();
            this._title.text = produce.Title;
            this._Render(produce.Recieps);
            this.Show();
        }

        private void _Render(List<SO_Reciep> recieps)
        {
            recieps.ForEach(
                (reciep) =>
                {
                    UIProduceItem item = Instantiate(this._prefab, this._container);
                    item.Set(reciep);
                }
            );
        }

        private void _Clear()
        {
            for (int i = 0; i < this._container.transform.childCount; i++)
            {
                Destroy(this._container.transform.GetChild(i).gameObject);
            }
        }
    }
}