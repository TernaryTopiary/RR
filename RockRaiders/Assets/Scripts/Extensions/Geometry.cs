using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Geometry
    {
        private static float Average(float f1, float f2) => (f1 + f2) / 2;

        public static Vector3 Average(Vector3 vector1, Vector3 vector2) => new Vector3(Average(vector1.x, vector2.x), Average(vector1.y, vector2.y), Average(vector1.z, vector2.z));

        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            if (!vectors.Any()) throw new ArgumentException("Error: Empty collection", nameof(vectors));
            return vectors.Count() == 1 ? vectors.First() : vectors.Aggregate((vec1, vec2) => Average(vec1, vec2));
        }

        public static void SetX(this Vector3 vector, float value) => vector.Set(value, vector.y, vector.z);

        public static void SetY(this Vector3 vector, float value) => vector.Set(vector.x, value, vector.z);

        public static void SetZ(this Vector3 vector, float value) => vector.Set(vector.x, vector.y, value);

        public static void Set(this Vector3 vector, Vector2 positionXY, float positionZ = default(float)) => vector.Set(positionXY.x, positionXY.y, positionZ);
    }
}