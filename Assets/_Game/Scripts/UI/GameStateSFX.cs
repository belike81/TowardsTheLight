using Astogames.Managers;
using UnityEngine;

namespace Astogames.UI
{
    [RequireComponent( typeof(AudioSource) )]
    public class GameStateSFX : MonoBehaviour
    {
        [SerializeField]
        private GameState _gameState;
        private AudioSource _audioSource;

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
            if ( state == _gameState ) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}