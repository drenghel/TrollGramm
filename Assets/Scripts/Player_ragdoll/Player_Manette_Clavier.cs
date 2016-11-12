﻿using UnityEngine;

public class Player_Manette_Clavier : MonoBehaviour
{
    public new GameObject camera = null;
    public float Speed = 8.0f;
    public float RotationSpeed = 50.0f;
    public float JumpHeight = 4.0f;

    private bool isGrounded = false;

    private float forwardVelocity;

    private Animator _animator;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    void Update()
    {


        Move();
        Rotation();
        Jump();
    }

    void Move()
    {
        float moveH = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        transform.Translate(new Vector3(moveH, 0, moveV));


        _animator.SetBool(CharacterAnimatorState.isWalkingStraight.ToString(), moveV > 0);
        _animator.SetBool(CharacterAnimatorState.isWalkingStraightBack.ToString(), moveV < 0);
        _animator.SetFloat(CharacterAnimatorState.XWalking.ToString(), Input.GetAxis("Vertical"));

    }

    void Rotation()
    {
        float rotH = Input.GetAxis("XboxleftX") * RotationSpeed * Time.deltaTime;
        float rotV = Input.GetAxis("XboxleftY") * RotationSpeed * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(0, rotV, 0);


        Quaternion nextRotation = camera.transform.rotation * Quaternion.Euler(rotH, 0, 0);
        if (nextRotation.eulerAngles.x <= 80 || nextRotation.eulerAngles.x >= 280)
            camera.transform.rotation = nextRotation;

    }

    void Jump()
    {
        float jump = Input.GetAxis("Jump") * JumpHeight * Time.deltaTime;

        if (isGrounded)
        {

            rb.velocity = new Vector3(0, jump, 0);
            if (jump > 0 && isGrounded)
            {

                _animator.SetTrigger(CharacterAnimatorState.ForwardJump.ToString());
            }
        }

    }

    void OnCollisionStay(Collision other)
    {
        if (useGUILayout)
        {

            isGrounded = true;
            _animator.SetBool(CharacterAnimatorState.isGrounded.ToString(), true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (isGrounded)
        {
            isGrounded = false;
            _animator.SetBool(CharacterAnimatorState.isGrounded.ToString(), false);

        }
    }


    void CalcForwardVelocity()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        forwardVelocity = localVelocity.z;

        if (forwardVelocity > 0.1f)
            Debug.Log(forwardVelocity);
    }
    /*void OnCollisionEnter(Collider other)
	{
		if(other.gameObject.tag == "Pacman")
		{
			Destroy (gameObject);
			SceneManager.LoadScene ("Game Over");
		}
	}*/
}



enum CharacterAnimatorState
{
    isWalkingStraight,
    isWalkingStraightBack,
    XWalking,
    YWalking,
    ForwardJump,
    isGrounded
}

enum Tags
{
    Ground
}