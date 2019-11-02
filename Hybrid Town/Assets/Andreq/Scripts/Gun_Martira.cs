using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Martira : Gun
{
    public float maxAngle = 60f;

    public Transform leftBlock;
    public Transform rightBlock;

    protected override void Shoot()
    {
        if (CreateBullet())
        {
            Products[typeBullet].Reduce();
            Vector2 direction = MovementPart.position - Slider.transform.position;
            var distance = Vector2.Distance(MovementPart.position, Slider.transform.position) / maxStretch;

            // Хуйня но я заебался уже
            if (!(MovementPart.localRotation.eulerAngles.z > 360 - maxAngle || MovementPart.localRotation.eulerAngles.z < maxAngle))
            {
                if ((MovementPart.localRotation.eulerAngles.z < 360 - maxAngle))
                {
                    direction = MovementPart.position - rightBlock.position;
                }
                else
                {
                    direction = MovementPart.position - leftBlock.position;
                }
            }

            BulletComponent.Shoot(direction * Force * distance);

            catapultLine.SetPosition(1, catapultLine.GetPosition(0));
            Slider.transform.position = startSliderPosition;
            _clickedOn = false;
            BulletComponent = null;
            StartCoroutine(RotateToStarWithDelay(0f));
        }

    }

    protected override void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - MovementPart.position;
        if (catapultToMouse.sqrMagnitude > _maxStretchSqr)
        {

            _rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = _rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0f;
        Slider.transform.position = mouseWorldPoint;
        var angle = MovementPart.rotation.eulerAngles.z;

        MovementPart.LookAt(Slider.transform, Vector3.forward);

        // Хуйня но я заебался уже
        if (!(MovementPart.localRotation.eulerAngles.z > 360 - maxAngle || MovementPart.localRotation.eulerAngles.z < maxAngle))
        {
                MovementPart.localRotation = Quaternion.Euler(new Vector3(0, 0, 360-maxAngle));

        }
    }
}