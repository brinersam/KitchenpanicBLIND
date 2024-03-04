using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMeSprite : MonoBehaviour
{
    Camera cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cameraMain.transform.rotation;
    }
}
