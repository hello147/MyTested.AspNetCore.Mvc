﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Http.ISession"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderSessionExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> NoSession<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.Session.Keys.Any())
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualShouldHaveTestBuilder.TestContext,
                    SessionTestBuilder.SessionName);
            }

            return actualShouldHaveTestBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> Session<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder, 
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualShouldHaveTestBuilder.TestContext,
                SessionTestBuilder.SessionName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.Session.Keys.Count());

            return actualShouldHaveTestBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets specific <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="sessionTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Http.ISession"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> Session<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder, 
            Action<ISessionTestBuilder> sessionTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            sessionTestBuilder(new SessionTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.Builder;
        }
    }
}
