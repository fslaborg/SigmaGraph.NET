namespace SigmaGraph.NET

open System
open System.IO
open Newtonsoft.Json
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

//open Cytoscape.NET.CytoscapeModel
open Giraffe.ViewEngine


/// <summary>
/// Provides methods to generate HTML content for visualizing graphs with Sigma.
/// </summary>
type HTML =
 

    /// <summary>
    /// Creates a script to render a graph with Sigma.js.
    /// </summary>
    /// <param name="graphData">JSON representation of the graph.</param>
    /// <param name="layout">Layout configuration for the graph.</param>
    /// <param name="settings">Settings configuration for the graph.</param>
    /// <param name="containerId">ID of the HTML container for the graph.</param>
    /// <param name="widgets">Widgets to include in the graph.</param>
    /// <param name="sigmaJSRef">Reference to Sigma.js libraries.</param>
    /// <returns>An HTML script element for embedding Sigma.js graph.</returns>
    static member CreateGraphScript
        (
            graphData: string,
            layout : string,
            settings: string,
            containerId: string,
            widgets: string,
            sigmaJSRef: JSlibReference
        ) =
        match sigmaJSRef with
        | Require gjs ->
            script
                [ _type "text/javascript" ]
                [
                    rawText (
                        Globals.REQUIREJS_SCRIPT_TEMPLATE
                                  //'[GRAPHOLOGY_JS]',
                                  //'[SIGMA_JS]',
                                  //'[GRAPHOLOGY-LIB_JS]',
                            .Replace("[GRAPHOLOGY_JS]", gjs.Graphology)
                            .Replace("[SIGMA_JS]", gjs.Sigma)
                            .Replace("[GRAPHOLOGY-LIB_JS]", gjs.GraphologyLib)
                            .Replace("[CONTAINERID]",containerId)
                            .Replace("[LAYOUT]",layout)
                            .Replace("[SETTINGS]",settings)
                            .Replace("[WIDGETS]",widgets)
                            .Replace("[GRAPHDATA]", graphData)
                    )
                ]
        | _ ->
            script
                [ _type "text/javascript" ]
                [
                    rawText (
                        Globals.SCRIPT_TEMPLATE
                            .Replace("[CONTAINERID]",containerId)
                            .Replace("[LAYOUT]",layout)
                            .Replace("[SETTINGS]",settings)
                            .Replace("[WIDGETS]",widgets)
                            .Replace("[GRAPHDATA]", graphData)
                    )
                ]

    /// <summary>
    /// Generates a complete HTML document including the graph and Sigma.js references.
    /// </summary>
    /// <param name="graphHTML">List of XML nodes representing the graph HTML.</param>
    /// <param name="sigmaJSRef">Reference to Sigma.js libraries.</param>
    /// <param name="AdditionalHeadTags">Optional additional tags to include in the &lt;head&gt; section.</param>
    /// <returns>An HTML document with the embedded graph.</returns>
    static member Doc(
        graphHTML: XmlNode list, 
        sigmaJSRef: JSlibReference,
        ?AdditionalHeadTags
    ) =
        let additionalHeadTags =
            defaultArg AdditionalHeadTags []

        let sigmaScriptRefs =
            match sigmaJSRef with
            | Local gjs -> [
                                script [ _src gjs.Graphology ] []
                                script [ _src gjs.Sigma ] []
                                script [ _src gjs.GraphologyLib ] []
                            
                           ]
            | CDN gjs -> [
                                script [ _src gjs.Graphology ] []
                                script [ _src gjs.Sigma ] []
                                script [ _src gjs.GraphologyLib ] []
                            
                           ]
            | Full gjs -> [
                                script [ _type "text/javascript" ] [ rawText gjs.Graphology ]
                                script [ _type "text/javascript" ] [ rawText gjs.Sigma ]
                                script [ _type "text/javascript" ] [ rawText gjs.GraphologyLib ]
                            ]
            | NoReference -> [rawText ""]
            | Require gjs -> [
                                script [ _src gjs.Graphology ] []
                                script [ _src gjs.Sigma ] []
                                script [ _src gjs.GraphologyLib ] []
                           ]


        html
            []
            [
                head
                    []
                    [
                        yield! sigmaScriptRefs
                        yield! additionalHeadTags
                    ]
                body [] [ yield! graphHTML]
            ]

    /// <summary>
    /// Creates HTML content for a graph with Sigma.js embedded in a specified container.
    /// </summary>
    /// <param name="graphData">JSON representation of the graph.</param>
    /// <param name="layout">Layout configuration for the graph.</param>
    /// <param name="settings">Settings configuration for the graph.</param>
    /// <param name="divId">ID of the HTML container for the graph.</param>
    /// <param name="widgets">Widgets to include in the graph.</param>
    /// <param name="sigmaJSRef">Reference to Sigma.js libraries.</param>
    /// <param name="Width">Optional width of the graph container.</param>
    /// <param name="Height">Optional height of the graph container.</param>
    /// <returns>A list of HTML nodes for rendering the graph.</returns>
    static member CreateGraphHTML
        (
            graphData: string,
            layout : string,
            settings: string,
            divId: string,
            widgets: string,
            sigmaJSRef: JSlibReference,
            ?Width: CssLength,
            ?Height: CssLength
        ) =
        let width, height = 
            Width |> Option.defaultValue Defaults.DefaultWidth,
            Height |> Option.defaultValue Defaults.DefaultHeight

        let graphScript =
            HTML.CreateGraphScript(
                graphData = graphData,
                layout = layout,
                settings = settings,
                containerId = divId,
                widgets = widgets,
                sigmaJSRef = sigmaJSRef
            )

        [
            
            div [ _id "graph" ] // TODO: maybe remove this div?
                [
                    div
                        [ _id divId; _style (sprintf "width: %s; height: %s" (CssLength.serialize width) (CssLength.serialize height) )]
                        [
                            rawText "&nbsp"
                            comment "SigmaGraph.NET graph will be drawn inside this DIV"
                        ]
                    graphScript
                ]
        ]

    /// <summary>
    /// Converts a CyGraph to its HTML representation, with optional display options.
    /// </summary>
    /// <param name="DisplayOpts">Optional display options for the graph.</param>
    /// <returns>A function that takes a SigmaGraph and returns HTML nodes.</returns>
    static member toGraphHTMLNodes (
        ?DisplayOpts: DisplayOptions
    ) =
        fun (graph:SigmaGraph) -> 

            let displayOptions = defaultArg DisplayOpts Defaults.DefaultDisplayOptions
            let sigmaReference = displayOptions |> DisplayOptions.getSigmaReference

            let guid = Guid.NewGuid().ToString()
            let id   = sprintf "e%s" <| Guid.NewGuid().ToString().Replace("-","").Substring(0,10)

            let ntsettings = new JsonSerializerSettings()
            let _ = ntsettings.ReferenceLoopHandling <- ReferenceLoopHandling.Ignore             

            let jsonGraph = JsonConvert.SerializeObject (graph.GraphData,ntsettings)

            let layout = Layout.serialize graph.Layout  
            
            let settings = JsonConvert.SerializeObject (graph.Settings,ntsettings)

            HTML.CreateGraphHTML(
                graphData = jsonGraph,
                layout = layout,
                settings = settings,
                divId = id,
                widgets = graph.GetWidgetsAsString(),
                sigmaJSRef = sigmaReference,
                // Maybe we should use the DisplayOptions width and height here?
                Width = graph.Width,
                Height = graph.Height
            )

    /// <summary>
    /// Converts a SigmaGraph to its HTML representation as a string.
    /// </summary>
    /// <param name="DisplayOpts">Optional display options for the graph.</param>
    /// <returns>A function that takes a SigmaGraph and returns HTML as a string.</returns>
    static member toGraphHTML(
        ?DisplayOpts: DisplayOptions
    ) =
        fun (graph:SigmaGraph) -> 
            graph
            |> HTML.toGraphHTMLNodes(?DisplayOpts = DisplayOpts)
            |> RenderView.AsString.htmlNodes

    /// <summary>
    /// Converts a SigmaGraph to its HTML representation and embeds it into a full HTML page.
    /// </summary>
    /// <param name="DisplayOpts">Optional display options for the graph.</param>
    /// <returns>A function that takes a SigmaGraph and returns a complete HTML document as a string.</returns>
    static member toEmbeddedHTML (
        ?DisplayOpts: DisplayOptions
    ) =
        fun (graph:SigmaGraph) ->
            let displayOptions = defaultArg DisplayOpts Defaults.DefaultDisplayOptions
            let sigmaReference = DisplayOptions.getSigmaReference displayOptions
            HTML.Doc(
                graphHTML = (HTML.toGraphHTMLNodes(DisplayOpts = displayOptions) graph),
                sigmaJSRef = sigmaReference
            )
            |> RenderView.AsString.htmlDocument


    /// <summary>
    /// Chooses the process to open plots with depending on the OS. Thanks to @zyzhu for hinting at a solution (https://github.com/plotly/Plotly.NET/issues/31)
    /// </summary>
    /// <param name="path">Path to the file to be opened.</param>
    static member internal openOsSpecificFile path =
        if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then
            let psi = System.Diagnostics.ProcessStartInfo(FileName = path, UseShellExecute = true)
            System.Diagnostics.Process.Start(psi) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then
            System.Diagnostics.Process.Start("xdg-open", path) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then
            System.Diagnostics.Process.Start("open", path) |> ignore
        else
            invalidOp "Not supported OS platform"

    /// <summary>
    /// Shows a SigmaGraph in the default web browser by creating an HTML file and opening it.
    /// </summary>
    /// <param name="graph">The SigmaGraph to display.</param>
    /// <param name="DisplayOpts">Optional display options for the graph.</param>
    /// <returns>The path to the temporary HTML file.</returns>
    static member show (graph:SigmaGraph, ?DisplayOpts: DisplayOptions) = 
        let guid = Guid.NewGuid().ToString()
        let html = HTML.toEmbeddedHTML(?DisplayOpts = DisplayOpts) graph
        let tempPath = Path.GetTempPath()
        let file = sprintf "%s.html" guid
        let path = Path.Combine(tempPath, file)
        File.WriteAllText(path, html)
        path |> HTML.openOsSpecificFile
