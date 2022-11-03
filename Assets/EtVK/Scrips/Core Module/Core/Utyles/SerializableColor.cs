using UnityEngine;

namespace EtVK.Core.Utyles
{
    [System.Serializable] public struct SerializableColor
    {
        public float Red { get; set; }
        public float Blue { get; set; }
        public float Green { get; set; }

        public SerializableColor(Color color) : this()
        {
            Red = color.r;
            Blue = color.b;
            Green = color.g;
        }

        public SerializableColor(float red, float blue, float green)
        {
            Red = red;
            Blue = blue;
            Green = green;
        }
    }
}