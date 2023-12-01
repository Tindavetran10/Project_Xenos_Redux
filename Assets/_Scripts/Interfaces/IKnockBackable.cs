using UnityEngine;

namespace _Scripts.Interfaces
{
    public interface IKnockBackable
    {
        void KnockBack(Vector2 angle, float strength, int direction);
    }
}