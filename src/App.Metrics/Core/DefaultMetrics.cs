﻿// Copyright (c) Allan Hardy. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using App.Metrics.Configuration;
using App.Metrics.Core.Interfaces;
using App.Metrics.Interfaces;
using App.Metrics.Internal;
using App.Metrics.Internal.Interfaces;
using App.Metrics.Utils;

namespace App.Metrics.Core
{
    /// <summary>
    ///     Provides access to record application metrics.
    /// </summary>
    /// <remarks>
    ///     This is the entry point to the application's metrics registry
    /// </remarks>
    /// <seealso cref="IMetrics" />
    internal sealed class DefaultMetrics : IMetrics
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultMetrics" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="clock">The clock.</param>
        /// <param name="globalFilter">The global filter.</param>
        /// <param name="measureMetricsProvider">The factory used to provide access to metric managers.</param>
        /// <param name="metricsBuilder">The factory used to provide access to metric builders.</param>
        /// <param name="metricsProvider">The metrics advanced manager factory.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="metricsManager">The metrics manager.</param>
        /// <param name="healthProvider">The health status provider.</param>
        public DefaultMetrics(
            AppMetricsOptions options,
            IClock clock,
            IFilterMetrics globalFilter,
            IMeasureMetrics measureMetricsProvider,
            IBuildMetrics metricsBuilder,
            IProvideMetrics metricsProvider,
            IProvideMetricValues dataManager,
            IManageMetrics metricsManager,
            IProvideHealth healthProvider)
        {
            Clock = clock ?? new StopwatchClock();
            GlobalTags = options.GlobalTags; // TODO: in reporting just get this from options
            GlobalFilter = globalFilter ?? new NoOpMetricsFilter();
            Measure = measureMetricsProvider;
            Build = metricsBuilder;
            Snapshot = dataManager;
            Provider = metricsProvider;
            Manage = metricsManager;
            Health = healthProvider;
        }

        /// <inheritdoc />
        public IProvideMetrics Provider { get; }

        /// <inheritdoc />
        public IBuildMetrics Build { get; }

        /// <inheritdoc />
        public IClock Clock { get; }

        /// <inheritdoc />
        public IProvideMetricValues Snapshot { get; }

        /// <inheritdoc />
        public IFilterMetrics GlobalFilter { get; }

        /// <inheritdoc />
        public GlobalMetricTags GlobalTags { get; }

        /// <inheritdoc />
        public IProvideHealth Health { get; }

        /// <inheritdoc />
        public IManageMetrics Manage { get; }

        /// <inheritdoc />
        public IMeasureMetrics Measure { get; }
    }
}