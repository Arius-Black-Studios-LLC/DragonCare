using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragonFollowTheLeaderState : DragonNPCState
{
    public DragonNPCManager leader;
    public float maxDistance=20;
    public float TimerMax = 10;
    public float counter;

    public override void PerformState(DragonNPCManager manager)
    {
        if(leader == null) 
        {
            //find all of the dragonNPCManagers
            DragonNPCManager[] allDragons = FindObjectsOfType<DragonNPCManager>();
            foreach(var dragon in allDragons)
            {

                if (dragon.currentstate is DragonFollowTheLeaderState)
                {
                    DragonFollowTheLeaderState followLeaderState = (DragonFollowTheLeaderState)dragon.currentstate;
                    if (followLeaderState.leader != null)
                    {
                        leader = followLeaderState.leader;
                        break;
                    }
                }
            }
            if(leader == null)
            {
                leader = manager;
            }

            //if non of them have a leader, you are the leader
        
        }

        counter += Time.deltaTime;
        if (counter > TimerMax)
        {
            if (leader == manager)
            {

                manager.agent.SetDestination(transform.position + Random.insideUnitSphere * maxDistance);
            }
            else
            {
                manager.agent.SetDestination(leader.transform.position);
            }
            counter = 0;
        }



    }

    public override void ResetState(DragonNPCManager manager)
    {
        leader = null;
        counter = 0;
    }
}
