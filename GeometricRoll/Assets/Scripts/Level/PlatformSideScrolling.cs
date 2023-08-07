using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSideScrolling : MonoBehaviour
{
    [SerializeField] private GameObject platformGroup;

    [Range(1f, 10f)][SerializeField] private float scrollSpeed;

    void Update()
    {
        MoveToLeft(platformGroup);
    }

    private void MoveToLeft(GameObject platform)
    {
        platform.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
}
