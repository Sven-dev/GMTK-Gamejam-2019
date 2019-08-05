using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int ActiveAI;

    private Transform Object;
    private Mover Mover;
    private Shooter Shooter;

    private void Start()
    {
        Object = transform.GetChild(0);

        Mover = GetComponent<Mover>();
        Shooter = GetComponentInChildren<Shooter>();

        StartAI(ActiveAI);
    }

    private void StartAI(int Number)
    {
        StartCoroutine(_ExecuteAI());
    }

    //AI 1: moves and shoots
    IEnumerator _ExecuteAI()
    {
        Shooter = transform.GetComponentInChildren<Shooter>();
        yield return new WaitForSeconds(2f);

        int number = ActiveAI;
        while (ActiveAI == number)
        {
            //Pick a location
            Vector3 destination = Field.GetPoint();

            while (ActiveAI == number && Vector3.Distance(Object.position, destination) > 1f)
            {
                Vector2 direction = destination - Object.position;

                //Rotate towards location
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
                Object.rotation = Quaternion.Lerp(Object.rotation, q, Time.deltaTime * 2.5f);

                //Move to it
                Mover.Move(Vector2.down);
                yield return null;
            }

            //Pick a target
            Transform target = Field.Player;


            float progress = 0;
            while(ActiveAI == number && progress < 1)
            {
                //Rotate towards location
                Vector2 direction = target.position - Object.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
                Object.rotation = Quaternion.Lerp(Object.rotation, q, progress);

                progress += Time.deltaTime * 2;
                yield return null;
            }

            //wait while looking at target
            float waittime = 2f;
            while (ActiveAI == number && waittime > 0)
            {
                Vector3 targ = target.position;
                targ.z = 0f;

                Vector3 objectPos = Object.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                Object.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 89f));

                waittime -= Time.deltaTime;
                yield return null;
            }

            //Shoot
            if (ActiveAI == number)
            {
                Shooter.Shoot();
                yield return new WaitForSeconds(1f);
            }
        }

        //Start the next AI
        StartAI(ActiveAI);
    }


    //AI 2: gets close and dashes

    //AI 3: doesn't move and fires triple shot

    //AI 4: 

    //AI 5

    //AI 6

    //AI 7

    //AI 8

    //AI 9
}
