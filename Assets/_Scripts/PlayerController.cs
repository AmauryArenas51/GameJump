using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const string SPEED_MULTIPLIER = "Speed Multiplier";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string GROUND = "Ground";
    private const string SPEED_F = "Speed_f";
    private const string DEATH_B = "Death_b";
    private const string DEATH_TYPE_INT = "DeathType_int";
    private const string OBSTACLE = "Obstacle";

    private Rigidbody playerRb; //Null
    public float jumpForce;
    public float gravityMultiplier;

    public bool isOnGround = true;

    private bool _gameOver = false;
    public bool GameOver { get => _gameOver; }

    private Animator _animator;

    private float speedMultiplier = 1;

    public ParticleSystem explosion, dust;

    public AudioClip jumpSound, crashSound;

    private AudioSource _audioSource;

    [Range(0, 1)]
    public float audioVolume;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = gravityMultiplier * new Vector3(0, -9.81f, 0);
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F, 1);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime / 10;

        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !GameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //F = m*a
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);

            dust.Stop();

            _audioSource.PlayOneShot(jumpSound, audioVolume);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            if (!GameOver)
            {
                isOnGround = true;
                dust.Play();
            }
        }
        else if (collision.gameObject.CompareTag(OBSTACLE))
        {
            _gameOver = true;
            explosion.Play();
            dust.Stop();


            _animator.SetBool(DEATH_B, true);
            _animator.SetInteger(DEATH_TYPE_INT, Random.Range(1, 3));

            _audioSource.PlayOneShot(crashSound, audioVolume);

            Invoke("RestartGame", 1f);
        }

    }

    void RestartGame()
    {
        speedMultiplier = 1;
        SceneManager.LoadSceneAsync("Prototype 3", LoadSceneMode.Single);
    }

}
