using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

    private void Awake()
    {
        Instantiate(gameManager, Vector3.zero, Quaternion.identity);
    }
}
