using Assets.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    //[SerializeField] private GameObject basicBallPrefab;
    //[SerializeField] private GameObject bigBallPrefab;
    //[SerializeField] private GameObject explosiveBallPrefab;


    [SerializeField] private Rigidbody2D pivot;
    //[SerializeField] private Transform arrowT;
    [SerializeField] private SpriteRenderer arrow;
    [SerializeField] private float arrowLengthMultiplier;

    [SerializeField] private float maxPullLength;
    [SerializeField] private float slingStrength;
    [SerializeField] private float ballRespawnDelay;

    [SerializeField] private BallInventoryUI BallInventoryUI;


    //[SerializeField] private GameObject currentBall;
    private GameObject currentBall;
    public GameObject CurrentBall { 
        get { return currentBall; }
        set 
        {
            currentBall = value;
            if (currentBall != null)
                currentBallRb = value.GetComponent<Rigidbody2D>();
            else
                currentBallRb = null;
        } 
    }
    public BallType currentBallType = BallType.BasicBall;
    [SerializeField] private Rigidbody2D currentBallRb;
    [SerializeField] private GameObject[] ballPrefabs;
    public float[] ballCounts;
    //public GameObject selectedBallType;


    private Camera mainCamera;
    private bool isDragging = false;

    public Vector3 directionUp;//no need?



    //public float basicBallCount;
    //public float bigBallCount;
    //public float explosiveBallCount;
    public event EventHandler OnBallLaunched;
    // Start is called before the first frame update
    void Start()
    {
        //ballCounts = new float[ballPrefabs.Length];
        mainCamera = Camera.main;
        //arrow = arrowT.GetComponent<SpriteRenderer>();

        SpawnNewBall();
    }


    // Update is called once per frame
    void Update()
    {

        Sling();
    }
    public bool SelectBallType(BallType ballType)
    {
        bool ballSelected = false;
        //switch (ballType)
        //{
        //    case BallType.BasicBall:
        //        if (basicBallCount > 0)
        //            ballSelected = true;
        //        break;
        //    case BallType.BigBall:
        //        if (bigBallCount > 0)
        //            ballSelected = true;
        //        break;
        //    case BallType.ExplosiveBall:
        //        if (explosiveBallCount > 0)
        //            ballSelected = true;
        //        break;
        //    default:
        //        break;
        //}
        if (ballCounts[(int)ballType] > 0)
        {
            ballSelected = true;
            if(CurrentBall!=null)
                Destroy(CurrentBall);
            currentBallType = ballType;
            SpawnNewBall();
        }
        return ballSelected;
    }
    private void Sling()
    {
        if (currentBallRb == null) { return; }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            arrow.enabled = false;
            if (isDragging)
            {
                LaunchBall();
            }
            isDragging = false;
            return;
        }
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        if (Vector2.Distance(worldPosition, pivot.position) > maxPullLength && !isDragging)
            return;

        isDragging = true;
        arrow.enabled = true;
        //currentBallRigidbody.isKinematic = true;

        if(Vector2.Distance(worldPosition, pivot.position) > maxPullLength)
        {
            currentBallRb.position = pivot.position+(new Vector2(worldPosition.x - pivot.position.x, worldPosition.y - pivot.position.y)).normalized* maxPullLength;
            Debug.Log("if");
        }
        else
        {
            currentBallRb.position = worldPosition;
            Debug.Log("else");
        }

        

        //arrow.LookAt(currentBallRb.position, directionUp);
        arrow.transform.right = -(new Vector3(currentBallRb.position.x, currentBallRb.position.y,0) - arrow.transform.position);
        
        arrow.size = new Vector2(Vector2.Distance(pivot.position, currentBallRb.position) * arrowLengthMultiplier, arrow.size.y);
        
    }
    private void SpawnNewBall()
    {
        //GameObject ballPrefab;
        //switch (currentBallType)
        //{
        //    case BallType.BasicBall:
        //        ballPrefab = basicBallPrefab;
        //        break;
        //    case BallType.BigBall:
        //        ballPrefab = bigBallPrefab;
        //        break;
        //    case BallType.ExplosiveBall:
        //        ballPrefab = explosiveBallPrefab;
        //        break;
        //    default:
        //        ballPrefab = basicBallPrefab;
        //        break;
        //}
        //currentBallRb = Instantiate(ballPrefab, pivot.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        if (ballCounts[(int)currentBallType] > 0)
            //currentBallRb = Instantiate(ballPrefabs[(int)currentBallType], pivot.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            CurrentBall = Instantiate(ballPrefabs[(int)currentBallType], pivot.position, Quaternion.identity);
    }

    private void LaunchBall()
    {
        Vector2 force = pivot.position - currentBallRb.position;
        currentBallRb.AddForce(force *currentBall.GetComponent<Ball>().speed * slingStrength, ForceMode2D.Impulse);
        currentBallRb.GetComponent<CircleCollider2D>().enabled = true;
        currentBallRb.GetComponent<Ball>().OnFired();
        currentBallRb = null;

        //switch (currentBallType)
        //{
        //    case BallType.BasicBall:
        //        basicBallCount--;
        //        break;
        //    case BallType.BigBall:
        //        bigBallCount--;
        //        break;
        //    case BallType.ExplosiveBall:
        //        explosiveBallCount--;
        //        break;
        //    default:
        //        break;
        //}
        ballCounts[(int)currentBallType]--;
        OnBallLaunched?.Invoke(this, EventArgs.Empty);
        //Invoke(nameof(SpawnNewBall), ballRespawnDelay);
        SpawnNewBall();
    }

    //private void DetachBall()
    //{
    //    currentBallSprintJoint.enabled = false;
    //    currentBallSprintJoint = null;

    //    Invoke(nameof(SpawnNewBall), respawnDelay);
    //}
}
