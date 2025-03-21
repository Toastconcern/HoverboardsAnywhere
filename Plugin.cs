using System;
using BepInEx;

namespace HoverboardsAnywhere
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;

		void Start()
		{
			/* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */
			GorillaTagger.OnPlayerSpawned(OnGameInitialized)

              		NetworkSystem.Instance.OnMultiplayerStarted += OnJoin;
           		NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeave;
		}

		void OnEnable()
		{
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized()
		{
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
		}

		void Update()
		{
			/* Code here runs every frame when the mod is enabled */
		}

		/* This attribute tells Utilla to call this method when a modded room is joined */
		public void OnJoin()
		{
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/
	    if (NetworkSystem.Instance.GameModeString.Contains("MODDED_"))
     		{
            GorillaLocomotion.Player.Instance.SetEnableHoverboard(enable: true);
            VRRig.LocalRig.hoverboardVisual.SetActive(active: true);
            inRoom = true;
		}
  		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		public void OnLeave()
		{
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            GorillaLocomotion.Player.Instance.SetEnableHoverboard(enable: false);
            VRRig.LocalRig.hoverboardVisual.SetActive(active: false);
            inRoom = false;
		}
	}
}
