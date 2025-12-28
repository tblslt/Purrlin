using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

//prevents incomming damage and staggers enemies that hit. if a successful block, refund mana
public class Counter : MonoBehaviour, Spell
{
    playerScript player;
    bool cancelled = false;
    Coroutine activeCoroutine = null;
    public int manaCost = 6;
    public float CastDuration = 1;
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

        player.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        //TODO: play counter animation
        while (Time.time < startTime + CastDuration * player.durationMultiplier)
        {
            yield return null;
        }
        player.canMove = true;
        player.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        //TODO: set animation back
    }
}