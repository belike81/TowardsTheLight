using Astogames.Managers;
using UnityEngine;

namespace Astogames.Interactables
{
    [RequireComponent( typeof(Collider) )]
    public class GameStateTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameState gameState;

        public void OnTriggerEnter()
        {
            GameManager.Instance.ChangeState( gameState );
        }
    }
}