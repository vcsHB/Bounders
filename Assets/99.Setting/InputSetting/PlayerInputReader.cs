using UnityEngine;
namespace Core.InputSystem
{

    public class PlayerInputReader : ScriptableObject
    {
        public Vector2 CurrentMoveDirection { get; protected set; }
        [field: SerializeField] public bool CanControl { get; protected set; } = true;

        public virtual void SetControlable(bool value)
        {
            CanControl = value;
        }
    }
}