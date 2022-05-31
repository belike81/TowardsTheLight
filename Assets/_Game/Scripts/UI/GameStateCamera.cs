using Astogames.Managers;
using Cinemachine;
using UnityEngine;

namespace Astogames.UI
{
    [RequireComponent( typeof(CanvasGroup) )]
    public class GameStateCamera : MonoBehaviour
    {
        [SerializeField]
        private GameState[] gameStates;

        private CinemachineVirtualCamera _camera;

        private void Awake()
        {
            _camera                          =  GetComponent<CinemachineVirtualCamera>();
            GameManager.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnBeforeStateChanged -= OnStateChanged;
        }

        private void OnStateChanged( GameState state )
        {
            foreach ( var gameState in gameStates )
                if ( state == gameState ) {
                    Show();
                    return;
                }

            Hide();
        }

        private void Show()
        {
            _camera.Priority = 100;
        }

        private void Hide()
        {
            _camera.Priority = 0;
        }
    }
}