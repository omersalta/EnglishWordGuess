using UnityEngine;

public class RotateRight : MonoBehaviour {
    private void FixedUpdate() {
        transform.Rotate(Vector3.up * 0.70f, Space.World);
    }
}
