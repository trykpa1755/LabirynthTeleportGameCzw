using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 12f; // szybkoœæ naszej postaci
    Vector3 velocity; // pos³u¿y do obliczenia prêdkoœci w dó³
    CharacterController characterController; // komponent Character Controller

    public Transform groundCheck; // miejsce na nasz obiekt
    public LayerMask groundMask; // grupa obiektów, które bêd¹ warstw¹ uznawan¹ za teren



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

        RaycastHit hit; // zmienna, w której zapisywana jest referencja do uderzonego obiektu

        if(Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down),
            out hit, 0.4f, groundMask))
        {
            string terrainType;
            terrainType = hit.collider.gameObject.tag;

            switch(terrainType)
            {
                default: // standardowa prêdkoœæ gdy chodzimy po dowolnym terenie
                    speed = 12;
                    break;

                case "Low":
                    speed = 3;
                    break;

                case "High":
                    speed = 20;
                    break;
            }
                
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "PickUp")
        {
            hit.gameObject.GetComponent<PickUp>().Picked();
        }
    }



}
