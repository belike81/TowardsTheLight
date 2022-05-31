using Astogames.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Astogames.UI
{
    [RequireComponent( typeof(Button) )]
    public class ButtonSound : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private AudioClip _clip;

        public void OnPointerEnter( PointerEventData eventData )
        {
            AudioSystem.Instance.PlaySound( _clip );
        }
    }
}