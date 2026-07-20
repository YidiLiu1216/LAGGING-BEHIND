using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    [Header("Moving Parameter")]
    public float maxspeed = 1f;
    public float minspeed = 0.7f;
    public float swayAmount = 1f;
    public float swaySpeed = 1f;
    SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private float randomOffset;
    private Rigidbody2D rb2d;
    private RigidbodyType2D originalRigidBodyType;

    //[Header("Picking Up Parameter")]
    public bool IsPaused { get; private set; }
    public bool IsCarried { get; private set; }

    protected Animator animator;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalRigidBodyType = rb2d.bodyType;
        animator = GetComponent<Animator>();
        IsCarried = false;
    }
    void Start()
    {
        startPosition = transform.position;
       
        randomOffset = Random.Range(0f, 2f * Mathf.PI); // Different sway for each NPC
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = GetRandomColor();
        }
    }

    public void Update()
    {
        if (!IsCarried)
        {
            movePeople(minspeed, maxspeed);
        }
    }
    void LateUpdate()
    {
        if (!IsCarried)
        {
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
    Color GetRandomColor()
    {
        return new Color(
            Random.Range(0.4f, 1f), // Red
            Random.Range(0.4f, 1f), // Green
            Random.Range(0.4f, 1f)  // Blue
        );
    }
    protected void movePeople(float min_s,float max_s) {
        float swayAmountRandom = Random.Range(0.0f, swayAmount);
        float xOffset = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount;
        Vector3 moveDir = new Vector3(xOffset, 1, 0).normalized;
        transform.position += moveDir * Random.Range(min_s, max_s) * Time.deltaTime;
    }
    public void ResetPeople()
    {
        IsCarried = false;
        rb2d.bodyType = originalRigidBodyType;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().sortingLayerName = "Character";
    }
    public void BeginCarry() {
        if (IsCarried) return;
        IsCarried = true;

        rb2d.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().sortingLayerName = "Carrying";
        if (TryGetComponent<PeopleAvoidence>(out var avoid))
        {
            avoid.enabled = false;
        }
        animator.SetBool("isGrabbing",true);
        //Debug.Log("carry");
    }
    public void MoveTo(Vector2 pos) {
        //Debug.Log(pos);
        rb2d.MovePosition(pos);
        //Debug.Log("moving");
    }
    public void EndCarry() {
        if (!IsCarried) return;
        IsCarried = false;

        rb2d.bodyType = originalRigidBodyType;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().sortingLayerName = "Character";
        if (TryGetComponent<PeopleAvoidence>(out var avoid))
        {
            avoid.enabled = true;
        }
        animator.SetBool("isGrabbing", false);
        //Debug.Log("end");
    }

}
