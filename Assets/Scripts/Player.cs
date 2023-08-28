using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [HeaderAttribute("Settings")]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity;
    private float horizontal, vertical;
    private Vector3 direction, directionForEnemy;
    [SerializeField]
    private float _jumpHeight;
    private float _mouseX, _mouseY;
    [SerializeField]
    private float _sensitivity;
    public float _money;
    public Health _health;
    public AudioSource _footsteps;

   
    void Start()
    {
        _footsteps = GetComponent<AudioSource>();
        _controller = GetComponent<CharacterController>();
        _health = GetComponent<Health>();

        _speed = 5f;
        _gravity = 20f;
        _jumpHeight = 10f;
        _sensitivity = 1.0f;
        UIManager.Instance.UpdateMoney();
        //lock cursor on Start
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;



    }


    void Update()
    {
        if(!_health.died && !UIManager.Instance._menuOpen)
        {
            Movement();
            CameraMovement();
        }
        else if(!_health.died)
            Movement();






        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.B)) && !_health.died)
        {
            if(Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
           
        }

   
    }



    private void CameraMovement()
    {


        
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        Vector3 curentRotation = transform.localEulerAngles;
        curentRotation.y += _mouseX * _sensitivity;
        transform.localRotation = Quaternion.AngleAxis(curentRotation.y, Vector3.up);

        Vector3 CameraRotation = Camera.main.gameObject.transform.localEulerAngles;
        CameraRotation.x -= _mouseY * _sensitivity;
        CameraRotation.x = Clamp(CameraRotation.x, -90f,90.0f);
        Camera.main.gameObject.transform.localRotation = Quaternion.AngleAxis(CameraRotation.x, Vector3.right);
    }

    
    public float NormalizeAngle(float angle)
    {
        float angle_ = angle - Mathf.Floor(angle / 360f) * 360f;

        return angle_;
    }

   
    //angle >= 0 , min <= max
    public float Clamp(float angle , float min , float max)

    {
        angle = NormalizeAngle(angle);

        if (min < 0f && max >= 0f)
        {
            min = NormalizeAngle(min);

            if (angle > max && angle < min)
            {
                float d1 = Mathf.Abs(max - angle);
                float d2 = Mathf.Abs(min - angle);

                if (d1 < d2)
                {
                    return max;
                } 

                else
                {
                    return min;
                }
            }

            else if (angle <= max)
            {
                return angle;
            }

            else
            {
                return angle;
            }
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

     
      
        if (Grounded())
        {

            directionForEnemy = direction = new Vector3(horizontal, 0, vertical);

            if(direction.x!=0 || direction.z!=0)
            {
                _footsteps.enabled = true;
            }
            else if (direction.x ==0 && direction.z == 0)
            {
                _footsteps.enabled = false;
            }
            direction *= _speed;
            direction = transform.TransformDirection(direction);

            if (Input.GetKey(KeyCode.Space))
            {
                direction.y += _jumpHeight;
            }
        }
        else
        {
            _footsteps.enabled = false;
            direction = new Vector3(horizontal * _speed, direction.y, vertical * _speed);
            direction = transform.TransformDirection(direction);
        }

       

        direction.y -= _gravity * Time.deltaTime;


        _controller.Move(direction * Time.deltaTime);
    }

    public Vector3 GetDirection()
    {
        return directionForEnemy;
    }

    public bool Grounded()
    {
        bool hitInfo = Physics.Raycast(transform.position, Vector3.down, 0.2f, 1 << 9);

        return hitInfo;

    }

}
