using System;
using UnityEngine;

namespace Astogames.Scriptables
{
    [CreateAssetMenu( fileName = "New Scriptable Enemy" )]
    public class ScriptableEnemy : ScriptableUnitBase
    {
        public EnemyType EnemyType;
    }

    [Serializable]
    public enum EnemyType
    {
        Regular  = 0,
        Lust     = 1,
        Gluttony = 2
    }
}