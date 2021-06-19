using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offset = 2;
    public bool right;
    public bool left;
    public bool reverseScale;
    private float width;
    private Camera cam;
    private Transform trans;

    private void Awake()
    {
        cam = Camera.main;
        trans = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        width = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!left || !right) {
            float camExtent = cam.orthographicSize * Screen.width / Screen.height;
            float edgeVisisblePositionRight = trans.position.x + width / 2 - camExtent;
            float edgeVisisblePositionLeft = trans.position.x - width / 2 + camExtent;

            if (cam.transform.position.x >= edgeVisisblePositionRight - offset && !right) {
                
                InstantiateBuddy(1);
                right = true;
            }
            else if(cam.transform.position.x <= edgeVisisblePositionLeft + offset && !left){
                
                InstantiateBuddy(-1);
                left = true;
            }
        }
    }

    void InstantiateBuddy(int dir) {
        Vector3 pos = new Vector3((trans.position.x + width) * dir, trans.position.y, trans.position.z);
        Transform buddy = Instantiate(trans,pos,trans.rotation);
        if (reverseScale) {
            buddy.localScale = new Vector3(buddy.localScale.x * -1, buddy.localScale.y, buddy.localScale.z);
        }
        buddy.parent = trans.parent;
        if (dir == 1)
        {
            buddy.GetComponent<Tiling>().right = true;
        }
        else if (dir == -1) {
            buddy.GetComponent<Tiling>().left = true;
        }
    }
}
