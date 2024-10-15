using System;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mouse_Pointer
{
    public class Highlight : MonoBehaviour
    {
        [SerializeField] private GameObject highlightPrefab;
        [SerializeField] private GameObject parentContainer;
        
        private GameObject highlightObj;
        private LineRenderer lineRenderer;
        private Vector2 mousePos;
        private Vector2 startMousePos;
        
        private void SetStartMousePos(GameObject position)
        {
            highlightObj = Instantiate(highlightPrefab, Vector3.zero, Quaternion.identity, parentContainer.transform);
            lineRenderer = highlightObj.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            startMousePos = position.transform.position;
        }

        private void SetCurrentMousePos()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }

        private void SetEndMousePos(GameObject position)
        {
            lineRenderer.SetPosition(1, new Vector3(position.transform.position.x, position.transform.position.y, 0f));
        }

        private void DeleteCurrentHighlight()
        {
            Destroy(highlightObj);
            ResetValues();
        }

        private void ResetValues()
        {
            highlightObj = null;
            lineRenderer = null;
        }

        private void OnEnable()
        {
            WordSystemEvents.ON_CREATE_HIGHLIGHT += SetStartMousePos;
            WordSystemEvents.ON_REPOSITION_HIGHLIGHT += SetCurrentMousePos;
            WordSystemEvents.ON_RELEASE_HIGHLIGHT += SetEndMousePos;
            WordSystemEvents.ON_DELETE_HIGHLIGHT += DeleteCurrentHighlight;
        }

        private void OnDisable()
        {
            WordSystemEvents.ON_CREATE_HIGHLIGHT -= SetStartMousePos;
            WordSystemEvents.ON_REPOSITION_HIGHLIGHT -= SetCurrentMousePos;
            WordSystemEvents.ON_RELEASE_HIGHLIGHT -= SetEndMousePos;
            WordSystemEvents.ON_DELETE_HIGHLIGHT -= DeleteCurrentHighlight;
        }
    }
}