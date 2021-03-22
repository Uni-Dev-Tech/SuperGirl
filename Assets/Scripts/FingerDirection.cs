using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerDirection : MonoBehaviour
{
    public Camera currentCamera;
    public Image internalRound;
    public Image extrenalRound;
    public int limitZone;

    [HideInInspector]
    public bool useFingerDirection = true;

    static public FingerDirection instance;

    private void Start()
    {
        if (FingerDirection.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        FingerDirection.instance = this;
    }
    private void Update()
    {
        if (useFingerDirection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                extrenalRound.gameObject.SetActive(true);
                extrenalRound.gameObject.transform.position = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = -(extrenalRound.gameObject.transform.position - Input.mousePosition);

                if (direction.x > limitZone)
                    direction -= new Vector3(direction.x - limitZone, 0, 0);
                if (direction.x < -limitZone)
                    direction -= new Vector3(direction.x + limitZone, 0, 0);

                if (direction.y > limitZone)
                    direction -= new Vector3(0, direction.y - limitZone, 0);
                if (direction.y < -limitZone)
                    direction -= new Vector3(0, direction.y + limitZone, 0);

                internalRound.transform.localPosition = direction;
            }

            if (Input.GetMouseButtonUp(0))
            {
                extrenalRound.gameObject.SetActive(false);
                internalRound.transform.localPosition = Vector3.zero;
            }
        }
    }
}
