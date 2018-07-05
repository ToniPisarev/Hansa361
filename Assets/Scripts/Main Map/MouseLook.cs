using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MouseLook : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothVector;

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public bool inGame;

    GameObject character;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.parent.gameObject;
        inGame = true;
        gameObject.GetComponent<BlurOptimized>().enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (inGame) {
            if (Cursor.lockState != CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.Locked;
            }           

            Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothVector.x = Mathf.Lerp(smoothVector.x, mouseDirection.x, 1f / smoothing);
            smoothVector.y = Mathf.Lerp(smoothVector.y, mouseDirection.y, 1f / smoothing);
            mouseLook += smoothVector;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        } else {
            Cursor.lockState = CursorLockMode.None;
            character.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
