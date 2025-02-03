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


    public override List<Led> getLeds()
    {

        List<Led> result = new List<Led>();

        for (int i = 0; i < amount; i++){
            float n = i / (amount - 1);
            Vector3 position = Vector3.Lerp(from.position, to.position, n);
            Led led = new Led(position);
            result.Add(led);
        }

        return result;
    }
}