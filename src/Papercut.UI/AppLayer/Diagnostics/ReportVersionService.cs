﻿// Papercut
// 
// Copyright © 2008 - 2012 Ken Robertson
// Copyright © 2013 - 2021 Jaben Cargman
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


namespace Papercut.AppLayer.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using Autofac;

    using Papercut.Core.Annotations;

    using Serilog;

    public class ReportVersionService : IStartable
    {
        private readonly ILogger _logger;

        public ReportVersionService(ILogger logger)
        {
            this._logger = logger;
        }

        public void Start()
        {
            var productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            this._logger.Information("Papercut Version {PapercutVersion:l}", productVersion);
        }

        #region Begin Static Container Registrations

        /// <summary>
        /// Called dynamically from the RegisterStaticMethods() call in the container module.
        /// </summary>
        /// <param name="builder"></param>
        [UsedImplicitly]
        static void Register([NotNull] ContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<ReportVersionService>().AsImplementedInterfaces().SingleInstance();
        }

        #endregion
    }
}