using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ESCOpenScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public GameObject ESCContainer;
    [SerializeField]
    public GameObject GameContainer;

    public void OnPointerClick(PointerEventData eventData)
    {
        ESCContainer.SetActive(true);
        GameContainer.SetActive(false);
    }
}
