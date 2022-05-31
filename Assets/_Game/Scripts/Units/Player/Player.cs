using Astogames.Managers;
using UnityEngine;

[RequireComponent( typeof(Rigidbody) )]
public class Player : UnitBase
{
    private bool               _autoMove;
    private bool               _canMove;
    private PlayerInputActions _playerInputActions;
    private Rigidbody          _rigidbody;

    private void Awake()
    {
        base.Awake();
        _rigidbody          = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _canMove               = false;
        _autoMove              = true;
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if ( _autoMove )
            AutoMove();
    }

    private void FixedUpdate()
    {
        if ( _canMove )
            Move();
        // Rotate();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    private void OnCollisionEnter( Collision collision )
    {
        var defaultCollisionDamage = 1;

        switch ( LayerMask.LayerToName( collision.gameObject.layer ) ) {
            case "Ground":
                defaultCollisionDamage = 1000;
                break;
            default:
                return;
        }

        TakeDamage( defaultCollisionDamage );
    }

    private void AutoMove()
    {
        transform.position += transform.forward * Time.deltaTime;
        // _rigidbody.AddForce( new Vector3(
        //     0,
        //     0,
        //     Stats.Speed ) );
    }


    private void Move()
    {
        _rigidbody.AddForce( new Vector3(
            _playerInputActions.Player.Move.ReadValue<Vector2>().x * Stats.Speed,
            0,
            0 ) );
    }

    private void Rotate()
    {
        transform.Rotate( new Vector3( _playerInputActions.Player.Move.ReadValue<Vector2>().x * Stats.Speed, 0, 0 ) );
    }

    protected override void OnStateChanged( GameState newState )
    {
        switch ( newState ) {
            case GameState.IntroCinematic:
                _canMove               = false;
                _autoMove              = true;
                _rigidbody.isKinematic = true;
                break;
            case GameState.Gameplay:
                _canMove               = true;
                _autoMove              = false;
                _rigidbody.isKinematic = false;
                _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                break;
            default:
                _canMove               = false;
                _autoMove              = false;
                _rigidbody.constraints = RigidbodyConstraints.None;
                break;
        }
    }

    protected override void Die()
    {
        GameManager.Instance.ChangeState( GameState.Lose );
        Destroy( gameObject );
    }

    public void ReceivePush( float push )
    {
        _rigidbody.AddForce( new Vector3(
            push,
            0,
            0 ) );
    }
}