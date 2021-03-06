﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;
using VoxelBusters.NativePlugins;
using UnityEngine.SceneManagement;

public class PreLoad : MonoBehaviour {
	private int _disclaimer;
	private 	NotificationType	m_notificationType;
	// Use this for initialization
	public bool fixedNotificationTime = false;
	public int notificationTime;
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		CancelAllLocalNotifications();
		NPBinding.NotificationService.RegisterNotificationTypes(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
		NPBinding.NotificationService.ClearNotifications();
		
		//PlayerPrefs.DeleteAll ();
		//print(I2.Loc.LocalizationManager.CurrentLanguageCode);

		if(I2.Loc.LocalizationManager.CurrentLanguageCode == "" || I2.Loc.LocalizationManager.CurrentLanguageCode == null)
			I2.Loc.LocalizationManager.CurrentLanguageCode = "en";

		//VoxelBusters.NativePlugins.NotificationService not = new VoxelBusters.NativePlugins.NotificationService ();
		//VoxelBusters.NativePlugins.CrossPlatformNotification c = new VoxelBusters.NativePlugins.CrossPlatformNotification ();

		//not.ScheduleLocalNotification (c);
		//CancelAllLocalNotifications();
		

        //not.ScheduleLocalNotification(CreateNotification(60, eNotificationRepeatInterval.MINUTE));

        //CrossPlatformNotification _notification = CreateNotification(60 * 60 * 24, eNotificationRepeatInterval.DAY);
        //CrossPlatformNotification _notification = CreateNotification (60, eNotificationRepeatInterval.DAY);
		//NPBinding.NotificationService.ScheduleLocalNotification (_notification);

		PlayerPrefs.SetInt ("QuoteCheck", -1);
		_disclaimer = PlayerPrefs.GetInt ("Disclaimer", 1);
		if (_disclaimer == 1) {
			SceneManager.LoadScene ("Disclaimer_SL");
		} else {
			//SceneManager.LoadScene ("Quote_NJ");
			StartCoroutine(ControlHistory.cargarEscena());
		}
	}

	void RegisterNotificationTypes (NotificationType _notificationTypes)
	{
		NPBinding.NotificationService.RegisterNotificationTypes(_notificationTypes);
	}

	private VoxelBusters.NativePlugins.CrossPlatformNotification CreateNotification (long _fireAfterSec, VoxelBusters.NativePlugins.eNotificationRepeatInterval _repeatInterval)
	{
		// User info
		IDictionary _userInfo			= new Dictionary<string, string>();
		_userInfo["data"]				= "custom data";

		VoxelBusters.NativePlugins.CrossPlatformNotification.iOSSpecificProperties _iosProperties			= new VoxelBusters.NativePlugins.CrossPlatformNotification.iOSSpecificProperties();
		_iosProperties.HasAction		= true;
		_iosProperties.AlertAction		= "alert action";

		VoxelBusters.NativePlugins.CrossPlatformNotification.AndroidSpecificProperties _androidProperties	= new VoxelBusters.NativePlugins.CrossPlatformNotification.AndroidSpecificProperties();
		_androidProperties.ContentTitle	= "Daily Quote";
		_androidProperties.TickerText	= "Check your new daily quote for today";
		_androidProperties.LargeIcon	= "icon.png"; //Keep the files in Assets/PluginResources/Android or Common folder.

		VoxelBusters.NativePlugins.CrossPlatformNotification _notification	= new VoxelBusters.NativePlugins.CrossPlatformNotification();
		_notification.AlertBody			= "Check your new daily quote for today"; //On Android, this is considered as ContentText
		_notification.FireDate			= System.DateTime.Now.AddSeconds(_fireAfterSec);
		_notification.RepeatInterval	= _repeatInterval;
		_notification.SoundName			= "Notification.mp3"; //Keep the files in Assets/PluginResources/Android or iOS or Common folder.
		_notification.UserInfo			= _userInfo;
		_notification.iOSProperties		= _iosProperties;
		_notification.AndroidProperties	= _androidProperties;

		return _notification;
	}

	private VoxelBusters.NativePlugins.CrossPlatformNotification CreateNotification (System.DateTime date, VoxelBusters.NativePlugins.eNotificationRepeatInterval _repeatInterval)
	{
		// User info
		IDictionary _userInfo			= new Dictionary<string, string>();
		_userInfo["data"]				= "custom data";

		VoxelBusters.NativePlugins.CrossPlatformNotification.iOSSpecificProperties _iosProperties			= new VoxelBusters.NativePlugins.CrossPlatformNotification.iOSSpecificProperties();
		_iosProperties.HasAction		= true;
		_iosProperties.AlertAction		= "alert action";

		VoxelBusters.NativePlugins.CrossPlatformNotification.AndroidSpecificProperties _androidProperties	= new VoxelBusters.NativePlugins.CrossPlatformNotification.AndroidSpecificProperties();
		_androidProperties.ContentTitle	= "Daily Quote";
		_androidProperties.TickerText	= "Check your new daily quote for today";
		_androidProperties.LargeIcon	= "icon.png"; //Keep the files in Assets/PluginResources/Android or Common folder.

		VoxelBusters.NativePlugins.CrossPlatformNotification _notification	= new VoxelBusters.NativePlugins.CrossPlatformNotification();
		_notification.AlertBody			= "Check your new daily quote for today"; //On Android, this is considered as ContentText
		_notification.FireDate			= date;
		_notification.RepeatInterval	= _repeatInterval;
		_notification.SoundName			= "Notification.mp3"; //Keep the files in Assets/PluginResources/Android or iOS or Common folder.
		_notification.UserInfo			= _userInfo;
		_notification.iOSProperties		= _iosProperties;
		_notification.AndroidProperties	= _androidProperties;

		return _notification;
	}

	private void CancelAllLocalNotifications ()
	{
		NPBinding.NotificationService.CancelAllLocalNotification();
	}

	// Update is called once per frame
	void Update () {
	}

	void EnableNotifications(){
		CancelAllLocalNotifications();
		CrossPlatformNotification _notification = new CrossPlatformNotification();
		if(fixedNotificationTime){
			System.DateTime setDate = new System.DateTime(System.DateTime.Now.Year,System.DateTime.Now.Month,System.DateTime.Now.Day,notificationTime,00,00);
			if(System.DateTime.Compare(System.DateTime.Now,setDate) >= 0)
				setDate = setDate.AddDays(1);
			_notification = CreateNotification (setDate, eNotificationRepeatInterval.DAY);
		}
		else
			_notification = CreateNotification ((60*60*24), eNotificationRepeatInterval.DAY);
		
		NPBinding.NotificationService.ScheduleLocalNotification(_notification);
		
		
	}

	void OnApplicationQuit()
	{
		EnableNotifications();
	}

	void OnApplicationFocus(bool focusStatus)
	{
		if(!focusStatus)
			EnableNotifications();
	}
}
