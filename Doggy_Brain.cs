using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog
{
    public string name;
    public int age;
    public float weight;
    public string color;

    public Dog()
    {
        name = "Ivana";
        age = 4;
        weight = 6;
        color = "purple";

    }

    public Dog(string iname)
    {
        name = iname;
        age = 4;
        weight = 6;
        color = "blue";
    }

    public float MonthlyPrice()
    {
        return (30f * weight / 50f * 0.5f);

    }

    public float MonthlyPrice(float priceoffood)
    {
        return (30f * weight / 50f * 0.5f * priceoffood);
    }
        
}

public class Behaviour
{
    public Biology animal_state;

    public void Update()
    {
        animal_state.Metabolize();

        if (animal_state.sleeping)
        {
            animal_state.Sleep();
        }
    }

}

public class Biology
{
    public int health;
    public int max_health;
    public int energy;
    public int max_energy;
    public int metabolism;
    public int stamina;
    public int max_stamina;
    public int trust;
    public int danger;
    public int age;
    public int max_age;
    public int desire;
    public bool gender;
    public int pregnant;
    public int max_pregnant;
    public int active_age;
    public bool sleeping_habit;
    public bool sleeping;

    public Biology(int input_max_health, int input_max_energy, int input_max_stamina, int input_max_age, int input_max_pregnant, int input_active_age, int input_desire, int input_metabolism, bool input_gender, bool input_sleeping_habit)
    {        
        max_health = input_max_health;
        max_energy = input_max_energy;
        max_stamina = input_max_stamina;
        max_age = input_max_age;
        max_pregnant = input_max_pregnant;
        active_age = input_active_age;
        desire = input_desire;
        metabolism = input_metabolism;
        health = max_health;
        energy = max_energy;
        stamina = max_stamina;
        age = 0;
        trust = 0;
        pregnant = -1;
        danger = 0;
        gender = input_gender;
        sleeping_habit = input_sleeping_habit;
        sleeping = false;        
    }
    public void Metabolize()
    {
        energy = energy - metabolism;
        stamina = stamina - metabolism;
    }

    public void Sleep()
    {
        energy = energy + (metabolism / 2);
        stamina = stamina + metabolism * 2;
        health = health + metabolism;
    }

    public void Eat(int calories)
    {
        energy = energy + calories;
    }
}

public class Doggy_Brain : MonoBehaviour
{
    public bool make_sleep;
    public bool damage;

    public Rigidbody rigidBody;

    public Animator animator;
    public Transform currentTarget, lookTarget;

    public float forceAmount;
    public float maxRandomAmount;


    public Vector3 heading;
    public float distanceToTarget;
    public float angleToTarget;

    public float randomAmount;
    public float closeEnoughToTarget;
    //public Dog puppy1;

    public Behaviour brain;

    public float CalculateDistanceToTarget()
    {
        heading = (currentTarget.position - transform.position);
        return (heading.magnitude);
    }


    // Start is called before the first frame update
    void Start()
    {
        brain = new Behaviour();
        brain.animal_state = new Biology(10000, 10000, 10000, 2, 1, 1, 50, 5, true, true);


        //puppy1 = new Dog();
        //Debug.Log("Price of " + puppy1.name + " is " + puppy1.MonthlyPrice());


}



    // Update is called once per frame
    void Update()
    {
        brain.Update();

        Debug.Log("The Health, Energy and Stamina is: " + brain.animal_state.health + " " + brain.animal_state.energy + " " + brain.animal_state.stamina + " ");
        distanceToTarget = CalculateDistanceToTarget();

        brain.animal_state.sleeping = make_sleep;

        if (damage)
        {
            brain.animal_state.health = brain.animal_state.health - 100;
            damage = false;
        }

        if (distanceToTarget > closeEnoughToTarget)
        {
            transform.LookAt(currentTarget);
            animator.SetFloat("animator_speed", 1.0f);
            rigidBody.AddForce(heading.normalized * forceAmount);
        }
        else 
        {
            transform.LookAt(lookTarget);
            animator.SetFloat("animator_speed", 0f);
        }
    }
}
