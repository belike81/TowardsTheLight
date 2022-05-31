using System;
using System.Collections;
using Astogames.Helpers;
using Astogames.Scriptables;
using Astogames.Systems;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Astogames.Managers
{
    public class GameManager : StaticInstance<GameManager>
    {
        [Header( "UI" )]
        [SerializeField]
        private Slider progressBar;

        [Header( "Camera setup" )]
        [SerializeField]
        private CinemachineVirtualCamera[] playerCameras;


        private float _playerProgress;
        private float _progressGoal;

        private bool _spawnEnemies;
        public GameState State { get; private set; }

        private void Start()
        {
            ChangeState( GameState.StartScreen );
            _progressGoal = 360;
        }

        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;

        public void ChangeState( GameState newState )
        {
            OnBeforeStateChanged?.Invoke( newState );

            State = newState;
            switch ( newState ) {
                case GameState.StartScreen:
                    HandleStarting();
                    break;
                case GameState.IntroCinematic:
                    HandleCinematic();
                    break;
                case GameState.Gameplay:
                    HandleGamePlay();
                    break;
                case GameState.Credits:
                    break;
                case GameState.Win:
                    HandleWin();
                    break;
                case GameState.Lose:
                    HandleLoose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof(newState), newState, null );
            }

            OnAfterStateChanged?.Invoke( newState );
        }

        private void HandleLoose()
        {
            AudioSystem.Instance.PlayDefaultMusic();
        }

        private void HandleStarting()
        {
            _playerProgress = 0;
            _spawnEnemies   = false;
            StopAllCoroutines();
            AudioSystem.Instance.PlayDefaultMusic();
        }

        private void HandleCinematic()
        {
            var playerTransform = UnitManager.Instance.SpawnPlayer()?.transform;
            foreach ( var virtualCamera in playerCameras ) {
                virtualCamera.Follow = playerTransform;
                virtualCamera.LookAt = playerTransform;
            }

            _spawnEnemies = true;
            StartCoroutine( SpawnEnemy() );
            AudioSystem.Instance.PlayDefaultMusic();
        }

        private void HandleGamePlay()
        {
            StartCoroutine( ProgressPlayer() );
        }

        private void HandleWin()
        {
            Debug.Log( "You win!" );
        }

        private void HandleCredits()
        {
            Debug.Log( "Credits!" );
        }

        private IEnumerator SpawnEnemy()
        {
            while ( _spawnEnemies ) {
                yield return new WaitForSeconds( 2 );
                UnitManager.Instance.SpawnUnit( EnemyType.Regular );
            }
        }

        private IEnumerator ProgressPlayer()
        {
            while ( _playerProgress < _progressGoal ) {
                progressBar.value = _playerProgress / _progressGoal;
                yield return new WaitForSeconds( 1 );
                _playerProgress++;
            }

            if ( _playerProgress >= _progressGoal ) ChangeState( GameState.Win );
        }
    }

    [Serializable]
    public enum GameState
    {
        StartScreen    = 0,
        IntroCinematic = 1,
        Gameplay       = 2,
        Credits        = 3,
        Win            = 4,
        Lose           = 5
    }
}