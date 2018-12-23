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
    public InputField mainInputField;

    //for internal referencing
    private Rigidbody2D playerRB;
    //private Animator anim;
    //private NetworkAnimator netAnim;
    private SpriteRenderer rendy;
    
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
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //netAnim = GetComponent<NetworkAnimator>();
        rendy = GetComponent<SpriteRenderer>();


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
                rendy.flipX = true;
                facing = Vector2.left;
                // invoke the change on the Server as you already named the function
                //CmdProvideFlipStateToServer(rendy.flipX);
            }
            else if (moveHorizontal > 0)
            {
                rendy.flipX = false;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, 1.5f);
        Debug.DrawRay(transform.position, facing * 1.5f, Color.green, 5.5f);
        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("Key"))
            {
                hit.collider.GetComponent<Key>().interact();
            }
            else if (hit.collider.tag.Equals("Info"))
            {
                hit.collider.GetComponent<Info>().interact();
            }
            else if (hit.collider.tag.Equals("PInfo"))
            {
                hit.collider.GetComponent<PicInfo>().interact();
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
