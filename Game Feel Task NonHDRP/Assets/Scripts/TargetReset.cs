using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    private FeedbackSettings feedbackSettings;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        feedbackSettings = FindObjectOfType<FeedbackSettings>();
    }

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        StartCoroutine(ResetPositionIE());
    }

    private IEnumerator ResetPositionIE()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Target reseted");
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        yield return new WaitForSeconds(0.1f);
        if (feedbackSettings.physicsToggle.isOn == true)
            rb.isKinematic = false;
    }
}
