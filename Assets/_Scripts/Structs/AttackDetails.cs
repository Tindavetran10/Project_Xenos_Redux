using UnityEngine;

namespace _Scripts.Structs
{
    [System.Serializable]
    public class WeaponAttackDetails
    {
        public string attackName;
        public float movementSpeed;
        public float damageAmount;

        public float knockBackStrength;
        public Vector2 knockBackAngle;
    }
}