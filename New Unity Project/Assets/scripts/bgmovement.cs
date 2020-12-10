using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class is responsible for the gradient background
public class bgmovement : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //refrences to the 4 gradients
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    [SerializeField] GameObject bg3;
    [SerializeField] GameObject bg4;

    // Update is called once per frame
    //this function just rotates the gradients
    void Update()
    {
        bg1.transform.Rotate(Vector3.forward * 0.5f);
        bg2.transform.Rotate(Vector3.forward * -0.5f);
        bg3.transform.Rotate(Vector3.forward * 1);
        bg4.transform.Rotate(Vector3.forward * -1);
    }
}
