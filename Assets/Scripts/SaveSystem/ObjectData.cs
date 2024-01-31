using System;

/// <summary>
/// Provides functionality for managing profile-independent data, including the <see cref="IsProfileIndependent"/> property.
/// </summary>

[Serializable]
public class ObjectData
{
    /// <summary>
    /// Gets or sets a value indicating whether the data associated with the object is profile-independent.
    /// </summary>
    public bool IsProfileIndependent { get; set; }
}