using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMPro.TMP_Text _title;
        [SerializeField] private TMPro.TMP_Text _amount;
        [SerializeField] private TMPro.TMP_Text _condition;
        [SerializeField] private TMPro.TMP_Text _weight;

        public void Set(SO_InventoryItem item, int amount, int condition)
        {
            this._icon.sprite = item.Icon;
            this._title.text = item.Title;
            this._amount.text = amount.ToString();
            this._condition.text = condition.ToString() + "%";
            this._weight.text = (item.Weight * amount).ToString("0.00");
        }
    }
}