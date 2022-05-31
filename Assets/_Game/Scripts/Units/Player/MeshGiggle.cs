using DG.Tweening;
using UnityEngine;

public class MeshGiggle : MonoBehaviour
{
    private void Start()
    {
        transform.DOShakePosition( 4f, new Vector3( 0.04f, 0.04f, 0.04f ), 1, 10 ).SetLoops( -1, LoopType.Yoyo );
        transform.DOLocalMoveY( transform.localPosition.y + 0.3f, 3 ).SetLoops( -1, LoopType.Yoyo )
            .SetEase( Ease.InOutQuad );
    }
}