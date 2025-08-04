namespace SigmaGraph.NET.Interactive

open SigmaGraph.NET

module Formatters = 
    
    /// <summary>Converts a Cytoscape type to it's HTML representation to show in a notebook environment.</summary>
    let toInteractiveHTML (graph:SigmaGraph): string = 
        let sigma = SigmaGraph.NET.InternalUtils.getUriJS()

        graph
        |> HTML.toGraphHTML(
            DisplayOpts = DisplayOptions.init(
                SigmaJSRef = JSlibReference.Require sigma
                )
            )