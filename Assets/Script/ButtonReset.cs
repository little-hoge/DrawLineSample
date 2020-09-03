using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour
{
    public GameObject m_LineObject;
    public Transform RetryButton;

    void Update() {

        if (Input.GetKey(KeyCode.R)) {
            RetryButton.position = new Vector3(0f, 4.5f, 0f);
            m_LineObject.GetComponent<LineManager>().DeleteLineObject();
        }

    }
   
}
