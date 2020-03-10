using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defencePower;
    public int manaPoints;
    public int maxMana;
    public List<Spell> spells;


    public void Hurt(int amount)
    {
        int damageAmount = amount;
        health = Mathf.Max(0, health - damageAmount);
        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int healAmount = Random.Range(0, 1) * (int)(amount + (maxHealth*.33f));
        health = Mathf.Min(maxHealth, health + healAmount);
        
    }

    public void Defend()
    {
        defencePower +=(int)(defencePower*.33f);
        Debug.Log("def increased.");
    }
    public bool CastSpell(Spell spell, Character targetCharacter)
    {
        bool succesful = manaPoints >= spell.manaCost;
        if (succesful)
        {
            Spell spellToCast = Instantiate<Spell>(spell, transform.position, Quaternion.identity);
            manaPoints -= spell.manaCost;
            spellToCast.Cast(targetCharacter);
        }
        return succesful;
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
        Debug.LogFormat("{0} has died!", characterName);
    }
}
