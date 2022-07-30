using EtVK.Input_Module;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerHead;
        
        private CharacterController characterController;
        private Transform cameraMainTransform;
        private PlayerManager playerManager;
        private RaycastHit slopeHit;

        private void Awake()
        {
            InitializeReferences();
        }

        private void Update()
        {
            if (characterController.isGrounded && playerManager.IsJumping)
            {
                playerManager.IsJumping = false;
                InputManager.Instance.JumpInputBlocked = true;
            }

            if (characterController.isGrounded && playerManager.DownVelocity.y < -0.5f)
            {
                var newDownVelocity = playerManager.DownVelocity;
                newDownVelocity.y = -0.5f;
                playerManager.DownVelocity = newDownVelocity;
            }

        }
        
        // Default value for speed is the walk speed, can be overridden
        public void UpdateNormalMovement(Vector2 movement, float speed = 0f, float rotationSpeed = 0f)
        {
            speed = speed == 0f ? playerManager.GetLocomotionData().WalkSpeed : speed;
            rotationSpeed = rotationSpeed == 0f ? playerManager.GetLocomotionData().RotationSpeed : rotationSpeed;
            
            
            var move = new Vector3(movement.x, 0f, movement.y);
            playerHead.rotation = Quaternion.Euler(0f, cameraMainTransform.localEulerAngles.y, 0f);
            move = playerHead.forward * move.z + playerHead.right * move.x;
            move.y = 0f;
            characterController.Move(move * Time.deltaTime * speed);

            var targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            var rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
        public void UpdateCombatMovement(Vector2 movement, float speed = 0f, float rotationSpeed = 0f)
        {
            speed = speed == 0f ? playerManager.GetLocomotionData().WalkSpeed : speed;
            rotationSpeed = rotationSpeed == 0f ? playerManager.GetLocomotionData().RotationSpeed : rotationSpeed;
            
            
            var move = new Vector3(movement.x, 0f, movement.y);
            playerHead.rotation = Quaternion.Euler(0f, cameraMainTransform.localEulerAngles.y, 0f);
            move = playerHead.forward * move.z + playerHead.right * move.x;
            move.y = 0f;
            characterController.Move(move * Time.deltaTime * speed);


            var targetAngle = cameraMainTransform.eulerAngles.y;
            var rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        
        public void UpdateForceMovementForward(float speed = 0f)
        {
            speed = speed == 0f ? playerManager.GetLocomotionData().WalkSpeed : speed;
            characterController.Move(transform.forward * Time.deltaTime * speed);
        }

        public void Move(Vector3 position)
        {
            characterController.Move(position);
        }
        
        public void Jump(float gravityValue = 0f, float gravityMultiplayer = 0f, float jumpHeight = 0f)
        {
            gravityValue = gravityValue == 0f ? playerManager.GetLocomotionData().GravityValue : gravityValue;
            gravityMultiplayer = gravityMultiplayer == 0f ? playerManager.GetLocomotionData().GravityMultiplayer : gravityMultiplayer;
            jumpHeight = jumpHeight == 0f ? playerManager.GetLocomotionData().JumpHeight : jumpHeight;
            playerManager.IsJumping = true;
            
            //Velocity * gravity
            var newDownVelocity = playerManager.DownVelocity;
            newDownVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue * gravityMultiplayer);
            playerManager.DownVelocity = newDownVelocity; 
            characterController.Move(playerManager.DownVelocity * Time.deltaTime);
        }
        
        public void UpdateGravity(float gravityValue = 0f, float gravityMultiplayer = 0f)
        {
            gravityValue = gravityValue == 0f ? playerManager.GetLocomotionData().GravityValue: gravityValue;
            gravityMultiplayer = gravityMultiplayer == 0f ? playerManager.GetLocomotionData().GravityMultiplayer : gravityMultiplayer;

            if (!characterController.isGrounded)
            {
                var newDownVelocity = playerManager.DownVelocity;
                newDownVelocity.y += gravityValue * gravityMultiplayer * Time.deltaTime;
                playerManager.DownVelocity = newDownVelocity;
            }

            characterController.Move(playerManager.DownVelocity * Time.deltaTime);
        }
        
        public void UpdatePlayerRotation(float rotationSpeed = 0f)
        {
            rotationSpeed = rotationSpeed == 0f ? playerManager.GetLocomotionData().RotationSpeed : rotationSpeed;
            
            var rotation = Quaternion.Euler(0f, cameraMainTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        public void UpdatePlayerRootMotionRotation(Animator animator, float rotationSpeed = 0f)
        {
            rotationSpeed = rotationSpeed == 0f ? playerManager.GetLocomotionData().RotationSpeed : rotationSpeed;
            
            var rotation = Quaternion.Euler(0f, cameraMainTransform.eulerAngles.y, 0f);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            animator.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        public void MoveOnSlope()
        {
            //If we are on a slope we smoothly go down
            if (playerManager.IsMoving() && OnSlope() && !OnSteepSlope())
            {
                characterController.Move(Vector3.down * characterController.height / 2 * playerManager.GetLocomotionData().SlopeFore * Time.deltaTime);
            }
            if (OnSteepSlope())
            {
                SteepSlopeMovement();
            }

        }
        
        private bool OnSlope()
        {
            if (playerManager.IsJumping)
                return false;

            if (Physics.Raycast(transform.position,
                    Vector3.down, out var hit, characterController.height / 2 * playerManager.GetLocomotionData().SlopeForceRayLenght))
            {
                if (hit.normal != Vector3.up)
                    return true;
            }

            return false;
        }

        private bool OnSteepSlope()
        {
            if (!characterController.isGrounded)
                return false;
            
            if (Physics.Raycast(transform.position,
                    Vector3.down, out var hit,
                    characterController.height / 2 * playerManager.GetLocomotionData().SlopeForceRayLenght))
            {
                var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                
                if (slopeAngle > playerManager.GetLocomotionData().SteepSlopeLimit)
                {
                    slopeHit = hit;
                    return true;
                }
            }

            return false;
        }

        private void SteepSlopeMovement()
        {
            var slopeDirection = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);

            var moveDirection = slopeDirection * -playerManager.GetLocomotionData().SteepSlopeSpeed;
            moveDirection.y = moveDirection.y - slopeHit.point.y;
            Move((moveDirection + Vector3.down * characterController.height / 2 * playerManager.GetLocomotionData().SlopeFore) * Time.deltaTime);
        } 
        
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
                return;

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3f)
                return;

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * playerManager.GetLocomotionData().PushPower;
        }
        
        
        private void InitializeReferences()
        {
            characterController = GetComponent<CharacterController>();
            playerManager = transform.root.GetComponent<PlayerManager>();
            cameraMainTransform = UnityEngine.Camera.main!.transform;
        }
    }
}
