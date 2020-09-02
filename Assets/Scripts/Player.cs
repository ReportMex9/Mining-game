﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float distance = 50f;
    [SerializeField] private GameObject interactText;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectsCheck();
    }

    void ObjectsCheck()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, layerMask))
        {
            interactText.SetActive(true);
            RemoveMine(hit);
            OpenDoor(hit);
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    void RemoveMine(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider.tag == "Mine")
            {
                Manager.GetManager().AddMinesCount();
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void OpenDoor(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider.tag == "Door")
            {
                Animator anim = hit.collider.gameObject.GetComponent<Animator>();
                bool isOpen = anim.GetBool("isOpen");
                anim.SetBool("isOpen", !isOpen);
            }
        }
    }
    
}