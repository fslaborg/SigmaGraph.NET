namespace Sigma.NET


open DynamicObj
open DynamicObj.Operators
open System.Runtime.InteropServices

open Giraffe.ViewEngine

/// Contains mutable global default values.
/// Changing these values will apply the default values to all consecutive Graph generations.
module Defaults =

    /// The default width of the graph container in generated HTML files. Default: 100%
    let mutable DefaultWidth = CssLength.Percent 100

    /// The default height of the graph container in generated HTML files. Default: 900 pixels
    let mutable DefaultHeight = CssLength.PX 900

    /// The default display options for the graph
    let mutable DefaultDisplayOptions = DisplayOptions.initDefault()

    /// Resets global defaults to the initial values
    let reset () =
        DefaultWidth <- CssLength.Percent 100
        DefaultHeight <- CssLength.PX 900
        DefaultDisplayOptions <- DisplayOptions.initDefault()
