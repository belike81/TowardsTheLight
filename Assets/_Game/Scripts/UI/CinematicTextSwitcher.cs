using System.Collections;
using Astogames.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Astogames.UI
{
    public class CinematicTextSwitcher : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textField;

        [SerializeField]
        private string[] stringsToShow;

        [SerializeField]
        private float timeToShow;
        private CanvasGroup _canvasGroup;

        private int _currentString;

        private void Awake()
        {
            GameManager.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnBeforeStateChanged -= OnStateChanged;
        }

        private void OnStateChanged( GameState state )
        {
            if ( state is not GameState.IntroCinematic ) return;

            _currentString = 0;
            _canvasGroup   = textField.GetComponent<CanvasGroup>();
            StartCoroutine( CycleText() );
        }

        private IEnumerator CycleText()
        {
            _canvasGroup.alpha = 0;
            while ( _currentString < stringsToShow.Length ) {
                var sequence = DOTween.Sequence();
                textField.SetText( stringsToShow[_currentString] );
                sequence.Append( _canvasGroup.DOFade( 100, 0.4f ) );
                sequence.AppendInterval( timeToShow );
                sequence.Append( _canvasGroup.DOFade( 0, 0.4f ) );
                _currentString++;
                yield return sequence.WaitForCompletion();
            }
        }
    }
}