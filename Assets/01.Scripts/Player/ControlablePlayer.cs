using Core.InputSystem;
using UnityEngine;
namespace Players
{

    public class ControlablePlayer : Player
    {
        [field: SerializeField] public PlayerInputReader playerInputReader { get; private set; }

    }
}