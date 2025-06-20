using UnityEngine;

namespace _GardenOfDreams.Scripts.Utilities
{
    [System.Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SerializableVector3(Vector3 vector) : this(vector.x, vector.y, vector.z)
        {
        }

        public void UpdateValue(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
        public static SerializableVector3 FromVector2(Vector3 v) => new SerializableVector3(v);
    }
}