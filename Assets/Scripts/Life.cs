// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Life : MonoBehaviour
// {
//     [SerializeField] private int maxHealth;
//     [SerializeField] private int health;
//     private bool alive;

//     public int Health { get => health; }
//     public int MaxHealth { get => maxHealth; set => maxHealth = value; }

//     public bool Alive { get => alive; }
//     public bool Dead { get => !alive; }

//     /// <summary>
//     /// OverActors are components that want to do something right before this object is destroyed
//     /// </summary>
//     private List<IDie> overActors;

//     public void Awake()
//     {
//         health = MaxHealth;
//         alive = true;
//         GetOverActors();
//     }


//     public void FixedUpdate()
//     {
//         if (!alive)
//         {
//             // Keep trying to die
//             Die();
//         }
//     }
    

//     /// <summary>
//     /// Something hurt me, take damage, maybe even die
//     /// </summary>
//     /// <param name="damage"></param>
//     public void TakeDamage(int damage)
//     {
//         if (!alive)
//         {
//             return;
//         }
//         health -= damage;
//         if (health <= 0)
//         {
//             alive = false;
//             Die();
//         }
//     }


//     /// <summary>
//     /// Something is healing me
//     /// </summary>
//     /// <param name="healing">Amount of healing</param>
//     /// <param name="okayToExceedMax">When true, health can go above maxHealth</param>
//     public void Heal(int healing, bool okayToExceedMax = false)
//     {
//         if (!alive)
//         {
//             return;
//         }
//         if (okayToExceedMax)
//         {
//             health += healing;
//             return;
//         }
//         if (health >= MaxHealth)
//         {
//             return;
//         }
//         health = Mathf.Max(MaxHealth, health + healing);
//     }


//     /// <summary>
//     /// Go through all overActors and have them play their death scene
//     /// Ensure everyone is ready to Die, then destroy the object
//     /// </summary>
//     public void Die()
//     {
//         bool readyToDie = true;
//         foreach(IDie overActor in overActors)
//         {
//             if (!overActor.Dying())
//             {
//                 overActor.Die();
//             }
//             if (!overActor.ReadyToDie())
//             {
//                 readyToDie = false;
//             }
//         }
//         if (readyToDie)
//         {
//             Destroy(gameObject);
//         }
//     }


//     /// <summary>
//     /// Register an overActor to the list
//     /// </summary>
//     /// <param name="overActor">Component that wants to do something before being Destroyed</param>
//     public void IHaveFinalWords(IDie overActor)
//     {
//         overActors.Add(overActor);
//     }


//     /// <summary>
//     /// For whatever reason, an overACtor is now gone, so it nolonger needs to do anything before Destroy
//     /// </summary>
//     /// <param name="overActor">Component that lelft</param>
//     public void NeverMind(IDie overActor)
//     {
//         overActors.Remove(overActor);
//     }


//     /// <summary>
//     /// Initiate list of components that need to do something before Destroy
//     /// </summary>
//     private void GetOverActors()
//     {
//         IDie[] overActorArray = GetComponentsInChildren<IDie>();
//         overActors = new List<IDie>(overActorArray);
//     }
// }
