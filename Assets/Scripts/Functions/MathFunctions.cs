using UnityEngine;

namespace MathFunctions
{
    public class MathFunc
    {
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
        {
            float t = Mathf.InverseLerp(iMin, iMax, value);// 0, 2, 0.5 == 0.25f;
            return Mathf.Lerp(oMin, oMax, t); //0, 20, 0.25f == 5f;
        }
    }

}
