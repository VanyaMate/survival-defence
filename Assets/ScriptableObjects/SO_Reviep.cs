using Controllers.Reciep;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "items/reciep", fileName = "SO_Reciep", order = 0)]
    public class SO_Reciep : ScriptableObject
    {
        public string Title;
        public float TimeToFinish;
        public Reciep Reciep;
    }
}