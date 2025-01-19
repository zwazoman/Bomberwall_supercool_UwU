using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlaymodeTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestStateMachine()
    {
        // Arrange

        GameObject test = new GameObject();
        AI_StateMachine stateMachine = test.AddComponent<AI_StateMachine>();

        AI_BaseState state = stateMachine.CurrentState;

        // Act

        stateMachine.TransitionTo(stateMachine.ChaseState);

        // Assert

        Assert.That(stateMachine.CurrentState, Is.EqualTo(state));

    }

    [Test]
    public void TestHealth()
    {
        // Assert
        GameObject test = new GameObject();
        test.AddComponent<Damageable>();
        PlayerHealth health = test.AddComponent<PlayerHealth>();

        GameObject killer = new GameObject();

        health.Maxhealth = 3;
        health.CurrentHealth = health.Maxhealth;

        bool DamageTookEventInvoked = false;
        health.OnDamageTook += () => DamageTookEventInvoked = true;

        // Act

        health.TakeDamage(killer);

        // Assert

        Assert.That(health.CurrentHealth, Is.EqualTo(health.Maxhealth - 1));
        Assert.That(DamageTookEventInvoked, Is.True);

    }
}
