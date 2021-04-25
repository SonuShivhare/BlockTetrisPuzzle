using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropEvent : MonoBehaviour, IDropHandler
{
    [SerializeField] public RectTransform Slots;

    private new Rigidbody2D rigidbody;
    private RectTransform tetrominoRectTransform;
    private RectTransform slotRectTranform;

    public enum Partition
    {
        side,
        middle
    }

    [SerializeField] Partition partition;

    public void OnDrop(PointerEventData eventData)
    {
        tetrominoRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();

        switch (partition)
        {
            case Partition.side:
                SnapBackToSlot(tetrominoRectTransform);
                break;
            case Partition.middle:
                if (!tetrominoRectTransform.GetComponent<Grid>().IsValidMove()) SnapBackToSlot(tetrominoRectTransform);
                tetrominoRectTransform.GetComponent<Tetromino>().isFinalPos = true;
                break;
        }
    }

    public void SnapBackToSlot(RectTransform rect)
    {
        foreach (RectTransform slot in Slots)
        {
            if (rect.GetComponent<Tetromino>().GetID() == slot.GetComponent<Tetromino>().GetID())
            {
                if (rect.anchoredPosition != slot.anchoredPosition)
                {
                    tetrominoRectTransform = rect;
                    AudioManager.instance.SnapBackSound();
                    tetrominoRectTransform.GetComponent<Tetromino>().isFinalPos = false;
                    tetrominoRectTransform.anchoredPosition = slot.anchoredPosition;
                    slotRectTranform = slot;
                    rigidbody = tetrominoRectTransform.GetComponent<Rigidbody2D>();
                    Invoke(nameof(UpdateData), 0.1f);
                }
            }
        }
    }

    private void UpdateData()
    {
        rigidbody.simulated = false;
        tetrominoRectTransform.anchoredPosition = slotRectTranform.anchoredPosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (partition)
        {
            case Partition.side:
                if (collision.transform.tag == "Tetromino")
                {
                    SnapBackToSlot(collision.transform.GetComponent<RectTransform>());
                }
                break;
        }
    }
}


