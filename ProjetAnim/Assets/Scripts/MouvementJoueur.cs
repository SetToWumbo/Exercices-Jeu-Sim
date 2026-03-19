using UnityEngine;
using UnityEngine.InputSystem;

public class MouvementJoueur : MonoBehaviour
{
    [SerializeField] private float vitesseSaut = 5;
    [SerializeField] private float vitesseBase = 5;
    [SerializeField] private float vitesseSprint = 2;

    private CharacterController characterController;
    private InputAction actionMouvement;
    private InputAction actionSaut;
    private InputAction actionCourse;
    private InputAction actionAttaque;

    private Animator _controller;
    private AnimationClip[] allClips;
    private AnimationClip clipMarche;
    private bool _isWalking = false;
    private float vitesseY = 0;

    void Start()
    {
        _controller =
            gameObject.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        _controller.SetBool("Walk", false);

        actionMouvement = InputSystem.actions.FindAction("Move");
        actionSaut = InputSystem.actions.FindAction("Jump");
        actionCourse = InputSystem.actions.FindAction("Sprint"); // Ex.6
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _controller.SetTrigger("Attack");
        }

        Vector2 inputMouvement = actionMouvement.ReadValue<Vector2>();
        _isWalking = inputMouvement.x == 0 && inputMouvement.y == 0;
        _controller.SetBool("Walk", _isWalking);
        Vector3 directionMouvement = new Vector3(inputMouvement.x, 0, inputMouvement.y);

        // Calcul la vitesse
        float vitesse = vitesseBase;
        if (actionCourse.IsPressed())
        {
            vitesse *= vitesseSprint;
        }

        float animationSpeed = vitesse > vitesseBase ? 2.5f : 1.5f;
        _controller.SetFloat("WalkSpeed", animationSpeed);

        // Dirige la vitesse selon axe local
        Vector3 vitesseLocale = vitesse * directionMouvement;
        vitesseLocale = transform.TransformDirection(vitesseLocale);

        // Calcule la vitesse en y (gravite + saut)
        if (characterController.isGrounded)
        {
            if (actionSaut.IsPressed()) vitesseY = vitesseSaut;
            else vitesseY = 0;
        }
        else
        {
            vitesseY += Physics.gravity.y * Time.deltaTime;
        }

        // Deplace le joueur
        Vector3 vitesseApplique = vitesseLocale + new Vector3(0, vitesseY, 0);
        characterController.Move(vitesseApplique * Time.deltaTime);
    }
}