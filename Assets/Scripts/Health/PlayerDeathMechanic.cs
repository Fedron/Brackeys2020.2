using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathMechanic : MonoBehaviour
{
    private CreaturesHealth pHealth;
    private PlayerAnimationHandler playerAnimation;
    private RewinderManager rewinderManager;
    public GameObject deathVFXPref;
    public float TimeForRevive;

    private void Awake()
    {
        pHealth = GetComponent<CreaturesHealth>();
        rewinderManager = FindObjectOfType<RewinderManager>();
        playerAnimation = GetComponent<PlayerAnimationHandler>();

        // adds core mechanics to player death
        // todo remove comments once we have revive particles
        //pHealth.OnDie += DeathParticles;
        pHealth.OnDie += playerAnimation.DieAnimation;
        pHealth.OnDie += rewinderManager.DeathRewind;
        //adds revive animation into the rewindermanager
        rewinderManager.normalRevive += playerAnimation.ReviveAnimation;
        rewinderManager.normalRevive += StopGameAndWaitUntilRevive;
        rewinderManager.normalRevive += ResetHealth;
        rewinderManager.changeDimention += playerAnimation.ReviveAnimation;
    }

    //private void DeathParticles() => Instantiate(deathVFXPref, transform.position, Quaternion.identity);
    private void StopGameAndWaitUntilRevive() => StartCoroutine(StopEnemies());
    private void ResetHealth() => pHealth.CurrentHealth = pHealth.MaxHealth;
    
    private IEnumerator StopEnemies()
    {
        //todo make enemies be not aggressive while rewinding
        DungeonManager dungeon = FindObjectOfType<DungeonManager>();
        List<GameObject> enemies = dungeon.rooms[dungeon.activeRoom].GetComponent<RoomManager>().enemies;
        foreach (GameObject enemy in enemies) {
            try {
                enemy.GetComponent<AIMoverandPathfinding>().enabled = false;
            } catch {}
        }
        yield return new WaitForSeconds(TimeForRevive);
        foreach (GameObject enemy in enemies) {
            try {
                enemy.GetComponent<AIMoverandPathfinding>().enabled = true;
            } catch {}
        }
    }
}
