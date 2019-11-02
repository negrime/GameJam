using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInTable : MonoBehaviour
{
    [System.Serializable]
    public struct ProsuctInTable
    {
        public Sprite Sprite;
        public string Description;
        public int Damage;
        public int Count;

        public override string ToString()
        {
            string str = "";
            str += "Урон: " + Damage + "\n";
            str += "Кол-во: " + Count;

            return str;
        }
    }
}
