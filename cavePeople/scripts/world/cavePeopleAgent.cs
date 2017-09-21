using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavePeople : Agent
{
    [Header("eagleEggs/cavePeople")]
    public cavePeopleAcademy academy;

    GameObject trueAgent;

    public override void InitializeAgent()
    {
        trueAgent = gameObject;

        public bool isRaining;
        public bool hasStick;
        public bool isOutside;

    }

    public override List<float> CollectState()
    {
        
        GameObject currentClosestGoal = academy.actorObjs[0];
        GameObject agent = academy.actorObjs[0];
        List<float> state = new List<float>();

        foreach (GameObject actor in academy.actorObjs)
        {
            if (actor.tag == "agent")
            {
                agent = actor;
                state.Add(actor.transform.position.x / 1);
                state.Add(actor.transform.position.z / 1);
                continue;
            }
        }
        
        state.Add(currentClosestGoal.transform.position.x / 1);
        state.Add(currentClosestGoal.transform.position.z / 1);
        state.Add(currentClosestPit.transform.position.x / 1);
        state.Add(currentClosestPit.transform.position.z / 1);

        return state;
    }

    // to be implemented by the developer
    public override void AgentStep(float[] act)
    {

        //reward = -0.01f;
        int action = Mathf.FloorToInt(act[0]);

        // 0 - Forward, 1 - Backward, 2 - Left, 3 - Right
        if (action == 3)
        {
            Collider[] checkWall = Physics.OverlapBox(new Vector3(trueAgent.transform.position.x + 1, 0, trueAgent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (checkWall.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                trueAgent.transform.translate = new Vector3(trueAgent.transform.position.x + 1, 0, trueAgent.transform.position.z);
            }
        }

        if (action == 2)
        {
            Collider[] checkWall = Physics.OverlapBox(new Vector3(trueAgent.transform.position.x - 1, 0, trueAgent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (checkWall.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                trueAgent.transform.translate = new Vector3(trueAgent.transform.position.x - 1, 0, trueAgent.transform.position.z);
            }
        }

        if (action == 0)
        {
            Collider[] checkWall = Physics.OverlapBox(new Vector3(trueAgent.transform.position.x, 0, trueAgent.transform.position.z + 1), new Vector3(0.3f, 0.3f, 0.3f));
            if (checkWall.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                trueAgent.transform.translate = new Vector3(trueAgent.transform.position.x, 0, trueAgent.transform.position.z + 1);
            }
        }

        if (action == 1)
        {
            Collider[] checkWall = Physics.OverlapBox(new Vector3(trueAgent.transform.position.x, 0, trueAgent.transform.position.z - 1), new Vector3(0.3f, 0.3f, 0.3f));
            if (checkWall.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                trueAgent.transform.translate = new Vector3(trueAgent.transform.position.x, 0, trueAgent.transform.position.z - 1);
            }
        }

        Collider[] hitObjects = Physics.OverlapBox(trueAgent.transform.position, new Vector3(0.3f, 0.3f, 0.3f));
        if (hitObjects.Where(col => col.gameObject.tag == "cave").ToArray().Length == 1)
        {
            if(isRaining){
            reward = 1f;} else {reward = -0.1f;}
            done = true;
        }
        if (hitObjects.Where(col => col.gameObject.tag == "stick").ToArray().Length == 1)
        {
            if(isRaining==false){
            reward = 1f;} else {reward = +0.1f;}
            done = true;
        }
        if (hitObjects.Where(col => col.gameObject.tag == "fire").ToArray().Length == 1)
        {
            if(hasStick){
            reward = 1f;} else {reward = -1f;}
            done = true;
        }
        if (isOutside && isRaining)
        {
            reward = -1f;
            done = true;
        }

        //if (trainMode == "train") {
        if (true)
        {
            academy.visualAgent.transform.position = trueAgent.transform.position;
            academy.visualAgent.transform.rotation = trueAgent.transform.rotation;
        }
    }

    // to be implemented by the developer
    public override void AgentReset()
    {
        academy.AcademyReset();
    }
}
