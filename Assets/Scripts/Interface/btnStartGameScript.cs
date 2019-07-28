using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class btnStartGameScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AppManager.startGame();
    }
}
