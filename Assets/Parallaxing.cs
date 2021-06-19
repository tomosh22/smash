using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] elements;
    private float[] scales;
    public float smoothing  = 1f;
    private Transform cam;
    private Vector3 prevCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevCamPos = cam.position;
        scales = new float[elements.Length];
        for (int i = 0; i < elements.Length; i++) {
            scales[i] = elements[i].position.z * -1;        
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < elements.Length; i++) {
            float parallax = (prevCamPos.x - cam.position.x) * scales[i];
            float backgroundTargetX = elements[i].position.x + parallax;
            Vector3 backGroundTarget = new Vector3(backgroundTargetX, elements[i].position.y, elements[i].position.z);
            elements[i].position = Vector3.Lerp(elements[i].position, backGroundTarget, smoothing * Time.deltaTime);
            
        }
        prevCamPos = cam.position;
    }
}
