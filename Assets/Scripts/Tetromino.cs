using UnityEngine;
using UnityEngine.UI;

public class Tetromino : MonoBehaviour
{
    public enum ID
    {
        Atlas_01 = 1,
        Atlas_02,
        Atlas_03,
        Atlas_04,
        Atlas_05,
        Atlas_06,
        Atlas_07,
        Atlas_08,
        Atlas_09,
        Atlas_10,
        Atlas_11,
        Atlas_12,
        Atlas_13,
        Atlas_14,
        Atlas_15,
        Atlas_16,
        Atlas_17,
        Atlas_18
    };

    [SerializeField] private ID iD;
    [SerializeField] private OnDropEvent onDropEvent;
    [SerializeField] private float slidUpSpeed;

    private RectTransform rectTransform;
    private new Rigidbody2D rigidbody;
    private CompositeCollider2D compositeCollider2D;
    private Grid grid;

    private bool isPhysicsSimulationActivated = true;
    private bool isAlreadyInVaildPos = false;
    private bool isPhyshicSimulationUpdated = false;
    private bool smothTransition = false;

    public bool isFinalPos = false;

    private Vector2 smoothTarget;
    public bool isGlowUp = false;
    public bool isSlidUp = false;
    public Vector2 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rigidbody = GetComponent<Rigidbody2D>();
        compositeCollider2D = GetComponent<CompositeCollider2D>();
        grid = GetComponent<Grid>();

        initialPosition = rectTransform.anchoredPosition;
    }

    private void FixedUpdate()
    {
        if (rigidbody != null)
        {
            if (rigidbody.velocity == Vector2.zero && isPhysicsSimulationActivated && isPhyshicSimulationUpdated)
            {
                isPhysicsSimulationActivated = false;
                Invoke(nameof(newMethod1), 1f);
                isPhyshicSimulationUpdated = false;
            }
        };


        if (smothTransition)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, smoothTarget, 10f);
            if (rectTransform.anchoredPosition == smoothTarget) smothTransition = false;
        }

        if (isGlowUp)
        {
            GetComponent<Image>().color = Color.cyan;
            Invoke(nameof(SlidUp), 2f);
        }

        if (isSlidUp) rectTransform.anchoredPosition += new Vector2(0, slidUpSpeed * Time.deltaTime);
    }


    public ID GetID()
    {
        return iD;
    }

    public void ActivatePhysicsSimulation()
    {
        rigidbody.gravityScale = 1;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isPhysicsSimulationActivated = true;
        isAlreadyInVaildPos = false;
    }

    public void DeactivatePhysicsSimulation()
    {
        rigidbody.gravityScale = 0;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        isPhyshicSimulationUpdated = true;
    }

    void newMethod1()
    {
        isAlreadyInVaildPos = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tetromino" || collision.transform.tag == "BottomBorder")
        {
            DeactivatePhysicsSimulation();
            grid.IsValidMove();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tetromino" && !isPhysicsSimulationActivated && !isAlreadyInVaildPos)
        {
            onDropEvent.SnapBackToSlot(rectTransform);
        }
    }

    public void SmothTransition(Vector2 targetPos)
    {
        smoothTarget = targetPos;
        smothTransition = true;
    }

    private void SlidUp()
    {
        isSlidUp = true;
    }
}
