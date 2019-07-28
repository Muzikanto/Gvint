using UnityEngine;
using UnityEngine.EventSystems;

public class btnRestartGameScript : MonoBehaviour, IPointerClickHandler
{
    public int countCells;

    public void OnPointerClick(PointerEventData eventData)
    {
        AppManager.restartGame(this);
    }
}
