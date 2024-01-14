using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "items/produce", fileName = "SO_Produce", order = 0)]
    public class SO_Produce : ScriptableObject
    {
        public string Title;
        public List<SO_Reciep> Recieps;
    }   
}