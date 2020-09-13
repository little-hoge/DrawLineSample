using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour
{
    public GameObject lineObject;
    public Transform circleTransform;

    void Update() {

        if (Input.GetKey(KeyCode.R)) {
            circleTransform.position = new Vector3(0f, 4.5f, 0f);
            lineObject.GetComponent<LineManager>().AllDeleteLine();
        }

    }
   
}
