using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    private PlayerController _playerController;
    
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private float _lastDashTime;
    
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private bool CanDash()
    {
        return Time.time >= _lastDashTime + dashCooldown;
    }
    
    public void Dash(InputAction.CallbackContext context)
    {
        if ((context.performed && !_playerController.isDashing && CanDash()) && _playerController.lastMovementDirection != Vector3.zero)
        {
            if (_playerController.movement.magnitude > 0.01f)
            {
                StartCoroutine(PerformDash());
            }
        }
    }

    IEnumerator PerformDash()
    {
        _lastDashTime = Time.fixedTime;
        Vector3 dashDirection = _playerController.lastMovementDirection.normalized;
        _playerController.isDashing = true;
        
        while (Time.fixedTime < _lastDashTime + dashTime)
        {
            // Применение силы для рывка (если нет препятствий)
            _playerController.rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
            if (!_playerController.isDashing)
                yield break;

            yield return null;
        }
        
        _playerController.isDashing = false;
        _playerController.playerAnimations.DashAnimation(_playerController.dir,_playerController.isDashing);
        _lastDashTime = Time.fixedTime;
    }
}
