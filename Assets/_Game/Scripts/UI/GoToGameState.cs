using Astogames.Managers;
using UnityEngine;

namespace Astogames.UI
{
    public class GoToGameState : MonoBehaviour
    {
        [SerializeField]
        private GameState gameState;

        public void SwitchState()
        {
            GameManager.Instance.ChangeState( gameState );
        }
    }
}