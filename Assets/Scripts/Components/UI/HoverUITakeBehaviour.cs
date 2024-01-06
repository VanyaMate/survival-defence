using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class HoverUITakeBehaviour : UIElementBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Set(SO_InventoryItem item, int amount)
        {
            this._text.text = $"({amount}) {item.Title}";
        }
    }
}