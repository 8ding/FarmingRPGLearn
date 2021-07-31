
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : SingletonMonoBehavior<Player>
{
    // 移动参数
    private float xInput;
    private float yInput;
    private bool isCarrying = false;
    private bool isIdle;
    private bool isLiftingToolDown;
    private bool isLiftingToolLeft;
    private bool isLiftingToolRight;
    private bool isLiftingToolUp;
    private bool isRunning;
    private bool isUsingToolDown;
    private bool isUsingToolLeft;
    private bool isUsingToolRight;
    private bool isUsingToolUp;
    private bool isSwingingToolDown;
    private bool isSwingingToolLeft;
    private bool isSwingingToolRight;
    private bool isSwingingToolUp;
    private bool isWalking;
    private bool isPickingUp;
    private bool isPickingDown;
    private bool isPickingLeft;
    private bool isPickingRight;

    private Camera MainCamera;
    private ToolEffect toolEffect = ToolEffect.none;
    #pragma warning disable
    private Rigidbody2D rigidbody2D;
    private Direction playerDirection;
    #pragma warning restore
    private float movementSpeed;

    private bool _playerInputIsDisabled = false;
    private AnimationOverrides animationOverrides;
    private List<CharacterAttribute> characterAttributeCustomisationList;
    private CharacterAttribute armsCharacterAttribute;
    private CharacterAttribute toolsCharacterAttribute;
    [SerializeField] private SpriteRenderer equippedItemSpriteRenderer = null;
    public bool PlayerInputIsDisabled
    {
        get
        {
            return _playerInputIsDisabled;
        }
        set
        {
            _playerInputIsDisabled = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        rigidbody2D = GetComponent<Rigidbody2D>();
        MainCamera = Camera.main;
        animationOverrides = GetComponentInChildren<AnimationOverrides>();

        armsCharacterAttribute = new CharacterAttribute(CharacterPartAnimator.arms, PartVariantColour.none, PartVariantType.none);
        characterAttributeCustomisationList = new List<CharacterAttribute>();
    }

    private void Update()
    {
        #region PlayerInput
        if(!PlayerInputIsDisabled)
        {
            InputCheck();
            ResetAnimationTriggers();
            PlayerMovementInput();
            PlayerWalkInput();
            EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect,
                isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                false, false, false, false);

        }
        #endregion
    }

    private void InputCheck()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            TimeManager.Instance.TestAdvanceGameMinute();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            TimeManager.Instance.TestAdvanceGameDay();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            SceneControllerManager.Instance.FadeAndLoadScene("Scene1_Farm", transform.position);
            
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed, yInput * movementSpeed) * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + move);
    }
    private void ResetAnimationTriggers()
    {
        isPickingRight = false;
        isPickingLeft = false;
        isPickingUp = false;
        isPickingDown = false;
        isUsingToolRight = false;
        isUsingToolLeft = false;
        isUsingToolUp = false;
        isUsingToolDown = false;
        isLiftingToolRight = false;
        isLiftingToolLeft = false;
        isLiftingToolUp = false;
        isLiftingToolDown = false;
        isSwingingToolRight = false;
        isSwingingToolLeft = false;
        isSwingingToolUp = false;
        isSwingingToolDown = false;
        toolEffect = ToolEffect.none;
    }
    private void PlayerMovementInput()
    {
        yInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");

        if (yInput != 0 && xInput != 0)
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;

            // Capture player direction for save game
            if (xInput < 0)
            {
                playerDirection = Direction.left;
            }
            else if (xInput > 0)
            {
                playerDirection = Direction.right;
            }
            else if (yInput < 0)
            {
                playerDirection = Direction.down;
            }
            else
            {
                playerDirection = Direction.up;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }
    }
    private void PlayerWalkInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Settings.walkingSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;
        }
    }

    public void EnablePlayerInput()
    {
        _playerInputIsDisabled = false;
    }
    public void DisablePlayerInput()
    {
        _playerInputIsDisabled = true;
    }

    public void DisableInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect,
            isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
            isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
            isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
            isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
            false, false, false, false);
    }

    public void ResetMovement()
    {
        xInput = 0;
        yInput = 0;
        isRunning = false;
        isWalking = false;
        isIdle = true;
    }
    public Vector3 GetPlayerViewportPosition()
    {
        Vector3 playerViewportPosition = MainCamera.WorldToViewportPoint(transform.position);
        return playerViewportPosition;
    }

    public void ShowedCarriedItem(int itemCode)
    {
        ItemDetails itemDetails = InventoryManager.Instance.GetItemDeatails(itemCode);
        if(itemDetails != null)
        {
            equippedItemSpriteRenderer.sprite = itemDetails.itemSprite;
            equippedItemSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            armsCharacterAttribute.partVariantType = PartVariantType.carry;
            characterAttributeCustomisationList.Clear();
            characterAttributeCustomisationList.Add(armsCharacterAttribute);
            animationOverrides.ApplyCharacterCustomisationParameters(characterAttributeCustomisationList);

            isCarrying = true;
        }
    }

    public void HidedCarriedItem()
    {
        equippedItemSpriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        armsCharacterAttribute.partVariantType = PartVariantType.none;
        characterAttributeCustomisationList.Clear();
        characterAttributeCustomisationList.Add(armsCharacterAttribute);
        animationOverrides.ApplyCharacterCustomisationParameters(characterAttributeCustomisationList);

        isCarrying = false;
    }
}
