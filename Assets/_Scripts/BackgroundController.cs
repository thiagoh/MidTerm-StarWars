using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector2 newPosition = transform.position;

        newPosition.y -= 1f * Time.deltaTime;

        if (newPosition.y <= -10f) {
            newPosition.y = 4f;
        }

        transform.position = newPosition;
    }
}
