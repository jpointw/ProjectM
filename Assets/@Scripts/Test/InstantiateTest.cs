using System;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public MinerController minerController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(minerController, Vector3.zero, Quaternion.identity);
        }
    }
}
