using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OctoCon : NetworkBehaviour
{

    //imported objects
    public Camera cam;

    public GameObject UIcam;

    //for internal referencing
    public Rigidbody2D playerRB;
    public SpriteRenderer rendy;
    
    private Vector2 facing;

    //spawn point
    private bool respawn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        //transform.position = new Vector3(0, 0, 0);
        //playerRB = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //netAnim = GetComponent<NetworkAnimator>();
        //rendy = GetComponent<SpriteRenderer>();
        UIcam = GameObject.Find("MMCan");
        UIcam.GetComponent<Canvas>().enabled = false;


    }


    // Update is called once per frame
    void Update()
    {

        Move();
        if (Input.GetButtonDown("Space"))
        {
            interact();
        }


    }

    //to move player + animations
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 5.000001f;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
        {
            //anim.SetBool("moveSide", true);
            //anim.SetBool("moveUp", false);
            //anim.SetBool("moveDown", false);
            //anim.SetBool("isMoving", true);
            if (moveHorizontal < 0)
            {
                //rendy.flipX = true;
                facing = Vector2.left;
                // invoke the change on the Server as you already named the function
                //CmdProvideFlipStateToServer(rendy.flipX);
            }
            else if (moveHorizontal > 0)
            {
                //rendy.flipX = false;
                facing = Vector2.right;
                // invoke the change on the Server as you already named the function
                //CmdProvideFlipStateToServer(rendy.flipX);
            }
        }
        else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical))
        {
            //anim.SetBool("moveSide", false);
            //anim.SetBool("isMoving", true);
            if (moveVertical > 0)
            {
                facing = Vector2.up;
                //anim.SetBool("moveUp", true);
                //anim.SetBool("moveDown", false);
            }

            else
            {
                facing = Vector2.down;
                //anim.SetBool("moveUp", false);
                //anim.SetBool("moveDown", true);
            }

        }
        else if (moveVertical == 0 && moveHorizontal == 0)
        {
            //anim.SetBool("isMoving", false);
        }
       
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        playerRB.transform.Translate(movement);

    }

    //interaction script
    private void interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, 1f);
        Debug.DrawRay(transform.position, facing * 1f, Color.green, 5.5f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer.Equals(9))
            {
                hit.collider.GetComponent<Key>().interact();
            }
            else if (hit.collider.gameObject.layer.Equals(10))
            {
                hit.collider.GetComponent<Info>().interact();
            }
            else if (hit.collider.gameObject.layer.Equals(11))
            {
                hit.collider.GetComponent<PicInfo>().interact();
            }
            else if (hit.collider.gameObject.layer.Equals(8))
            {
                hit.collider.GetComponent<Info>().interact();
            }       
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {

        
    }

    [Command]
    void CmdDestroy(GameObject state)
    {
        // make the change local on the server
        Debug.Log("Triggered");
        NetworkServer.Destroy(state);

    }

}
