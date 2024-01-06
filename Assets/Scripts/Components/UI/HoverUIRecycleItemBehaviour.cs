using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class HoverUIRecycleItemBehaviour : UIElementBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void Set(SO_InventoryItem item, int needAmount, int inInventory)
        {
            this._text.text = $"{inInventory} / {needAmount} {item.Title}";
            this._image.color = needAmount <= inInventory ? Color.green : Color.red;
        }
    }
}