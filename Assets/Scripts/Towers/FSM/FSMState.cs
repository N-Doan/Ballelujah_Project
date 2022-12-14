using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class is adapted and modified from the FSM implementation class available on UnifyCommunity website
// The license for the code is Creative Commons Attribution Share Alike.
// It's originally the port of C++ FSM implementation mentioned in Chapter01 of Game Programming Gems 1
// You're free to use, modify and distribute the code in any projects including commercial ones.
// Please read the link to know more about CCA license @http://creativecommons.org/licenses/by-sa/3.0/

// This class represents the States in the Finite State System.
// Each state has a Dictionary with pairs (transition-state) showing
// which state the FSM should be if a transition is fired while this state
// is the current state.
// Reason method is used to determine which transition should be fired .
// Act method has the code to perform the actions the NPC is supposed to do if it큦 on this state.

public abstract class FSMState
{
    protected Dictionary<transition, fSMStateID> map = new Dictionary<transition, fSMStateID>();
    protected fSMStateID stateID;
    public fSMStateID ID { get { return stateID; } }
    protected Vector3 destPos;
    protected Transform[] waypoints;
    protected float curRotSpeed;
    protected float curSpeed;

    public void addTransition(transition transition, fSMStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == transition.None || id == fSMStateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Since this is a Deterministc FSM,
        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        //Debug.Log("Added : " + transition + " with ID : " + id);
    }

    // This method deletes a pair transition-state from this state큦 map.
    // If the transition was not inside the state큦 map, an ERROR message is printed.
    public void deleteTransition(transition trans)
    {
        // Check for NullTransition
        if (trans == transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this State큦 List");
    }


    // This method returns the new state the FSM should be if
    // this state receives a transition  
    public fSMStateID getOutputState(transition trans)
    {
        // Check for NullTransition
        if (trans == transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return fSMStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
        return fSMStateID.None;
    }


    // Used to initialize variables when re-entering state
    public virtual void enterStateInit(Transform npc)
    {
    }

    // Decides if the state should transition to another on its list
    // NPC is a reference to the npc tha is controlled by this class
    public abstract void reason( Transform npc);

    // This method controls the behavior of the NPC in the game World.
    // Every action, movement or communication the NPC does should be placed here
    // NPC is a reference to the npc tha is controlled by this class
    public abstract void act( Transform npc);

}
