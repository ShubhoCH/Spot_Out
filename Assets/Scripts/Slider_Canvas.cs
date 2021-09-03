using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slider_Canvas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Manager manager;
    bool move_slider, wait;
    private CanvasGroup canvasGroup;
    private RectTransform reactTransform;
    [SerializeField] private Canvas canvas;
    float snappingLimit, upperLimit, lowerLimit;
    Vector3 previosPosition;
    private void OnEnable()
    {
        move_slider = false;
        snappingLimit = manager.SendSnappingValue();
        upperLimit = manager.SendUpperLimit();
        lowerLimit = manager.SendLowerLimit();
        transform.localPosition = new Vector2(((int)(transform.localPosition.x / snappingLimit)) * snappingLimit, 0);
        reactTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if (move_slider == true && transform.localPosition.x > -lowerLimit && transform.localPosition.x < upperLimit)
        {
            if (transform.parent.name == "VS_Pivot")
                transform.position = new Vector2(transform.position.x, Input.mousePosition.y);
            else
                transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
        }
        else if(move_slider == true && (transform.localPosition.x < -lowerLimit || transform.localPosition.x > upperLimit))
            wait = true;
        else if(move_slider == false && manager.CheckIfSliderIsPositionedCorrectlyOrNot(transform) != 0)
            manager.CorrectPos(transform);
        if(wait == true && move_slider == true)
        {
            if (transform.parent.name == "VS_Pivot")
            {
                if (transform.localPosition.x < -lowerLimit && Input.mousePosition.y < transform.position.y)
                    transform.position = new Vector2(transform.position.x, Input.mousePosition.y);
                else if(transform.localPosition.x > upperLimit && Input.mousePosition.y > transform.position.y)
                    transform.position = new Vector2(transform.position.x, Input.mousePosition.y);
            }
            else
            {
                if (transform.localPosition.x < -lowerLimit && Input.mousePosition.x > transform.position.x)
                    transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
                else if (transform.localPosition.x > upperLimit && Input.mousePosition.x < transform.position.x)
                    transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        move_slider = true;
        transform.GetChild(0).localScale = new Vector2(1f, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        move_slider = false;
        wait = false;
        transform.GetChild(0).localScale = new Vector2(0.82f, 0.82f);
        if(transform.localPosition.x >= 0)
        {
            if (transform.localPosition.x > ((int)((transform.localPosition.x) / snappingLimit) * snappingLimit + snappingLimit / 2))
                transform.localPosition = new Vector2(((int)((transform.localPosition.x) / snappingLimit) + 1) * snappingLimit, 0);
            else
                transform.localPosition = new Vector2(((int)((transform.localPosition.x) / snappingLimit)) * snappingLimit, 0);
        }
        else
        {
            if (transform.localPosition.x < ((int)((transform.localPosition.x) / snappingLimit) * snappingLimit - snappingLimit / 2))
                transform.localPosition = new Vector2(((int)((transform.localPosition.x) / snappingLimit) - 1) * snappingLimit, 0);
            else
                transform.localPosition = new Vector2(((int)((transform.localPosition.x) / snappingLimit)) * snappingLimit, 0);
        }
        if (PlayerPrefs.GetString("SFX", "true") == "true")
            FindObjectOfType<AudioManager>().Play("Snap");
        manager.Check();
    }
    //IBeginDragHandler, IEndDragHandler,
    //public void OnDrag(PointerEventData eventData)
    //{
    //    reactTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    canvasGroup.alpha = 1f;
    //    canvasGroup.blocksRaycasts = true;
    //}
}
