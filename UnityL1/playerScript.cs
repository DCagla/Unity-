using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    int totalBox = 5;
    int touchedBox = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material.color = Color.red;
        Debug.Log("Player Created");  
    }

    // Update is called once per frame
    void Update()
    {
        // this = gameObject
        //this.transform.Translate( new Vector2(1.0f,0.0f) * Time.deltaTime);
        if(Input.GetAxisRaw("Horizontal") > 0){
            this.transform.Translate(new Vector3(10.0f, 0) * Time.deltaTime);   
        }
        if(Input.GetAxisRaw("Horizontal") < 0){
            this.transform.Translate(new Vector3(-10.0f, 0) * Time.deltaTime);   
        }
        if(Input.GetAxisRaw("Vertical") > 0){
            this.transform.Translate(new Vector3(0, 10.0f) * Time.deltaTime);  
        }
        if(Input.GetAxisRaw("Vertical") < 0){
            this.transform.Translate(new Vector3(0, -10.0f) * Time.deltaTime);
        }
    }

   void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Box"))
    {
        // Kutunun rengini yeşile çevir
        SpriteRenderer boxRenderer = col.gameObject.GetComponent<SpriteRenderer>();
        if (boxRenderer != null && boxRenderer.color != Color.green)
        {
            boxRenderer.color = Color.green;
            touchedBox++;

            Rigidbody2D boxRigidbody = col.gameObject.GetComponent<Rigidbody2D>();
            if (boxRigidbody != null)
            {
                boxRigidbody.isKinematic = true;
            }

            if (touchedBox == totalBox)
            {
                Debug.Log("You Win");
            }
        }
    }
}


    
}
