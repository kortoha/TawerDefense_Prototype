using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _visualAim;
    [SerializeField] private AudioSource _touchSound;

    private PlayerInput _input;
    private Camera _mainCamera;
    private GameObject _currentVisualAim;
    private bool _isAimCreated;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.ShootPC.performed += OnShootingPC;

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsPointerOverUIObject())
        {
            ShootingOnPhone();
        }
    }

    private void OnShootingPC(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!IsPointerOverUIObject())
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            int ignoreLayersMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Tower"));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayersMask))
            {
                Vector3 worldPos = hit.point;
                worldPos.y = _visualAim.transform.position.y;

                if (!_isAimCreated)
                {
                    _currentVisualAim = Instantiate(_visualAim, worldPos, Quaternion.identity);
                    _touchSound.Play();
                    _isAimCreated = true;
                }
                else
                {
                    _currentVisualAim.transform.position = worldPos;
                    _touchSound.Play();
                }
            }
        }
    }

    private void ShootingOnPhone()
    {
        if (MainTower.Instance != null)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    Vector2 touchPos = touch.position;

                    Ray ray = _mainCamera.ScreenPointToRay(new Vector3(touchPos.x, touchPos.y, _mainCamera.nearClipPlane));

                    int ignoreLayersMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Tower"));
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayersMask))
                    {
                        Vector3 worldPos = hit.point;
                        worldPos.y = _visualAim.transform.position.y;

                        if (!_isAimCreated)
                        {
                            _touchSound.Play();
                            _currentVisualAim = Instantiate(_visualAim, worldPos, Quaternion.identity);
                            _isAimCreated = true;
                        }
                        else
                        {
                            _currentVisualAim.transform.position = worldPos;
                            _touchSound.Play();
                        }
                    }
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}