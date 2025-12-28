//stop movement and heal after animation is played
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Heal : MonoBehaviour, Spell
{
    playerScript player;
    bool cancelled = false;
    Coroutine activeCoroutine = null;
    public int manaCost = 10;
    public float baseCastTime = 2;
    void Awake()
    {
        player = GetComponentInParent<playerScript>();
    }
    void Spell.Cancel()
    {
        player.queuedSpell = null;
        if (activeCoroutine != null)
        {
            //TODO: stop animation
            StopCoroutine(activeCoroutine);
            player.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            player.canMove = true;
            activeCoroutine = null;
        }
    }

    void Spell.Cast()
    {
        player.queuedSpell = null;
        player.canMove = false;
        activeCoroutine = StartCoroutine(Casting());
    }
    IEnumerator Casting()
    {

        if (manaCost > player.mana) { activeCoroutine = null; yield break; }
        player.mana -= manaCost;
        float startTime = Time.time;

        //TODO: play heal animation
        player.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        while (Time.time < startTime + baseCastTime * player.durationMultiplier)
        {
            yield return null;
        }
        player.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        player.canMove = true;

        //TODO: set animation back
    }

}