﻿using System;
using Fomm.PackageManager;
using System.IO;
using Fomm.Games.Fallout3.Tools.TESsnip;
using Fomm.PackageManager.ModInstallLog;
using System.Windows.Forms;
using Fomm.Games.Fallout3.Tools.BSA;
using System.Collections.Generic;
using Fomm.Games.Fallout3.Tools.AutoSorter;
using Fomm.Games.Fallout3.Script;

namespace Fomm.Games.FalloutNewVegas.Script
{
	public class FalloutNewVegasModInstallScript : Fallout3ModInstallScript
	{
		#region Constructors

		/// <summary>
		/// A simple constructor that initializes the object.
		/// </summary>
		/// <param name="p_fomodMod">The <see cref="fomod"/> against which to run the script.</param>
		public FalloutNewVegasModInstallScript(fomod p_fomodMod, ModInstallerBase p_mibInstaller)
			: base(p_fomodMod, p_mibInstaller)
		{
		}

		#endregion

		#region Version Checking

		/// <summary>
		/// Indicates whether or not NVSE is present.
		/// </summary>
		/// <returns><lang cref="true"/> if NVSE is installed; <lang cref="false"/> otherwise.</returns>
		public override bool ScriptExtenderPresent()
		{
			PermissionsManager.CurrentPermissions.Assert();
			return File.Exists("nvse_loader.exe");
		}

		/// <summary>
		/// Gets the version of the sript extender that is installed.
		/// </summary>
		/// <returns>The version of the sript extender that is installed, or <lang cref="null"/> if no
		/// sript extender is installed.</returns>
		public override Version GetScriptExtenderVersion()
		{
			PermissionsManager.CurrentPermissions.Assert();
			if (!File.Exists("nvse_loader.exe"))
				return null;
			return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo("nvse_loader.exe").FileVersion.Replace(", ", "."));
		}

		#endregion

		#region Plugin Activation

		protected override void DoCommitActivePlugins()
		{
			File.WriteAllLines(((FalloutNewVegasGameMode)Program.GameMode).PluginsFilePath, GetActivePlugins());
		}

		#endregion

		#region Ini Management

		#region Ini File Value Retrieval

