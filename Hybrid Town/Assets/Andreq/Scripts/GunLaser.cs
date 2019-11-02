using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaser : Gun
{
    protected override void Shoot()
    {
        if (CreateBullet())
        {
            Products[typeBullet].Reduce();

            Vector2 direction = MovementPart.position - Slider.transform.position;
            var distance = Vector2.Distance(MovementPart.position, Slider.transform.position) / maxStretch;

            var delay = 0.4f;
            ((Bullet_Laser)BulletComponent).delay = delay;
            ((Bullet_Laser)BulletComponent).Maximum = 50f;

            BulletComponent.Shoot(MovementPart.transform.rotation.eulerAngles);

            catapultLine.SetPosition(1, catapultLine.GetPosition(0));
            Slider.transform.position = startSliderPosition;
            _clickedOn = false;
            BulletComponent = null;
            StartCoroutine(RotateToStarWithDelay(delay));
        }
    }

    protected override bool CreateBullet()
    {
        var result = base.CreateBullet();

        return result;
    }


}
