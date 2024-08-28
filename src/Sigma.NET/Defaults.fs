namespace Sigma.NET


open DynamicObj
open DynamicObj.Operators
open System.Runtime.InteropServices

open Giraffe.ViewEngine

/// <summary>
/// Contains mutable global default values.
/// Changing these values will apply the default values to all consecutive Graph generations.
/// </summary>
module Defaults =
    /// <summary>
    /// The default width of the graph container in generated HTML files. Default: 100%.
    /// </summary>
    let mutable DefaultWidth = CssLength.Percent 100
    
    /// <summary>
    /// The default height of the graph container in generated HTML files. Default: 900 pixels.
    /// </summary>
    let mutable DefaultHeight = CssLength.PX 900
    
    /// <summary>
    /// The default display options for the graph.
    /// </summary>
    let mutable DefaultDisplayOptions = DisplayOptions.initDefault()
    
    /// <summary>
    /// Resets global defaults to the initial values.
    /// </summary>
    let reset () =
        DefaultWidth <- CssLength.Percent 100
        DefaultHeight <- CssLength.PX 900
        DefaultDisplayOptions <- DisplayOptions.initDefault()
