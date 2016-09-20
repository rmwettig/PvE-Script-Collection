using UnityEngine;
using System.Collections;

/// <summary>
/// Helper class for displaying information
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Tooltip
{
    private string title;
    public string Title { get { return title; } }
    private string description;
    public string Description { get { return description; } }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="caption">title text</param>
    /// <param name="content">description text</param>
    public Tooltip(string caption, string content)
    {
        title = caption;
        description = content;
    }
}
