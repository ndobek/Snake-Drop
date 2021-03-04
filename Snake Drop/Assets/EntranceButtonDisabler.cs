using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceButtonDisabler : MonoBehaviour
{
    [SerializeField]
    private GameObject Button;
    private EntranceSlot slot;
    private PlayerManager player;

    private void Start()
    {
        slot = GetComponent<EntranceSlot>();
        player = GameManager.instance.playerManagers[0];
    }

    void Update()
    {
        if (Button) Button.SetActive(slot.Active && player.GameInProgress && !player.RoundInProgress);
    }

    public void MoveEntranceSlotHere()
    {
        player.PositionWaitSlot(slot);
    }
}
