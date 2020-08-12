using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectMouseInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
                        IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{   
    Material new_material;
    void Start() {
        new_material = GetComponent<Renderer>().material;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogFormat("{0} clicked!", gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogFormat("{0} pointer down!", gameObject.name);
         new_material.color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogFormat("{0} pointer enter!", gameObject.name);
        new_material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.LogFormat("{0} pointer exit!", gameObject.name); 
        new_material.color = Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogFormat("{0} pointer up!", gameObject.name);
        new_material.color = Color.yellow;
    }
}
