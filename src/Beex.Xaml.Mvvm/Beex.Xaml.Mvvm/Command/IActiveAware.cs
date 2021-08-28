// Copyright (c) well-e.
// Licensed under the MIT License.
// base on:https://github.com/prismlibrary

using System;

namespace Beex.Xaml.Mvvm
{
    /// <summary>
    /// Interface that defines if the object instance is active
    /// and notifies when the activity changes.
    /// </summary>
    public interface IActiveAware
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Notifies that the value for <see cref="IsActive"/> property has changed.
        /// </summary>
        event EventHandler IsActiveChanged;
    }
}
