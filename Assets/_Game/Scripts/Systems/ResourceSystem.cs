using System.Collections.Generic;
using System.Linq;
using Astogames.Helpers;
using Astogames.Scriptables;
using UnityEngine;

namespace Astogames.Systems
{
    public class ResourceSystem : StaticInstance<ResourceSystem>
    {
        private Dictionary<EnemyType, ScriptableEnemy> _enemiesDisctionary;
        public List<ScriptableEnemy> Enemies { get; private set; }
        public ScriptablePlayer Player { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            Player              = Resources.Load<ScriptablePlayer>( "Player/PlayerSO" );
            Enemies             = Resources.LoadAll<ScriptableEnemy>( "Enemies" ).ToList();
            _enemiesDisctionary = Enemies.ToDictionary( r => r.EnemyType, r => r );
        }

        public ScriptableEnemy GetEnemy( EnemyType t )
        {
            return _enemiesDisctionary[t];
        }

        public ScriptableEnemy GetRandomEnemy()
        {
            return Enemies[Random.Range( 0, Enemies.Count )];
        }
    }
}