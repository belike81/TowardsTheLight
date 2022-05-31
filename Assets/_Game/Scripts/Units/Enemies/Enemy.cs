using System.Collections;
using Astogames.Managers;
using UnityEngine;

[RequireComponent( typeof(Rigidbody) )]
public class Enemy : UnitBase
{
    [SerializeField]
    private float directionChangeTime = 2;
    private float     _currentDirection = 1;
    private float     _currentSideForce = 1;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine( DirectionChanger() );
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce( new Vector3(
            _currentSideForce,
            0,
            Stats.Speed * -1 ) );
    }

    private void OnCollisionEnter( Collision collision )
    {
        var defaultCollisionDamage = 1;

        switch ( LayerMask.LayerToName( collision.gameObject.layer ) ) {
            case "Ground":
                defaultCollisionDamage = 1000;
                break;
            case "Player":
                if ( collision.gameObject.TryGetComponent( out Player player ) )
                    HandlePlayerCollision( player );
                break;
            default:
                return;
        }

        TakeDamage( defaultCollisionDamage );
    }

    private void HandlePlayerCollision( Player player )
    {
        var sideModifier = player.gameObject.transform.position.x - transform.position.x > 0 ? 1 : -1;
        player.ReceivePush( Stats.Power * sideModifier );
    }


    private IEnumerator DirectionChanger()
    {
        while ( true ) {
            _currentSideForce = _currentDirection * Stats.Speed;
            if ( _currentSideForce is 0 ) _currentSideForce = 1;
            yield return new WaitForSeconds( directionChangeTime );
            _currentDirection *= -1;
        }
    }

    protected override void Die()
    {
        UnitManager.Instance.RemoveFromPool( this );
    }

    protected override void OnStateChanged( GameState state ) { }
}