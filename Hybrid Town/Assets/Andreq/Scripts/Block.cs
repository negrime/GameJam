using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Unit ParentUnit;
    public List<Block> LinkedUpBlock = new List<Block>();
    public List<Block> LinkedDownBlock = new List<Block>();

    public List<Unit> LinkedUpUnit = new List<Unit>();
    public List<Unit> LinkedDownUnit = new List<Unit>();



    public static bool Emit = false;

    private void Awake()
    {
         Calculate(draw: true);
    }


    public (List<Unit>, List<Unit>) CalculateLinkedUnit()
    {
        return Calculate();
        return (LinkedDownUnit, LinkedUpUnit);
    }
    public (List<Unit>, List<Unit>) Calculate(bool draw = false)
    {
        float offset = 0.2f;
        Vector3 startUp = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        Vector3 startDown = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);

        float distance = 0.2f;
        float duration = 1f;

        if (draw)
        {
            Debug.DrawRay(startUp, Vector2.up, Color.green, duration);
            #region
            /*
            Debug.DrawRay(startDown, Vector2.down, Color.green, duration);
            Debug.DrawRay(start, Vector2.left, Color.green, duration);
            Debug.DrawRay(start, Vector2.right, Color.green, duration);
            */
            #endregion
        }

        var hit_up = Physics2D.Raycast(startUp, Vector2.up, 10000, LayerMask.GetMask("Struct"));

        //var hit_up = Physics2D.Raycast(startUp, Vector2.up, distance);
        if (hit_up.collider != null)
        {
            var collider = hit_up.collider;
            if (collider.tag == "block" && collider.gameObject != gameObject)
            {
                var block = collider.GetComponent<Block>();
                LinkedUpBlock.Add(block);
                block.LinkedDownBlock.Add(this);

                // block.ParentUnit.GetLinkedLists();
            }
        }



        #region
        /*
        var hit_down = Physics2D.Raycast(startDown, Vector2.down, distance);
        if (hit_down.collider != null)
        {
            var collider = hit_down.collider;
            if (collider.tag == "block" && collider.gameObject != gameObject)
            {
                LinkedDownBlock.Add(collider.GetComponent<Block>());
            }
        }

        var hit_left = Physics2D.Raycast(start, Vector2.left, distance);
        var hit_right = Physics2D.Raycast(start, Vector2.right, distance);

        if (hit_left.collider != null)
        {
            var collider = hit_left.collider;
            if (collider.tag == "block")
            {
                LinkedBlock.Add(collider.GetComponent<Block>());
            }
        }
        if (hit_right.collider != null)
        {
            var collider = hit_right.collider;
            if (collider.tag == "block")
            {
                LinkedBlock.Add(collider.GetComponent<Block>());
            }
        }
        */
        #endregion

        List<Unit> LinkedDownUnit = LinkedDownBlock
                                    .Select(itm => itm.ParentUnit)
                                    .Distinct()
                                    .ToList();
        List<Unit> LinkedUpUnit = LinkedUpBlock
                            .Select(itm => itm.ParentUnit)
                            .Distinct()
                            .ToList();
        this.LinkedDownUnit = LinkedDownUnit;
        this.LinkedUpUnit = LinkedUpUnit;

        if (LinkedDownUnit == null || LinkedUpUnit == null)
            Debug.LogError("Пиздец");

        return (LinkedDownUnit, LinkedUpUnit);
    }
}
