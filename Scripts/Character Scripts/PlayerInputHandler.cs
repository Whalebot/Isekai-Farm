
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerInputHandler : MonoBehaviour
{
    [FoldoutGroup("Components")] public InputManager input;
    [FoldoutGroup("Components")] public Status status;
    [FoldoutGroup("Components")] public Camera cam;
    //public Movement mov;
    [FoldoutGroup("Components")] public Movement mov;
    [FoldoutGroup("Components")] public AttackScript attack;
    [FoldoutGroup("Components")] public EquipmentScript equip;
    [FoldoutGroup("Components")] public Dodge dodge;
    [FoldoutGroup("Components")] public ToolScript tool;
    [FoldoutGroup("Components")] public CharacterAnimator animator;
    [FoldoutGroup("Components")] public InventoryScript inventory;

    [FoldoutGroup("Auto Aim")]
    [Header("Auto Aim")]
    [FoldoutGroup("Auto Aim")] public bool lockOn;
    [FoldoutGroup("Auto Aim")] public Transform lockOnIndicator;
    [FoldoutGroup("Auto Aim")] public Vector3 indicatorOffset;
    [FoldoutGroup("Auto Aim")] public bool autoAim;
    [FoldoutGroup("Auto Aim")] public List<Transform> visibleTargets = new List<Transform>();
    [FoldoutGroup("Auto Aim")] public float lockOnRadius;
    [FoldoutGroup("Auto Aim")] public float lockOnViewAngle;
    [FoldoutGroup("Auto Aim")] public LayerMask lockOnMask;
    [FoldoutGroup("Auto Aim")] public LayerMask obstacleMask;

    private Vector3 forwardVector;
    private Vector3 rightVector;
    [HideInInspector] public Vector3 relativeDirection;

    Ray ray;
    RaycastHit hit;

    [FoldoutGroup("Interact")]
    [Header("Interact")]
    public InteractScript interact;
    [FoldoutGroup("Interact")] public LayerMask interactableMask;
    [FoldoutGroup("Interact")] public GameObject interactUI;
    [FoldoutGroup("Interact")] public GameObject failInteractFX;
    public Status enemyStatus;
    public GameObject skillQuickslots;

    private void Awake()
    {
    }

    private void Start()
    {

        mov = GetComponent<Movement>();

        status.deathEvent += PlayerDeath;
        DataManager.Instance.saveDataEvent += SaveCharacter;
        DataManager.Instance.loadDataEvent += LoadCharacter;

        //input.controlScheme = ControlScheme.PS4;

        input.R1input += SprintStart;
        input.R1release += SprintStop;
        input.R2input += OpenQuickslot;
        input.R2release += CloseQuickslot;
        input.R3input += ToggleLockOn;
        input.R3Right += TapRight;
        input.R3Left += TapLeft;
    }

    public void UpdateCorrectHP()
    {
        //status.rawStats.currentHealth = status.character.stats.currentHealth;
    }

    void OpenQuickslot()
    {
        skillQuickslots.SetActive(true);
    }

    void CloseQuickslot()
    {
        skillQuickslots.SetActive(false);
    }

    void BreakLockOn()
    {

    }

    void ToggleLockOn()
    {
        Transform temp = FindVisibleTargets();
        if (!lockOn && temp != null)
        {
            CameraManager.Instance.SetLockOnTarget(temp);
            enemyStatus = temp.GetComponentInParent<Status>();
            enemyStatus.deathEvent += ResetLockOn;
            mov.SetStrafeTarget(temp);
            lockOn = true;
        }
        else
        {
            CameraManager.Instance.DisableLockOn();
            mov.ResetStrafe();
            lockOn = false;
        }
    }

    void ResetLockOn()
    {

        Transform temp = FindVisibleTargets();
        if (temp != null)
        {
            CameraManager.Instance.SetLockOnTarget(temp);
            print(temp);
            mov.strafeTarget = temp;
            mov.strafe = true;
            lockOn = true;

        }
        else
        {
            print("Reset");
            CameraManager.Instance.DisableLockOn();
            mov.strafeTarget = temp;
            mov.strafe = false;
            lockOn = false;
        }
    }

    Vector3 RelativeToCamera(Vector2 v)
    {

        //Calculate forward and right
        forwardVector = Vector3.Cross(-Vector3.up, cam.transform.right).normalized;
        rightVector = cam.transform.right;
        Vector3 temp = ((rightVector * v.x) + (forwardVector * v.y));

        return temp;
    }

    void Update()
    {
        if (GameManager.gameOver) return;
        if (GameManager.isPaused) return;

        relativeDirection = RelativeToCamera(input.inputDirection);

    }

    void TapRight()
    {
        if (lockOn)
        {
            print("tap");
            Transform temp = FindVisibleTargets(true);
            if (temp == null) return;
            CameraManager.Instance.SetLockOnTarget(temp);
            mov.strafeTarget = temp;

        }
    }
    void TapLeft()
    {
        if (lockOn)
        {
            print("tapL");
            Transform temp = FindVisibleTargets(false);
            if (temp == null) return;
            CameraManager.Instance.SetLockOnTarget(temp);
            mov.strafeTarget = temp;
        }
    }

    Transform FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, lockOnRadius, lockOnMask);
        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            if (targetsInRadius[i].CompareTag("Enemy"))
            {
                Transform tempTarget = targetsInRadius[i].transform;
                Vector3 dirToTarget = (tempTarget.position - transform.position).normalized;
                if (Vector3.Angle(cam.transform.forward, dirToTarget) < lockOnViewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, tempTarget.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                    {
                        //  if (hit.transform == tempTarget)
                        visibleTargets.Add(tempTarget);
                    }
                }
            }
        }
        if (visibleTargets.Count > 0) return ClosestTargetToCenter();
        else return null;
    }

    public Transform FindVisibleTargets(bool right)
    {
        visibleTargets.Clear();

        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, lockOnRadius, lockOnMask);
        for (int i = 0; i < targetsInRadius.Length; i++)

        {

            if (targetsInRadius[i].CompareTag("Enemy"))
            {
                Transform tempTarget = targetsInRadius[i].transform;
                Vector3 dirToTarget = (tempTarget.position - transform.position).normalized;
                if (Vector3.Angle(cam.transform.forward, dirToTarget) < lockOnViewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, tempTarget.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                    {
                        if (tempTarget != mov.strafeTarget)
                        {
                            visibleTargets.Add(tempTarget);
                        }

                    }
                }
            }
        }
        if (visibleTargets.Count > 0) return ClosestTargetToCenter(right);
        else return null;
    }

    Transform ClosestTargetToCenter(bool right)
    {
        float shortestDistance = Screen.width;
        int closestTarget = 0;
        bool solution = false;
        for (int i = 0; i < visibleTargets.Count; i++)
        {
            Vector2 oldPos = cam.WorldToScreenPoint(mov.strafeTarget.position);
            Vector2 screenPos = cam.WorldToScreenPoint(visibleTargets[i].position);
            if (screenPos.x < Screen.width / 2 && !right)
            {
                float tempDist = Vector2.Distance(oldPos, screenPos);
                if (tempDist < shortestDistance)
                {
                    solution = true;
                    shortestDistance = tempDist;
                    closestTarget = i;
                }
            }
            else if (screenPos.x > Screen.width / 2 && right)
            {
                float tempDist = Vector2.Distance(oldPos, screenPos);
                if (tempDist < shortestDistance)
                {
                    solution = true;
                    shortestDistance = tempDist;
                    closestTarget = i;
                }
            }
        }
        if (solution)
            return visibleTargets[closestTarget];
        else return null;
    }

    Transform ClosestTargetToCenter()
    {
        float shortestDistance = lockOnViewAngle;
        int closestTarget = 0;
        for (int i = 0; i < visibleTargets.Count; i++)
        {
            Vector2 screenPos = cam.WorldToScreenPoint(visibleTargets[i].position);
            //if (screenPos.x < Screen.width / 2) { }
            //if (screenPos.x > Screen.width / 2) { }
            float tempDist = Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), screenPos);
            if (tempDist < shortestDistance)
            {
                shortestDistance = tempDist;
                closestTarget = i;
            }
        }
        return visibleTargets[closestTarget];
        //(visibleTargets[closestTarget].position - transform.position).normalized;
    }

    void PlayerDeath()
    {
        if (GameManager.gameOver) return;
        GameManager.gameOver = true;
    }

    private void FixedUpdate()
    {
        if (GameManager.gameOver) return;
        if (GameManager.menuOpen || GameManager.isPaused)
        {
            mov.direction = Vector3.zero;
            return;

        }


        mov.direction = relativeDirection;

        if (lockOn)
        {

            //mov.strafeTarget.GetComponent<Status>

            if (mov.strafeTarget == null || (mov.strafeTarget.position - transform.position).magnitude > lockOnRadius)
                ResetLockOn();
            else
            {
                lockOnIndicator.position = mov.strafeTarget.position + indicatorOffset;
                lockOnIndicator.gameObject.SetActive(true);
            }

        }
        else lockOnIndicator.gameObject.SetActive(false);

        switch (status.currentState)
        {
            case Status.State.Neutral:
                NeutralInput();
                break;
            case Status.State.InAnimation:
                InAnimationInput();
                break;
            case Status.State.Hitstun:
                break;
            case Status.State.Blockstun:
                break;
            default: break;
        }
    }


    void SprintStart()
    {
        mov.sprinting = true;
    }

    void SprintStop()
    {
        mov.sprinting = false;
    }

    void NeutralInput()
    {

        attack.block = input.L1Hold;
        status.blocking = input.L1Hold;
        mov.forcedWalk = attack.block;

       // if (mov.ground)
            if (InputAvailable())
            {
                switch (input.inputQueue[0])
                {

                    case 1:
                        if (inventory.activeItem.itemSO != null)
                        {
                            if (inventory.activeItem.itemSO.itemUsage == ItemSO.ItemUsage.Unusable)
                            {
                                Instantiate(failInteractFX, transform.position, Quaternion.identity);
                                return;
                            }
                            if (inventory.activeItem.itemSO.itemUsage == ItemSO.ItemUsage.Consume)
                            {
                                status.GoToState(Status.State.TopAnimation);
                                animator.EatAnimation();
                            }
                            else if (inventory.activeItem.itemSO.itemUsage == ItemSO.ItemUsage.Place)
                            {
                                if (interact.blocked) Instantiate(failInteractFX, transform.position, Quaternion.identity);
                                else
                                {
                                    status.GoToState(Status.State.InAnimation);
                                    animator.PlantAnimation();
                                }

                            }
                            else if (inventory.activeItem.itemSO.itemUsage == ItemSO.ItemUsage.Plant)
                            {
                                if (TerrainScript.Instance != null)
                                    if (!TerrainScript.Instance.CheckTexture(interact.groundPos))
                                    {
                                        Instantiate(failInteractFX, transform.position, Quaternion.identity);
                                    }
                                    else
                                    {
                                        if (interact.blocked) Instantiate(failInteractFX, transform.position, Quaternion.identity);
                                        else
                                        {
                                            status.GoToState(Status.State.InAnimation);
                                            animator.PlantAnimation();
                                        }

                                    }
                            }
                        }
                        else
                        {

                            if (equip.wateringCan && tool.waterContent <= 0 && !interact.water)
                            {
                                attack.ExtraAttack();
                            }
                            else if (equip.wateringCan && interact.water)
                            {
                                animator.RefillCan();
                            }
                            else
                                attack.LightAttack();
                        }
                        Delete();
                        break;
                    case 2:
                        if (inventory.activeItem.itemSO != null)
                        {
                            status.GoToState(Status.State.TopAnimation);
                            animator.UnequipAnimation();
                        }
                        else
                        {
                            if (equip.wateringCan && tool.waterContent <= 0 && !interact.water)
                            {
                                attack.ExtraAttack();
                            }
                            else if (equip.wateringCan && interact.water)
                            {
                                animator.RefillCan();
                            }
                            else
                                attack.HeavyAttack();
                        }
                        Delete();
                        break;

                    case 3:
                        if (GameManager.inventoryMenuOpen) { Delete(); return; }
                        if (mov.ground)
                        {
                            if (interact.foundItem)
                                interact.PickItem();
                            else if (interact.foundPlant)
                                interact.PickPlant();
                            else if (interact.foundInteractable)
                            {
                                interact.lastInteractable.Interact();
                            }
                            else { }
                            //InventoryScript.Instance.UseActiveItem();

                            Delete();
                        }
                        break;
                    case 4:
                        if (mov.ground)
                        {
                            if (!GameManager.isPaused)
                                mov.Jump();
                            Delete();

                        }
                        break;

                    case 5:
                        if (mov.ground)
                        {
                            dodge.Dash();

                            Delete();
                        }
                        break;

                    case 6:
                        if (mov.ground)
                        {
                            attack.ParryStart();
                            Delete();
                        }
                        break;
                    case 7:
                        if (mov.ground)
                        {
                            attack.Combo(0);
                            Delete();
                        }
                        break;
                    case 8:
                        if (mov.ground)
                        {
                            attack.Combo(1);
                            Delete();
                        }
                        break;
                    case 9:
                        if (mov.ground)
                        {
                            attack.Combo(2);
                            Delete();
                        }
                        break;
                    case 10:
                        if (mov.ground)
                        {
                            attack.Combo(3);
                            Delete();
                        }
                        break;
                    default: break;
                }
            }
    }

    void InAnimationInput()
    {
        //mov.sprinting = false;
        attack.block = false;
        status.blocking = false;

        if (InputAvailable())
        {
            if (attack.attackString) { NeutralInput(); }
            else if (dodge.recovering)
            {
                switch (input.inputQueue[0])
                {
                    case 1:

                        attack.LightAttack();
                        Delete();
                        break;
                    case 0:

                        dodge.Dash();
                        Delete();
                        break;
                }
            }
        }
    }

    public void SaveCharacter()
    {
        DataManager.Instance.currentSaveData.profile.stats = status.baseStats;
        DataManager.Instance.currentSaveData.profile.currentStats = status.rawStats;

        //status.character.stats.currentHealth = status.rawStats.currentHealth;
    }

    public void LoadCharacter()
    {
        status.ReplaceStats(status.baseStats, DataManager.Instance.currentSaveData.profile.stats);
        status.ReplaceStats(status.rawStats, DataManager.Instance.currentSaveData.profile.currentStats);
    }

    bool InputAvailable()
    {
        return input.inputQueue.Count > 0;
    }

    public void Delete()
    {
        input.inputQueue.RemoveAt(0);
    }

    public Vector3 AngleToVector(float angleInDegrees)
    {
        if (relativeDirection.sqrMagnitude > 0.01F)
            angleInDegrees += (Quaternion.LookRotation(relativeDirection, Vector3.up).eulerAngles).y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
