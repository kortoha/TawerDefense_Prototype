using UnityEngine;

public class Aim_Visual : MonoBehaviour
{
    private const string SHOOTING_PARAMETR = "IsShooting";

    private Animator _amimator;

    private void Start()
    {
        _amimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(MainTower.Instance == null)
        {
            _amimator.SetBool(SHOOTING_PARAMETR, false);
        }
    }
}
