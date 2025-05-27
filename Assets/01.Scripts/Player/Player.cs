using Core.InputSystem;
using UnityEngine;
namespace Players
{

    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerInputReader playerInputReader { get; private set; }

        

    }
}