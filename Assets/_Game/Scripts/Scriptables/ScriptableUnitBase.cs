using System;
using UnityEngine;

namespace Astogames.Scriptables
{
    public abstract class ScriptableUnitBase : ScriptableObject
    {
        [SerializeField]
        private Stats _stats;

        public UnitBase Prefab;

        public string Description;
        public Sprite MenuSprite;
        public Stats BaseStats => _stats;
    }

    [Serializable]
    public struct Stats
    {
        public float Speed;
        public int   Health;
        public int   Power;
    }
}