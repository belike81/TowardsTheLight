using System;
using Astogames.Managers;
using UnityEngine;

namespace Astogames.UI
{
    [RequireComponent( typeof(AudioSource) )]
    public class GameStateMusic : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField]
        private GameStateMusicAssign[] gameStateMusicAssigns;

        private void Awake()
        {
            _audioSource                     =  GetComponent<AudioSource>();
            GameManager.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnBeforeStateChanged -= OnStateChanged;
        }

        private void OnStateChanged( GameState state )
        {
            foreach ( var gameStateMusicAssign in gameStateMusicAssigns )
                if ( state == gameStateMusicAssign.GameState ) {
                    _audioSource.clip = gameStateMusicAssign.Clip;
                    return;
                }
        }
    }

    [Serializable]
    public struct GameStateMusicAssign
    {
        public GameState GameState;
        public AudioClip Clip;
    }
}