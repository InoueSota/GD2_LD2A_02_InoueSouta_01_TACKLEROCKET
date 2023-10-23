using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // ��]�p�x
    private float rotateValue;
    // ��]�p�x�̑�����
    private float rotatePower = 100f;

    // �ړ�����
    public Vector3 moveDirection;
    // ��{�ړ����x
    private float moveSpeed = 1f;

    // �`���[�W��UI
    [SerializeField] private GameObject chargeUI1;
    [SerializeField] private GameObject chargeUI2;
    [SerializeField] private GameObject chargeUI3;
    // �`���[�W�{�^����������
    private float pushChargeTime;
    // �`���[�W�{�^���������ԊԊu
    private float pushChargeInterval = 0.65f;
    // �`���[�W�ʂ𓾂�
    public int chargeCount = 0;

    // �_�b�V���J�n�t���O
    private bool isDushActive;
    // �ړ��ʁi�P�i�K�ځj
    private float movePower1 = 2f;
    // �ړ��ʁi�Q�i�K�ځj
    private float movePower2 = 3.5f;
    // �ړ��ʁi�R�i�K�ځj
    private float movePower3 = 5f;
    // �ړ��C�[�W���O
    private float moveFixedTime = 1.2f;
    private float moveLeftTime;
    public float moveTime;
    private Vector3 moveStartPosition;
    public Vector3 moveEndPosition;
    private float maxDistance;
    
    // �d�͕ω��t���O
    public bool isChangeGravityActive;
    // �d�͕ω���]�C�[�W���O
    private float startRotate;
    private float endRotate;
    // �d�͐�
    [SerializeField] private GameObject topLineObj;
    [SerializeField] private GameObject bottomLineObj;
    [SerializeField] private GameObject leftLineObj;
    [SerializeField] private GameObject rightLineObj;

    private PlayerCollision playerCollision;
    private CameraManager cameraManager;
    private GravityManager gravityManager;
    [SerializeField] private GameObject fireObj;

    void Start()
    {
        rotateValue = 90f;
        moveDirection = Quaternion.Euler(0, 0, rotateValue) * Vector2.right;
        transform.localRotation = Quaternion.Euler(0, 0, rotateValue);
        isDushActive = false;
        maxDistance = Vector3.Distance(Vector3.zero, Vector3.one * movePower3);
        playerCollision = GetComponent<PlayerCollision>();
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        gravityManager = GetComponent<Gravity>().gravityManager;
    }

    void Update()
    {
        Rotate();
        BaseMove();
        Charge();
        Dush();
        ClampInCamera();
    }

    void Rotate()
    {
        // ��
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.RightArrow)))
        {
            float deltaRotatePower = rotatePower * Time.deltaTime;
            rotateValue += deltaRotatePower;
            if (rotateValue >= 360f) { rotateValue = 0f; }
            moveDirection = Quaternion.Euler(0, 0, rotateValue) * Vector2.right;
            transform.localRotation = Quaternion.Euler(0, 0, rotateValue);
        }

        // �E
        if ((!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            float deltaRotatePower = rotatePower * Time.deltaTime;
            rotateValue -= deltaRotatePower;
            if (rotateValue <= 0f) { rotateValue = 360f; }
            moveDirection = Quaternion.Euler(0, 0, rotateValue) * Vector2.right;
            transform.localRotation = Quaternion.Euler(0, 0, rotateValue);
        }
    }

    void BaseMove()
    {
        if (!Input.GetKey(KeyCode.Space) && !playerCollision.isKnockBack && !isDushActive)
        {
            Vector3 deltaMove = moveDirection * moveSpeed * Time.deltaTime;
            transform.position += deltaMove;
        }
    }

    void Charge()
    {
        if (!playerCollision.isKnockBack && !isDushActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                pushChargeTime += Time.deltaTime;
                pushChargeTime = Mathf.Clamp(pushChargeTime, 0f, pushChargeInterval * 3f);
            }
            else if (pushChargeTime < pushChargeInterval * 1f)
            {
                pushChargeTime = 0f;
                chargeUI1.SetActive(false);
                chargeUI2.SetActive(false);
                chargeUI3.SetActive(false);
            }
            // �`���[�W�{�^���̉������Ԃ�1�b�ȏ�̂Ƃ��̓_�b�V�����s����
            else
            {
                DushInitialize();
                pushChargeTime = 0f;
                chargeUI1.SetActive(false);
                chargeUI2.SetActive(false);
                chargeUI3.SetActive(false);
            }

            if (pushChargeTime >= pushChargeInterval * 3f)
            {
                chargeUI3.SetActive(true);
            }
            else if (pushChargeTime >= pushChargeInterval * 2f)
            {
                chargeUI2.SetActive(true);
            }
            else if (pushChargeTime >= pushChargeInterval * 1f)
            {
                chargeUI1.SetActive(true);
            }
        }
    }

    void DushInitialize()
    {
        if (pushChargeTime >= pushChargeInterval * 3f)
        {
            chargeCount = 3;
            moveEndPosition = transform.position + moveDirection * movePower3;
        }
        else if (pushChargeTime >= pushChargeInterval * 2f)
        {
            chargeCount = 2;
            moveEndPosition = transform.position + moveDirection * movePower2;
        }
        else if (pushChargeTime >= pushChargeInterval * 1f)
        {
            chargeCount = 1;
            moveEndPosition = transform.position + moveDirection * movePower1;
        }
        moveStartPosition = transform.position;
        float nowDistance = Vector3.Distance(transform.position, new(moveEndPosition.x, moveEndPosition.y, transform.position.z));
        float t = nowDistance / maxDistance;
        moveTime = moveFixedTime * t;
        moveLeftTime = moveTime;
        fireObj.SetActive(true);
        isDushActive = true;
    }

    void Dush()
    {
        if (isDushActive)
        {
            moveLeftTime -= Time.deltaTime;
            if (moveLeftTime <= 0f) { moveLeftTime = 0f; }
            float t = moveLeftTime / moveTime;
            transform.position = Vector3.Lerp(moveEndPosition, moveStartPosition, t * t);
            if (isChangeGravityActive)
            {
                rotateValue = Mathf.Lerp(endRotate, startRotate, t * t);
                moveDirection = Quaternion.Euler(0, 0, rotateValue) * Vector2.right;
                transform.localRotation = Quaternion.Euler(0, 0, rotateValue);
            }
            if (moveLeftTime == 0f) 
            { 
                isDushActive = false;
                isChangeGravityActive = false;
                fireObj.SetActive(false);
                chargeCount = 0;
            }
        }
    }

    void ClampInCamera()
    {
        if (cameraManager)
        {
            float cameraLeft = -cameraManager.halfWidth;
            float cameraRight = cameraManager.halfWidth;
            float cameraTop = cameraManager.halfHeight;
            float cameraBottom = -cameraManager.halfHeight;

            float thisLeft = transform.position.x - transform.localScale.x * 0.5f;
            if (cameraLeft > thisLeft)
            {
                ChangeGravityInitialize();
                endRotate = 180f;
                SetActiveGravityLine(leftLineObj);
                gravityManager.GravityInitialize(GravityManager.GravityPattern.LEFT);
                moveEndPosition = new(cameraRight - transform.localScale.x * 0.5f, moveStartPosition.y, moveStartPosition.z);
            }

            float thisRight = transform.position.x + transform.localScale.x * 0.5f;
            if (cameraRight < thisRight)
            {
                ChangeGravityInitialize();
                endRotate = 0f;
                SetActiveGravityLine(rightLineObj);
                gravityManager.GravityInitialize(GravityManager.GravityPattern.RIGHT);
                moveEndPosition = new(cameraLeft + transform.localScale.x * 0.5f, moveStartPosition.y, moveStartPosition.z);
            }

            float thisTop = transform.position.y + transform.localScale.y * 0.5f;
            if (cameraTop < thisTop)
            {
                ChangeGravityInitialize();
                endRotate = 90f;
                SetActiveGravityLine(topLineObj);
                gravityManager.GravityInitialize(GravityManager.GravityPattern.TOP);
                moveEndPosition = new(moveStartPosition.x, cameraBottom + transform.localScale.y * 0.5f, moveStartPosition.z);
            }

            float thisBottom = transform.position.y - transform.localScale.y * 0.5f;
            if (cameraBottom > thisBottom)
            {
                ChangeGravityInitialize();
                endRotate = 270f;
                SetActiveGravityLine(bottomLineObj);
                gravityManager.GravityInitialize(GravityManager.GravityPattern.BOTTOM);
                moveEndPosition = new(moveStartPosition.x, cameraTop - transform.localScale.y * 0.5f, moveStartPosition.z);
            }
        }
    }

    void SetActiveGravityLine(GameObject nextActiveLine)
    {
        if (topLineObj && topLineObj.activeSelf)
        {
            topLineObj.SetActive(false);
        }
        if (bottomLineObj && bottomLineObj.activeSelf)
        {
            bottomLineObj.SetActive(false);
        }
        if (leftLineObj && leftLineObj.activeSelf)
        {
            leftLineObj.SetActive(false);
        }
        if (rightLineObj && rightLineObj.activeSelf)
        {
            rightLineObj.SetActive(false);
        }
        nextActiveLine.SetActive(true);
        nextActiveLine.GetComponent<GravityLineManager>().Create();
    }

    void ChangeGravityInitialize()
    {
        fireObj.SetActive(false);
        moveStartPosition = transform.position;
        moveTime = 1f;
        moveLeftTime = moveTime;
        playerCollision.isKnockBack = false;
        startRotate = rotateValue;
        isChangeGravityActive = true;
        isDushActive = true;
    }

    public void HitMeteoriteInitialize()
    {
        fireObj.SetActive(false);
        isDushActive = false;
        isChangeGravityActive = false;
    }
}
