using UnityEngine;

public class Grid : MonoBehaviour
{
    int xCount, yCount;
    bool xPosValid = false, yPosValid = false;
    int xSize = 52, ySize = 49;
    int offset;

    int xCoordinate;
    int yCoordinate;

    private Vector2 position;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        xCount = Mathf.RoundToInt(rectTransform.sizeDelta.x / xSize);
        yCount = Mathf.RoundToInt(rectTransform.sizeDelta.y / ySize);
    }

    public bool IsValidMove()
    {
        xCoordinate = Mathf.RoundToInt(rectTransform.anchoredPosition.x + 156);
        yCoordinate = Mathf.RoundToInt(rectTransform.anchoredPosition.y + 220.5f);

        xPosValid = false;
        yPosValid = false;

        if (xCount % 2 == 0)
        {
            offset = (xSize / 2) / 2;
            position.x = isPosValid(xCoordinate, xSize / 2, ref xPosValid);
            position.x -= 156;
        }
        else
        {
            offset = xSize / 2;
            position.x = isPosValid(xCoordinate, xSize, ref xPosValid);
            position.x -= 156;
        }

        if (yCount % 2 == 0)
        {
            offset = ((ySize) / 2) / 2;
            position.y = isPosValid(yCoordinate, ySize / 2, ref yPosValid);
            position.y -= 220f;

        }
        else
        {
            offset = (ySize) / 2;
            position.y = isPosValid(yCoordinate, ySize, ref yPosValid);
            position.y -= 220.5f;
        }

        if (xPosValid && yPosValid)
        {
            rectTransform.anchoredPosition = position;
            return true;
        }
        else return false;
    }

    private int isPosValid(int coordinate, int size, ref bool check)
    {
        for (int i = (coordinate - offset); i <= (coordinate + offset); i++)
        {
            if (i % size == 0)
            {
                check = true;
                return i;
            }
        }
        return 0;
    }
}
