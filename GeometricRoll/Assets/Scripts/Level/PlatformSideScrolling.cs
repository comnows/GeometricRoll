using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSideScrolling : MonoBehaviour
{
    [Range(1f, 10f)][SerializeField] private float scrollSpeed;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
}
