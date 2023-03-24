using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorseMove : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float mouseSensitivity = 10;


    //[SerializeField] bool isMove = false;
    [SerializeField] bool isJump = false;
    [SerializeField] bool onGround = true;

    [SerializeField] float force = 10;
    [SerializeField] Transform targetJump;
    [SerializeField] BoxCollider water;
    [SerializeField] Transform cam;
    [SerializeField] Transform sky;
    [SerializeField] float smoothSky = -10;

    //[SerializeField] float heightHorse;
    
    [SerializeField] float timePutBrick;
    [SerializeField] int numberOfBricks = 10;

    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickGroup;
    [SerializeField] List<GameObject> brickList;

    float currentTime;
    Rigidbody bodyHorse;
    //SphereCollider bodyCollider;

    void Start()
    {
        bodyHorse = GetComponent<Rigidbody>();
        //bodyCollider = bodyHorse.GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (UIManager.Instance.isPlay)
        {
            /*if (isMove) */
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.y < -1)
            {
                cam.SetParent(null);
                //isMove = false;
                UIManager.Instance.isLose = true;
            }

            if (/*isMove && */Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + mouseX, 0);
                sky.transform.localPosition = new Vector3(sky.transform.localPosition.x + mouseX * smoothSky, sky.transform.localPosition.y, 0);
                if (sky.transform.localPosition.x >= 400) sky.transform.localPosition = new Vector3(-112, sky.transform.localPosition.y, 0);
                else if (sky.transform.localPosition.x <= -400) sky.transform.localPosition = new Vector3(112, sky.transform.localPosition.y, 0);
            }
            
            /*if (Input.GetKeyDown(KeyCode.A))
            {
                bodyHorse.velocity = Vector3.zero;
                transform.position = new Vector3(0, 0.15f, 0);
                cam.SetParent(transform);
                cam.transform.position = new Vector3(0, 3.1f, -6.09f);
                cam.eulerAngles = new Vector3(20.4f, 0, 0);
            }*/

            if (!onGround && !isJump)
            {
                OnWater();
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ground")
        {
            onGround = true;
            isJump = false;
            /*if (isJump)
            {
                isJump = false;
                bodyCollider.isTrigger = false;
            }*/
            water.enabled = true;
        }
        if (collision.collider.tag == "water")
        {
            onGround = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "brick")
        {
            Destroy(other.gameObject);
            numberOfBricks++;
            GameObject brickAdd = Instantiate(brick,brickGroup.transform);
            brickAdd.transform.localPosition = new Vector3(0, 0, 0.0075f * numberOfBricks);
            brickList.Add(brickAdd);
            UIManager.Instance.addBrick.Play();
        }
        if (other.tag == "finish")
        {
            UIManager.Instance.isFinish = true;
            //isMove = false;
        }
        /*if (other.tag == "ground")
        {
            isJump = false;
            bodyCollider.isTrigger = false;
        }*/
        
    }
    void OnWater()
    {
        if (numberOfBricks > 0)
        {
            if (timePutBrick + currentTime < Time.time)
            {
                currentTime = Time.time;
                PutBrick();
            }
        }
        else
        {
            isJump = true;
            bodyHorse.velocity = (targetJump.transform.position - transform.position).normalized * force;
            //bodyCollider.isTrigger = true;
            water.enabled = false;
            UIManager.Instance.jump.Play();
        }
    }
    void PutBrick()
    {
        numberOfBricks--;
        brickList[numberOfBricks].transform.localPosition = new Vector3(0, 0, -0.2f);
        brickList[numberOfBricks].transform.SetParent(null);
        brickList.Remove(brickList[numberOfBricks]);
        UIManager.Instance.putBrick.Play();
    }
}

