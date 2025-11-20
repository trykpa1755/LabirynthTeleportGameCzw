using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 12f; // szybkoœæ naszej postaci
    Vector3 velocity; // pos³u¿y do obliczenia prêdkoœci w dó³
    CharacterController characterController; // komponent Character Controller


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }


}
