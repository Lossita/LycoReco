using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopScript : MonoBehaviour
{

    public Camera Camera;
    public GameObject Chinako;
    public GameObject BulletHole;
    private Vector2 _screenRes = new(Screen.width, Screen.height);
    private Vector2 _worldTopRight;
    private Sprite _lastSprite;
    private Vector2 _initPositon;
    private Vector2 _mostRight;
    private Vector2 _mostLeft;


    // Start is called before the first frame update
    void Start()
    {
        _worldTopRight = Camera.ScreenToWorldPoint(_screenRes);
        _initPositon = Chinako.transform.position = new Vector2(0, -_worldTopRight.y);
        _lastSprite = Chinako.GetComponent<SpriteRenderer>().sprite;
        _mostRight = Vector2.right * _worldTopRight.x * 2;
        _mostLeft = Vector2.left * _worldTopRight * 2;
    }

    void FixedUpdate()
    {


        if (Chinako.GetComponent<SpriteRenderer>().sprite.name != _lastSprite.name)
        {
            Chinako.GetComponent<PolygonCollider2D>().TryUpdateShapeToAttachedSprite();
            _lastSprite = Chinako.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Input.mousePosition;

            Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

            if (hit)
            {
                var audioSource = Camera.GetComponent<AudioSource>();
                audioSource.Play();
                print(hit.point);
                var dir = DifferDirection(screenPos);
                float distance = 0;
                if (dir == -1) //left
                {
                    var tempV2 = _mostRight + Vector2.up * screenPos.y;
                    var hit2 = Physics2D.Raycast(tempV2, Vector2.left);
                    var pos = hit2.point;
                    Debug.DrawLine(tempV2,pos);
                    print(pos);
                    distance = Math.Abs(screenPos.x - pos.x);
                    Chinako.GetComponent<Animator>().Play("LeftMove");
                }
                else if (dir == 1)
                {
                    var tempLeft = _mostLeft + Vector2.up * screenPos.y;
                    var hitLeft = Physics2D.Raycast(tempLeft, Vector2.right);
                    var posLeft = hitLeft.point;
                    Debug.DrawLine(tempLeft,posLeft);
                    print(posLeft);
                    distance = Math.Abs(screenPos.x - posLeft.x);
                    Chinako.GetComponent<Animator>().Play("RightMove");
                }

                print(distance + 0.25);
                Chinako.transform.position += Vector3.right * (distance+0.5F) * dir;
                var holePos = new Vector3(hit.point.x, hit.point.y);
                GameObject hole = GameObject.Instantiate(BulletHole,holePos,new Quaternion(0,0,0,0));
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            Chinako.transform.position = _initPositon;

    }

    int DifferDirection(Vector2 position)
    {
        return position.x > Chinako.transform.position.x ? -1 : 1;
    }

}
