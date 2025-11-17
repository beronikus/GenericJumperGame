using System;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
   [SerializeField] private AudioClip jumpSoundEffect;
   [SerializeField] private ParticleSystem deathParticleSystem;
   [SerializeField] private AudioClip deathSoundEffect;
   [SerializeField] private AudioClip victoryAudioClip;

   private void Start()
   {
      Player.Instance.OnJump += Player_OnJump;
      Player.Instance.OnReachingGoal += Player_OnReachingGoal;
      Player.Instance.OnDeath += OnDeath;
   }

   private void OnDeath(object sender, EventArgs e)
   {
      Instantiate(deathParticleSystem, Player.Instance.transform.position, Quaternion.identity);
      AudioSource.PlayClipAtPoint(deathSoundEffect, Camera.main.transform.position);
   }

   private void Player_OnReachingGoal(object sender, EventArgs e)
   {
      AudioSource.PlayClipAtPoint(victoryAudioClip, Camera.main.transform.position);
   }

   private void Player_OnJump(object sender, EventArgs e)
   {
      AudioSource.PlayClipAtPoint(jumpSoundEffect, Camera.main.transform.position); 
   }
}
