using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BetterCardRotation : MonoBehaviour
{
    public RectTransform CardFront;

    public RectTransform CardBack;

    public Transform targetFacePoint;

    public Collider col;

    private bool showingBack = false;

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
                                  direction: (-Camera.main.transform.position + targetFacePoint.position).normalized,
                                  maxDistance: (-Camera.main.transform.position + targetFacePoint.position).magnitude);
        bool passedThrough = false;
        foreach (RaycastHit h in hits)
        {
            if (h.collider == col)
            {
                passedThrough = true;
            }
        }

        if (passedThrough != showingBack)
        {
            showingBack = passedThrough;
            if(showingBack)
            {
                CardFront.gameObject.SetActive(false);
                CardBack.gameObject.SetActive(true);
            }
            else
            {
                CardBack.gameObject.SetActive(false);
                CardFront.gameObject.SetActive(true);
            }
        }
    }
}
