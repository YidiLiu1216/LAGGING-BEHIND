using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void UpdatePosition(Vector2 mouseWorld) {
        gameObject.transform.position = mouseWorld;
    }
    public void Fire() {

    }
}
