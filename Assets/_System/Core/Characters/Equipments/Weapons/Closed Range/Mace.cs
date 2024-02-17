using MyHeroWay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

public class Mace : Weapon
{
    public LayerMask layerContact;
    public bool canDoCombo;
    [SerializeField]
    private int currentIndexMoveSet;
    public Vector2 sizeCollider;
    public Vector2 offsetCollider;

    public override void Initialize(Controller controller)
    {
        base.Initialize(controller);
    }

    public override void SetEquipmentData(EquipmentData data)
    {
        base.SetEquipmentData(data);
    }
    public override void SetUpPassive(CharacterStats originalStats)
    {

    }
    protected override void OnEvent(string obj)
    {
        if (obj.Equals("PlayFX"))
        {

        }
        if (obj.Equals(StrManager.TriggerDamageEvent))
        {
            var colls = new Collider2D[5];
            var hitDirection = controller.core.movement.facingDirection;
            Vector3 pos = handleFacingPosition(hitDirection);
            Physics2D.OverlapBoxNonAlloc(pos, sizeCollider, 0, colls, layerContact);
            DamageInfo damageInfo = new DamageInfo(controller.core);
            damageInfo.SetupCombo(weaponMoveSets.moveSets[currentIndexMoveSet]);
            if (controller is PlayerController)
            {
                var characterEquipment = PlayerControlManager.Instance.playerController.characterEquipment;
                var primary = characterEquipment.primaryWeapon;
                var secondary = characterEquipment.secondaryWeapon;
                if (secondary != null)
                {
                    damageInfo.SetupWeaponData(primary.weaponStats, secondary.weaponStats);
                }
                else
                {
                    damageInfo.SetupWeaponData(primary.weaponStats);

                }
            }
            else
            {
                damageInfo.SetupWeaponData(this.weaponStats);
            }

            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i] == null) continue;
                IDamage target = colls[i].GetComponent<IDamage>();
                if (target != null)
                {
                    target.TakeDamage(damageInfo);
                }
            }
            canDoCombo = false;
        }
        if (obj.Equals(StrManager.ActiveComboEvent))
        {
            canDoCombo = true;
        }
        if (obj.Equals(StrManager.DeactiveComboEvent))
        {
            canDoCombo = false;
            isActiveCombo = false;
        }
        if (obj.Equals("EmptyState"))
        {
            isActiveCombo = false;
        }


    }
    public override void OnEquip()
    {
        base.OnEquip();
    }

    public override void OnUnEquip()
    {
        base.OnUnEquip();

    }
    public override void OnUpdate()
    {
        /*if (controller.isStunning)
        {
            isActiveCombo = false;
        }*/
    }
    private bool PredictHit()
    {
        var colls = new Collider2D[5];
        var hitDirection = controller.GetMovement().facingDirection;
        Vector3 pos = handleFacingPosition(hitDirection);
        Physics2D.OverlapBoxNonAlloc(pos, sizeCollider, 0, colls, layerContact);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i] == null) continue;
            IDamage target = colls[i].GetComponent<IDamage>();
            if (target != null && target.controller != controller)
            {
                return true;
            }
        }
        return false;
    }
    public override void TriggerWeapon()
    {
        if (canDoCombo)
        {
            currentIndexMoveSet++;
            if (currentIndexMoveSet >= weaponMoveSets.moveSets.Length)
            {

                currentIndexMoveSet = 0;
            }
            isActiveCombo = PredictHit();
            var currentMoveSet = weaponMoveSets.moveSets[currentIndexMoveSet];
            controller.IsInteracting = true;
            controller.animatorHandle.SetBool(StrManager.isInteracting, controller.IsInteracting);
            controller.animatorHandle.SetBool(currentMoveSet.animationName, true);
            canDoCombo = false;
        }
        else
        {
            if (!controller.IsInteracting && !canDoCombo)
            {
                isActiveCombo = PredictHit();
                currentIndexMoveSet = 0;
                var currentMoveSet = weaponMoveSets.moveSets[currentIndexMoveSet];
                controller.IsInteracting = true;
                controller.animatorHandle.SetBool(StrManager.isInteracting, controller.IsInteracting);
                controller.animatorHandle.SetBool(currentMoveSet.animationName, true);
            }
        }
    }

    public Vector3 handleFacingPosition(EFacingDirection facingDirection)
    {
        Vector3 pos = Vector3.zero;
        switch (controller.GetMovement().facingDirection)
        {
            case EFacingDirection.DOWN:
                pos = controller.transform.position + new Vector3(offsetCollider.x, offsetCollider.y * -1);
                break;
            case EFacingDirection.LEFT:
                pos = controller.transform.position + new Vector3(offsetCollider.y * -1, offsetCollider.x);
                break;
            case EFacingDirection.RIGHT:
                pos = controller.transform.position + new Vector3(offsetCollider.y * 1, offsetCollider.x);
                break;
            case EFacingDirection.UP:
                pos = controller.transform.position + new Vector3(offsetCollider.x, offsetCollider.y * 1);
                break;
        }
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (controller == null)
        {
            Vector3 pos = transform.position + (Vector3)offsetCollider;
            pos.z = 0;
            Gizmos.DrawWireCube(pos, sizeCollider);
        }
        else
        {
            Vector3 pos = handleFacingPosition(controller.GetMovement().facingDirection);
            Gizmos.DrawWireCube(pos, sizeCollider);
        }
    }
}
