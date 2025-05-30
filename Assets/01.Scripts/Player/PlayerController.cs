using UnityEngine;

namespace Players
{


    public class PlayerController : MonoBehaviour
    {
        private ControlablePlayer _player;
        [SerializeField] private Vector2 _zAxisClampRange;
        [SerializeField] private float _moveSpeed;

        private void Awake()
        {
            _player = GetComponent<ControlablePlayer>();
        }

        private void FixedUpdate()
        {
            float moveZ = _player.playerInputReader.CurrentMoveDirection.y * _moveSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = transform.localPosition;
            newPosition.z += moveZ;

            newPosition.z = Mathf.Clamp(newPosition.z, _zAxisClampRange.x, _zAxisClampRange.y);

            transform.localPosition = newPosition;
        }
    }


}