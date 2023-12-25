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

        public void Set(SO_InventoryItem item, int amount)
        {
            this._icon.sprite = item.Icon;
            this._title.text = item.Title;
            this._amount.text = amount.ToString();
        }
    }
}