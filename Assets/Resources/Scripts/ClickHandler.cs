﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ClickHandler : MonoBehaviour {
	public static string Scene_To_Load = "";
	// Handle Click events

	void OnMouseDown(){
		switch (gameObject.tag) {
		case "PlayButton":
			GPS.AwardAchievement ("FirstGame");
			Scene_To_Load = "MainGame";
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Play_Down");
			break;
		case "CharactersButton":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Characters_Down");
			break;
		case "RetryButton":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Retry_Down");
			break;
		case "HomeButton":
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Ui/Home_Down");
			break;
		case "SettingsButton":
			Scene_To_Load = "Settings";
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Ui/Settings_Down");
			break;
		case "AchievementsButton":
			Scene_To_Load = "StartMenu";
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Ui/Achievements_Down");
			break;
		case "LeaderboardsButton":
			Scene_To_Load = "StartMenu";
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Ui/Leaderboard_Down");
			break;
		case "ChooseCharacterButton":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Characters_Choose_Down");
			break;
		case "RightScroll":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Scroll_Down");
			break;
		case "LeftScroll":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Scroll_Down");
			break;
		case "ReviveButton":
			NotificationManager.chosen = NotificationManager.optionChosen.revive;
			break;
		case "NopeButton":
			NotificationManager.chosen = NotificationManager.optionChosen.norevive;
			break;
		}

	}
	void OnMouseUp(){
		if (SceneManager.GetActiveScene ().name == "StartMenu") {
			Animator transition = Camera.main.GetComponent<Animator> ();
			transition.Play ("StartMenu_Camera_RotateAway");
		} else if (SceneManager.GetActiveScene ().name == "Characters" && gameObject.tag == "PlayButton") {
			Change_Scene ();
		}

		switch (gameObject.tag) {
		case "RetryButton":
			MainGameManager.Start_New ();
			break;
		case "HomeButton":
			Scene_To_Load = "StartMenu";
			Change_Scene ();
			break;
		
		case "CharactersButton":
			Scene_To_Load = "Characters";
			Change_Scene ();
			break;
		case "AchievementsButton":
			if (Social.localUser.authenticated == true) {
				PlayGamesPlatform.Instance.ShowAchievementsUI ();
			} else {
				Social.localUser.Authenticate ((bool success) => {
					if (success) {
						Debug.Log ("Login Succesful");
						MainGameManager.GPGS_Logged_In = true;
					} else {
						Debug.Log ("Login Failed");
					}
				});
			}
			break;
		case "LeaderboardsButton":
			if (Social.localUser.authenticated == true) {
				Social.ShowLeaderboardUI ();
			} else {
				Social.localUser.Authenticate ((bool success) => {
					if (success) {
						Debug.Log ("Login Succesful");
						MainGameManager.GPGS_Logged_In = true;
					} else {
						Debug.Log ("Login Failed");
					}

				});
			}
			break;
		case "RightScroll":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Scroll_Up");
			CharacterMenuManager.RightScroll ();
			break;
		case "LeftScroll":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Scroll_Up");
			CharacterMenuManager.LeftScroll ();
			break;
		case "ChooseCharacterButton":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Ui/Characters_Chosen");
			SaveManager.SetCharacter (CharacterMenuManager.currentCharacter);
			CharacterMenuManager.chosenCharacter = CharacterMenuManager.currentCharacter;
			break;
		}
	}


	public void Change_Scene(){
		SceneManager.LoadScene (Scene_To_Load);
	}


}





