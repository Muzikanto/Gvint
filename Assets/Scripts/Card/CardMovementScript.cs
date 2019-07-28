using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public bool isDraggable;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        // TempCard
        TempCardGO = GameObject.Find("TempCard");
        // --------
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTempCardParent = transform.parent;
        isDraggable = MainScript.game.isPlayerTurn == GetComponent<CardController>().isPlayerOneCard;
        // isDraggable = DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SELF_HAND && MainScript.game.isPlayerTurn;

        if (!isDraggable)
        {
            return;
        }

        // TempCard
        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());
        // --------

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());

        // TempCard
        TempCardGO.transform.SetParent(GameObject.Find("Game").transform);
        TempCardGO.transform.localPosition = new Vector3(2340, 0);
        // --------
    }
}
