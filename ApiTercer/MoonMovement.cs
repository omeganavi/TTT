using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    public Vector2 point1;
    private void Awake()
    {
        StartCoroutine("MoonMoving", point1);
    }

    private IEnumerator MoonMoving(Vector2 point)
    {
        transform.position = Vector2.Lerp(transform.position, point, 4);
        yield return transform.position;
    }
}
