using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private bool raycastEnabled;
    [SerializeField] private Camera arCamera;
    void Update()
    {
        #region Raycast
        if (CharactersController.CanChangeCharacter)
        {
            Vector3 rayOrigin = arCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hitObject;
            if (Physics.Raycast(rayOrigin, arCamera.transform.forward, out hitObject, 100))
            {
                OnDetected(hitObject.transform.gameObject);
            }

        }
        #endregion
    }

    void OnDetected(GameObject go)
    {
        if (go.GetComponent<Character>() != null)
            CharactersController.CurrentCharacter = go.GetComponent<Character>();
    }
}
