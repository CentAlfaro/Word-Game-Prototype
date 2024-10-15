using System;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mouse_Pointer
{
    public class Highlight : MonoBehaviour
    {
        [Header("Materials References")]
        [SerializeField] private List<Material> materials = new List<Material>();
        [SerializeField] private Material currentMaterial;

        [Header("Highlighter Instantiation References")]
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
            lineRenderer.material = SelectedMaterial();
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

        private Material SelectedMaterial()
        {
            if (materials.Count <= 1)
            {
                return materials[0];
            }

            if (!currentMaterial)
            {
                ReorganizeMaterials();
            }
            
            return currentMaterial;
        }

        private void ReorganizeMaterials()
        {
            currentMaterial = materials[0];
            materials.Remove(currentMaterial);
            materials.Add(currentMaterial);
        }

        private void OnEnable()
        {
            WordSystemEvents.ON_CREATE_HIGHLIGHT += SetStartMousePos;
            WordSystemEvents.ON_REPOSITION_HIGHLIGHT += SetCurrentMousePos;
            WordSystemEvents.ON_RELEASE_HIGHLIGHT += SetEndMousePos;
            WordSystemEvents.ON_DELETE_HIGHLIGHT += DeleteCurrentHighlight;
            WordSystemEvents.ON_REORGANIZE_HIGHLIGHT_MATERIALS += ReorganizeMaterials;
        }

        private void OnDisable()
        {
            WordSystemEvents.ON_CREATE_HIGHLIGHT -= SetStartMousePos;
            WordSystemEvents.ON_REPOSITION_HIGHLIGHT -= SetCurrentMousePos;
            WordSystemEvents.ON_RELEASE_HIGHLIGHT -= SetEndMousePos;
            WordSystemEvents.ON_DELETE_HIGHLIGHT -= DeleteCurrentHighlight;
            WordSystemEvents.ON_REORGANIZE_HIGHLIGHT_MATERIALS += ReorganizeMaterials;
        }
    }
}