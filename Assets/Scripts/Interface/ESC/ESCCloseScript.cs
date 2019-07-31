using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ESCCloseScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public GameObject ESCcontainer;

    public void OnPointerClick(PointerEventData eventData)
    {
        ESCcontainer.SetActive(false);
    }
}
