using UnityEngine;
namespace Core.InputSystem
{

    public class PlayerInputReader : ScriptableObject
    {
        public Vector2 CurrentMoveDirection { get; protected set; }

    }
}