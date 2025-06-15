using UnityEngine;
namespace ObjectManage
{

    public class CrownCam : MonoBehaviour
    {
        private Camera _camera;
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _camera.clearFlags = CameraClearFlags.SolidColor;
            _camera.backgroundColor = new Color(0f, 0f, 0f, 0f); // 투명한 검정

        }
    }
}