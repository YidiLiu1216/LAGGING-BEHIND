using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [Header("Settings")]
    public float longPressTime = 0.25f;      // hold duration to begin carrying
    public LayerMask npcLayer;               // set to the NPC layer
    public LayerMask facilityLayer;
    public float carryHeightOffset = 0f;     // add if need slight Y offset while dragging

    [SerializeField] CrosshairController crossHair;
    public KeyCode aimKey = KeyCode.Mouse1;
    bool isAiming=false;
    int killingPeopleSanChange = 3;

    Camera cam;
    float pressTimer;
    bool isPressing;
    bool longPressTriggered;

    GameObject target;       // NPC pressed on
    People carrying;     // NPC currently being carried
    Vector2 carryOffset;
    private void Awake()
    {
        cam = Camera.main;
        if (!cam) cam = FindFirstObjectByType<Camera>();
    }
    void Update()
    {
        if (Time.timeScale == 0) { return; }
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        bool isAiming = Input.GetKey(aimKey) && carrying == null;
        //var hit = Physics2D.OverlapPoint(mouseWorld, facilityLayer);
        crossHair.gameObject.SetActive(isAiming);
        if (isAiming)
        {
            
            crossHair.UpdatePosition(mouseWorld);
            if (Input.GetMouseButtonDown(0))
            {
                target = HitNPC(mouseWorld);
                FireTheGun(target);
                target = null;
            }
        }
        if (carrying)
        {
            carrying.MoveTo(mouseWorld + carryOffset + new Vector2(0f, carryHeightOffset));

            // Tap again to drop
            if (Input.GetMouseButtonDown(0))
            {
                //move to one of the facility
                var hit = Physics2D.OverlapPoint(mouseWorld, facilityLayer);
                if (hit != null) {
  
                    hit.GetComponent<Facilities>().putNPCinFacilities(carrying.tag);
                    PoolManager.Instance.Despawn(carrying.tag, carrying.gameObject);
                }
                carrying.EndCarry();
                carrying = null;
                // consume this click so it doesn't toggle pause immediately
                isPressing = false;
                longPressTriggered = false;
                target = null;

            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (!isAiming)
            {
                isPressing = true;
                longPressTriggered = false;
                pressTimer = 0f;
                
            }
            target = HitNPC(mouseWorld);
            //Debug.Log(target?target.tag:"null");
        }
        if (isPressing)
        {
            pressTimer += Time.deltaTime;

            // Long-press ¡ú start carrying the target (if any)
            if (!longPressTriggered && pressTimer >= longPressTime && target)
            {
                
                if (target.tag == "Normal") {
                    carrying = target.GetComponent<People>();
                   
                }
                else
                {
                    carrying = target.GetComponentInParent<People>();
                }
                AudioController.instance.playGrabSFX();
                carrying.BeginCarry();
                // store offset so the NPC doesn't jump to cursor center
                //carryOffset = (Vector2)carrying.transform.position - mouseWorld;
                carryOffset = new Vector2(0.2f, -0.3f);
                longPressTriggered = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!longPressTriggered&&!isAiming)
            {
                if (target)
                {
                    if (target.tag == "Lazy")
                    {
                        target.GetComponent<LazyPeople>().Resume();
                    }
                    if (target.tag == "Reactionary") {
                        target.GetComponent<Reactionary>().Resume();
                    }
                }
                pressTimer += Time.deltaTime;
            }
            isPressing = false;
            target = null;
        }
       
        
    }
        GameObject HitNPC(Vector2 worldPoint)
        {
            // Use OverlapPoint for precise top-down click; 
            var hit = Physics2D.OverlapPoint(worldPoint, npcLayer);
            if (!hit) return null;
            return hit.gameObject;
        }
    public void UpdateCrosshairPosition(Vector2 mouseWorld)
    {
        crossHair.transform.position = mouseWorld;
    }
    void FireTheGun(GameObject target) {
        AudioController.instance.playShootSFX();
        if (!target) return;
        if (target.tag == "Body") return;
        //Debug.Log("Fire!!");
        AudioController.instance.playCharacterAMaleScream();
        Vector3 targetpos = target.transform.position;
        PoolManager.Instance.Despawn(target.tag,target);
        PoolManager.Instance.Spawn("Body", targetpos,Quaternion.identity);
        PlayerData.instance.changeSan(-killingPeopleSanChange);
        PlayerData.instance.updateKillCount(target.tag);
    }

}