		/// <summary>
		/// Retrieves the specified Fallout.ini value as a string.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as a string.</returns>
		public override string GetFalloutIniString(string p_strSection, string p_strKey)
		{
			return GetSettingsString(FalloutNewVegasGameMode.SettingsFile.FOIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified Fallout.ini value as an integer.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as an integer.</returns>
		public override int GetFalloutIniInt(string p_strSection, string p_strKey)
		{
			return GetSettingsInt(FalloutNewVegasGameMode.SettingsFile.FOIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified FalloutPrefs.ini value as a string.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as a string.</returns>
		public override string GetPrefsIniString(string p_strSection, string p_strKey)
		{
			return GetSettingsString(FalloutNewVegasGameMode.SettingsFile.FOPrefsIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified FalloutPrefs.ini value as an integer.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as an integer.</returns>
		public override int GetPrefsIniInt(string p_strSection, string p_strKey)
		{
			return GetSettingsInt(FalloutNewVegasGameMode.SettingsFile.FOPrefsIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified GECKCustom.ini value as a string.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as a string.</returns>
		public override string GetGeckIniString(string p_strSection, string p_strKey)
		{
			return GetSettingsString(FalloutNewVegasGameMode.SettingsFile.GeckIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified GECKCustom.ini value as an integer.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as an integer.</returns>
		public override int GetGeckIniInt(string p_strSection, string p_strKey)
		{
			return GetSettingsInt(FalloutNewVegasGameMode.SettingsFile.GeckIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified GECKPrefs.ini value as a string.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as a string.</returns>
		public override string GetGeckPrefsIniString(string p_strSection, string p_strKey)
		{
			return GetSettingsString(FalloutNewVegasGameMode.SettingsFile.GeckPrefsIniPath, p_strSection, p_strKey);
		}

		/// <summary>
		/// Retrieves the specified GECKPrefs.ini value as an integer.
		/// </summary>
		/// <param name="p_strSection">The section containing the value to retrieve.</param>
		/// <param name="p_strKey">The key of the value to retrieve.</param>
		/// <returns>The specified value as an integer.</returns>
		public override int GetGeckPrefsIniInt(string p_strSection, string p_strKey)
		{
			return GetSettingsInt(FalloutNewVegasGameMode.SettingsFile.GeckPrefsIniPath, p_strSection, p_strKey);
		}

		#endregion

		#region Ini Editing

		/// <summary>
		/// Sets the specified value in the Fallout.ini file to the given value. 
		/// </summary>
		/// <param name="p_strSection">The section in the Ini file to edit.</param>
		/// <param name="p_strKey">The key in the Ini file to edit.</param>
		/// <param name="p_strValue">The value to which to set the key.</param>
		/// <param name="p_booSaveOld">Not used.</param>
		/// <returns><lang cref="true"/> if the value was set; <lang cref="false"/>
		/// if the user chose not to overwrite the existing value.</returns>
		public override bool EditFalloutINI(string p_strSection, string p_strKey, string p_strValue, bool p_booSaveOld)
		{
			return EditINI(FalloutNewVegasGameMode.SettingsFile.FOIniPath, p_strSection, p_strKey, p_strValue);
		}

		/// <summary>
		/// Sets the specified value in the FalloutPrefs.ini file to the given value. 
		/// </summary>
		/// <param name="p_strSection">The section in the Ini file to edit.</param>
		/// <param name="p_strKey">The key in the Ini file to edit.</param>
		/// <param name="p_strValue">The value to which to set the key.</param>
		/// <param name="p_booSaveOld">Not used.</param>
		/// <returns><lang cref="true"/> if the value was set; <lang cref="false"/>
		/// if the user chose not to overwrite the existing value.</returns>
		public override bool EditPrefsINI(string p_strSection, string p_strKey, string p_strValue, bool p_booSaveOld)
		{
			return EditINI(FalloutNewVegasGameMode.SettingsFile.FOPrefsIniPath, p_strSection, p_strKey, p_strValue);
		}

		/// <summary>
		/// Sets the specified value in the GECKCustom.ini file to the given value. 
		/// </summary>
		/// <param name="p_strSection">The section in the Ini file to edit.</param>
		/// <param name="p_strKey">The key in the Ini file to edit.</param>
		/// <param name="p_strValue">The value to which to set the key.</param>
		/// <param name="p_booSaveOld">Not used.</param>
		/// <returns><lang cref="true"/> if the value was set; <lang cref="false"/>
		/// if the user chose not to overwrite the existing value.</returns>
		public override bool EditGeckINI(string p_strSection, string p_strKey, string p_strValue, bool p_booSaveOld)
		{
			return EditINI(FalloutNewVegasGameMode.SettingsFile.GeckIniPath, p_strSection, p_strKey, p_strValue);
		}

		/// <summary>
		/// Sets the specified value in the GECKPrefs.ini file to the given value. 
		/// </summary>
		/// <param name="p_strSection">The section in the Ini file to edit.</param>
		/// <param name="p_strKey">The key in the Ini file to edit.</param>
		/// <param name="p_strValue">The value to which to set the key.</param>
		/// <param name="p_booSaveOld">Not used.</param>
		/// <returns><lang cref="true"/> if the value was set; <lang cref="false"/>
		/// if the user chose not to overwrite the existing value.</returns>
		public override bool EditGeckPrefsINI(string p_strSection, string p_strKey, string p_strValue, bool p_booSaveOld)
		{
			return EditINI(FalloutNewVegasGameMode.SettingsFile.GeckPrefsIniPath, p_strSection, p_strKey, p_strValue);
		}

		#endregion

		#endregion

		#region Misc Info

		/// <summary>
		/// Determines if archive invalidation is active.
		/// </summary>
		/// <returns><lang cref="true"/> if archive invalidation is active;
		/// <lang cref="false"/> otherwise.</returns>
		public override bool IsAIActive()
		{
			return Tools.ArchiveInvalidation.IsActive();
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Cleans up used resources.
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion
	}
}
