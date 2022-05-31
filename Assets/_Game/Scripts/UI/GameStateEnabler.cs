using Astogames.Managers;
using DG.Tweening;
using UnityEngine;

namespace Astogames.UI
{
    [RequireComponent( typeof(CanvasGroup) )]
    public class GameStateEnabler : MonoBehaviour
    {
        [SerializeField]
        private GameState gameState;
        [SerializeField]
        private float fadeDuration = 1;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup                     =  GetComponent<CanvasGroup>();
            GameManager.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnBeforeStateChanged -= OnStateChanged;
        }

        private void OnStateChanged( GameState state )
        {
            if ( state == gameState ) Show();
            else Hide();
        }

        private void Show()
        {
            _canvasGroup.DOFade( 100, fadeDuration );
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable   = true;
        }

        private void Hide()
        {
            _canvasGroup.DOFade( 0, fadeDuration );
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable   = false;
        }
    }
}