using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHorseMove : MonoBehaviour
{
    [SerializeField] float speed = 10;

    [SerializeField] bool isMove = false;
    [SerializeField] bool onGround = true;

    //[SerializeField] BoxCollider water;

    [SerializeField] float timePutBrick;
    [SerializeField] int numberOfBricks = 10;

    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickGroup;
    [SerializeField] List<GameObject> brickList;

    float currentTime;
    void Update()
    {
        if (isMove && UIManager.Instance.isPlay) transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.y < 0.1f)
        {
            onGround = false;
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
        if (!onGround)
        {
            OnWater();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "ground")
        {
            onGround = true;
            //transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "brick")
        {
            Destroy(other.gameObject);
            numberOfBricks++;
            GameObject brickAdd = Instantiate(brick, brickGroup.transform);
            brickAdd.transform.localPosition = new Vector3(0, 0, 0.0075f * numberOfBricks);
            brickList.Add(brickAdd);
        }
        if (other.tag == "finish")
        {
            isMove = false;
            UIManager.Instance.isLate = true;
        }
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
        else isMove = false;
    }
    void PutBrick()
    {
        numberOfBricks--;
        brickList[numberOfBricks].transform.localPosition = new Vector3(0, 0, -0.2f);
        brickList[numberOfBricks].transform.SetParent(null);
        brickList.Remove(brickList[numberOfBricks]);
    }
}
