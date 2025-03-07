﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace TodoApp.Avalonia.Database;

public class TodoItem : OfflineClientEntity
{
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets if the task was completed.
    /// </summary>
    public bool IsComplete { get; set; } = false;
}
