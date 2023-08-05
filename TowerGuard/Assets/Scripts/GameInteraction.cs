using UnityEngine;

public class GameInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _visualAim;
    private PlayerInput _input;
    private Camera _mainCamera;
    private GameObject _currentVisualAim;
    private bool _isAimCreated;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.ShootPC.performed += OnShootingPC;
        //_input.Player.ShootPhone.performed += OnShootingPhone;

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ShootingOnPhone();
    }

    private void OnShootingPC(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // PC ver
        if (MainTower.Instance != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPos = hit.point;
                worldPos.y = _visualAim.transform.position.y;

                if (!_isAimCreated)
                {
                    _currentVisualAim = Instantiate(_visualAim, worldPos, Quaternion.identity);
                    _isAimCreated = true;
                }
                else
                {
                    _currentVisualAim.transform.position = worldPos;
                }
            }
        }
    }

    private void ShootingOnPhone()
    {
        // Phone ver
        if (MainTower.Instance != null)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Ended)
                {
                    Vector2 touchPos = touch.position;

                    Ray ray = _mainCamera.ScreenPointToRay(new Vector3(touchPos.x, touchPos.y, _mainCamera.nearClipPlane));
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Vector3 worldPos = hit.point;
                        worldPos.y = _visualAim.transform.position.y;

                        if (!_isAimCreated)
                        {
                            _currentVisualAim = Instantiate(_visualAim, worldPos, Quaternion.identity);
                            _isAimCreated = true;
                        }
                        else
                        {
                            _currentVisualAim.transform.position = worldPos;
                        }
                    }
                }
            }
        }
    }

    //private void OnShootingPhone(UnityEngine.InputSystem.InputAction.CallbackContext context)
    //{
    //    // Phone ver
    //    if (MainTower.Instance != null)
    //    {
    //        Vector2 inputPos = context.ReadValue<Vector2>();
    //        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(inputPos.x, inputPos.y, _mainCamera.nearClipPlane));
    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            Vector3 worldPos = hit.point;
    //            worldPos.y = _visualAim.transform.position.y;

    //            if (!_isAimCreated)
    //            {
    //                _currentVisualAim = Instantiate(_visualAim, worldPos, Quaternion.identity);
    //                _isAimCreated = true;
    //            }
    //            else
    //            {
    //                _currentVisualAim.transform.position = worldPos;
    //            }
    //        }
    //    }
    //}

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}