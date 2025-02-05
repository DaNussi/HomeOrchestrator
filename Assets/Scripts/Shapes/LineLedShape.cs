using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LineLedShape : LedShape
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] int amount;

    public override List<Vector3> GeneratePositions()
    {
        List<Vector3> result = new List<Vector3>();

        if (amount <= 0) amount = 1;

        float delta = 1f / (amount - 1);
        float index = 0;

        if (amount == 1)
        {
            delta = 0f;
            index = 0.5f;
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 position = Vector3.LerpUnclamped(from.position, to.position, index);
            result.Add(position);


            index += delta;
        }

        return result;
    }

}