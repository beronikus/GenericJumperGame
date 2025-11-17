using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
   
   public static GameManager Instance { get; private set; }

   [SerializeField] private GameObject successUI;
   [SerializeField] private GameObject deathUI;
   
   private void Awake()
   {
      Instance = this;
   }

  

   private void Start()
   {
      Player.Instance.OnReachingGoal += Player_OnReachingGoal;
      Player.Instance.OnDeath += Player_OnDeath;
      successUI.SetActive(false);
      deathUI.SetActive(false);
   }

   private void Player_OnDeath(object sender, EventArgs e)
   {
     deathUI.SetActive(true);
   }

   private void Player_OnReachingGoal(object sender, EventArgs e)
   {
      successUI.SetActive(true);
      Player.Instance.enabled = false;
   }

   public void RestartGame()
   {
      SceneManager.LoadScene(0);
   }
}
