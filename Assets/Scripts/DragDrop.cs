using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] CanvasScaler canvasScaler;
    [SerializeField] private Canvas[] slotCanvas;

    private RectTransform rectTransform;
    private new Rigidbody2D rigidbody;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rigidbody = GetComponent<Rigidbody2D>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //AudioManager.instance.SelectionSound();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.instance.SelectionSound();
        canvasGroup.blocksRaycasts = false;
        canvas.sortingOrder = 3;
        updateSlotCanavas(2);
        eventData.pointerDrag.GetComponent<CompositeCollider2D>().isTrigger = true;
        eventData.pointerDrag.GetComponent<Tetromino>().ActivatePhysicsSimulation();
        rigidbody.simulated = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasScaler.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvas.sortingOrder = 1;
        updateSlotCanavas(0);
        eventData.pointerDrag.GetComponent<CompositeCollider2D>().isTrigger = false;
        rigidbody.simulated = true;
    }

    private void updateSlotCanavas(int num)
    {
        foreach (var item in slotCanvas)
        {
            item.sortingOrder = num;
        }
    }
}
