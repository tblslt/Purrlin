using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

//needles made of blood erupt from your face that fan out like a shotgun, costs health instead of mana
public class Acupuncture : MonoBehaviour, Spell
{
    playerScript player;
    Coroutine activeCoroutine = null;
    public GameObject needles;
    public int baseDamage = 1;

    int healthCost = 3;
    float baseCastTime = .2f;
    void Awake()
    {
        player = GetComponentInParent<playerScript>();
    }
    void Spell.Cancel()
    {
        player.queuedSpell = null;
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            player.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            activeCoroutine = null;
        }
    }
    void Spell.Cast()
    {
        player.queuedSpell = null;
        if (activeCoroutine == null)
        {
            player.canMove = false;
            activeCoroutine = StartCoroutine(Casting());
        }
    }

    IEnumerator Casting()
    {

        if (healthCost >= player.health) { activeCoroutine = null; yield break; }
        player.health -= healthCost;

        //TODO: play animation
        //if (enemy in hitbox collider)
        {
            //TODO: damage all enemy touching
            if (player.health < player.maxHealth)
            {
                player.health += 1;
            }
        }
        float startTime = Time.time;
        player.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        while (Time.time < startTime + baseCastTime * player.durationMultiplier)
        {
            yield return null;
        }
        player.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        //TODO: set animation back
    }
}