using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour
{
    [SerializeField]
    public static int Players = 1;
    [SerializeField]
    public Behaviour[] DisableComponents;
    [SyncVar]
    [SerializeField]
    public int m_PlayerNumber = 0;                  // This specifies which player this the manager for.
    [SyncVar]
    [SerializeField]
    public string m_ColoredPlayerText = "Player 1";  // A string that represents the player with their number colored to match their tank.
    [SyncVar]
    [SerializeField]
    public int m_Wins = 0;                          // The number of wins this player has so far.

    [Command]
    public void Cmd_getCurrentPlayer()
    {
        NetworkSetup.Players++;

        m_PlayerNumber = NetworkSetup.Players;
    }

    // Use this for initialization
    void Start () {
	    if(!isLocalPlayer)
        {
            Cmd_getCurrentPlayer();
            m_ColoredPlayerText = "Player " + m_PlayerNumber;

            for (int i = 0; i < DisableComponents.Length; i++)
            {
                DisableComponents[i].enabled = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
