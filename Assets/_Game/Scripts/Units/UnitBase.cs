using Astogames.Managers;
using Astogames.Scriptables;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public EnemyType EnemyType { get; set; }
    protected Stats Stats { get; set; }

    protected void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }

    protected abstract void OnStateChanged( GameState state );

    public void SetStats( Stats stats )
    {
        Stats = stats;
    }

    protected void TakeDamage( int damage )
    {
        var newStats = Stats;
        newStats.Health = Mathf.Clamp( Stats.Health - damage, 0, Stats.Health );

        SetStats( newStats );

        if ( newStats.Health is 0 ) Die();
    }

    protected abstract void Die();
}