using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathUtil
{
    public class Util : MonoBehaviour
    {
        public static bool CanSeeObj(GameObject player,GameObject NPC,float range)
        {
            Vector3 dir = Vector3.Normalize(player.transform.position - NPC.transform.position);
            float angleDist = Vector3.Dot(NPC.transform.forward, dir);

           // Debug.Log(angleDist);

            Debug.DrawRay(NPC.transform.position, NPC.transform.forward * 10, Color.red);
            Debug.DrawRay(NPC.transform.position, dir * 10, Color.green);

            if (angleDist > range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector3 objSide(GameObject player, GameObject NPC)
        {
            Vector3 dir = Vector3.Normalize(player.transform.position - NPC.transform.position);
            Vector3 crossProd = Vector3.Cross(NPC.transform.forward, dir);

            Debug.DrawRay(NPC.transform.position,crossProd*10,Color.blue);
            if (crossProd.y > 0)
            {
                Debug.Log(player.name + "is right" + NPC.name);
            }
            else
            {
                Debug.Log(player.name + "is left" + NPC.name);
            }

            return crossProd;
        }
    }
}
