//roots grow in the direction the player is facing that damages enemies and heals if hit
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
public class Leach : MonoBehaviour, Spell
{
    playerScript player;
    Coroutine activeCoroutine = null;
    public GameObject hitbox;
    public int baseDamage = 1;

    int manaCost = 3;
    float baseCastTime = .5f;
    int heal = 1;
    void Awake()
    {
        player = GetComponentInParent<playerScript>();
    }
    void Spell.Cancel()
    {
        player.queuedSpell = null;
        if (activeCoroutine != null)
        {
            hitbox = null;
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }
    void Spell.Cast()
    {
        player.queuedSpell = null;
        if (activeCoroutine == null)
        {
            activeCoroutine = StartCoroutine(Casting());
        }
    }

    IEnumerator Casting()
    {

        if (manaCost > player.mana) { activeCoroutine = null; yield break; }
        player.mana -= manaCost;
        float startTime = Time.time;

        //TODO: play animation
        //if (enemy in hitbox collider)
        {
            //TODO: damage all enemy touching
            if (player.health < player.maxHealth)
            {
                player.health += 1;
            }
        }
        while (Time.time < startTime + baseCastTime * player.durationMultiplier)
        {

            yield return null;
        }
        //TODO: set animation back
    }
}
