using Astogames.Helpers;
using Astogames.Scriptables;
using Astogames.Systems;
using UnityEngine;
using UnityEngine.Pool;

namespace Astogames.Managers
{
    public class UnitManager : StaticInstance<UnitManager>
    {
        [SerializeField]
        private Vector3 playerSpawnPosition;

        [SerializeField]
        private Vector3[] unitSpawnPositions;

        [SerializeField]
        private int unitsToSpawn = 10;

        [SerializeField]
        private EnemyType unitTypeToSpawn = EnemyType.Regular;

        private ObjectPool<UnitBase> _unitsPool;
        private int                  _unitsSpawned;

        private void Start()
        {
            _unitsPool = new ObjectPool<UnitBase>(
                InstantiateUnit,
                ActivateUnit,
                DeactivateUnit,
                unit => Destroy( unit.gameObject ),
                false,
                unitsToSpawn,
                unitsToSpawn * 2
            );

            _unitsSpawned = 0;
        }

        private void ActivateUnit( UnitBase unit )
        {
            unit.transform.position = GetRandomSpawnPosition();
            unit.gameObject.SetActive( true );
        }

        private void DeactivateUnit( UnitBase unit )
        {
            unit.transform.position = GetRandomSpawnPosition();
            unit.gameObject.SetActive( true );
        }

        private UnitBase InstantiateUnit()
        {
            var enemyScriptable = ResourceSystem.Instance.GetEnemy( unitTypeToSpawn );
            var unit = Instantiate( enemyScriptable.Prefab, GetRandomSpawnPosition(), Quaternion.identity, transform );
            unit.SetStats( enemyScriptable.BaseStats );
            unit.EnemyType = enemyScriptable.EnemyType;

            return unit;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return unitSpawnPositions[Random.Range( 0, unitSpawnPositions.Length )];
        }

        public Player SpawnPlayer()
        {
            var player = Instantiate( ResourceSystem.Instance.Player.Prefab, playerSpawnPosition, Quaternion.identity,
                transform );
            player.SetStats( ResourceSystem.Instance.Player.BaseStats );
            return (Player) player;
        }

        public void SpawnUnit( EnemyType regular )
        {
            if ( _unitsSpawned == unitsToSpawn ) return;
            if ( unitTypeToSpawn != regular ) {
                if ( _unitsSpawned != 0 ) return;
                _unitsPool.Clear();
            }

            unitTypeToSpawn = regular;
            _unitsPool.Get();
            _unitsSpawned++;
        }

        public void RemoveFromPool( UnitBase unit )
        {
            _unitsPool.Release( unit );
            _unitsSpawned--;
        }
    }
}