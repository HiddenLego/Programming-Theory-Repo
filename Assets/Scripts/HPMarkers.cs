using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMarkers : MonoBehaviour
{
    [SerializeField] private int threshold;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.hitsLeft < threshold)
        {
            gameObject.SetActive(false);
        }
    }
}
