﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using BuildsAppReborn.Contracts.Models;
using BuildsAppReborn.Infrastructure;

namespace BuildsAppReborn.Client
{
    [Export(typeof(GlobalSettingsContainer))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class GlobalSettingsContainer
    {
        #region Constructors

        public GlobalSettingsContainer()
        {
            this.buildMonitorSettingsFilePath = Path.Combine(Consts.ApplicationUserProfileFolder, "buildMonitorSettings.json");
            this.generalSettingsFilePath = Path.Combine(Consts.ApplicationUserProfileFolder, "generalSettings.json");

            BuildMonitorSettingsContainer = SettingsContainer<BuildMonitorSettings>.Load(this.buildMonitorSettingsFilePath);
            this.generalSettingsContainer = SettingsContainer<GeneralSettings>.Load(this.generalSettingsFilePath);

            if (!this.generalSettingsContainer.Any())
            {
                this.generalSettingsContainer.Add(new GeneralSettings());
            }
        }

        #endregion

        #region Public Properties

        public SettingsContainer<BuildMonitorSettings> BuildMonitorSettingsContainer { get; }

        /// <summary>
        /// Gets the general settings.
        /// </summary>
        /// <value>
        /// The general settings.
        /// </value>
        public GeneralSettings GeneralSettings => this.generalSettingsContainer.Single();

        #endregion

        #region Public Methods

        public void Save()
        {
            BuildMonitorSettingsContainer.Save(this.buildMonitorSettingsFilePath);
            this.generalSettingsContainer.Save(this.generalSettingsFilePath);
        }

        #endregion

        #region Private Fields

        private String buildMonitorSettingsFilePath;
        private SettingsContainer<GeneralSettings> generalSettingsContainer;
        private String generalSettingsFilePath;

        #endregion
    }
}