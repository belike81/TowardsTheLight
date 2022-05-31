using System.Collections;
using UnityEngine;

public class FXLoop : MonoBehaviour
{

    [SerializeField]
    private float timeToWait;

    [SerializeField]
    private GameObject particleObject;
    
    private void Start()
    {
        StartCoroutine( DisableAndEnable() );
    }

    private IEnumerator DisableAndEnable()
    {
        while ( true ) {
            particleObject.SetActive( !particleObject.activeInHierarchy );
            yield return new WaitForSeconds( timeToWait );
        }
    }
}
