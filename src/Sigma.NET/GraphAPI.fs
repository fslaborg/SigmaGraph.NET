namespace Sigma.NET

open System.Runtime.InteropServices
open System
open DynamicObj
open System.Globalization
open Microsoft.FSharp.Reflection
//open Microsoft.FSharp.Core.FSharpValue

// https://www.bsimard.com/2018/04/25/graph-viz-with-sigmajs.html


[<AutoOpen>]
type VisGraphElement() = 
    
    /// <summary>
    /// Initializes a new node with the given key.
    /// </summary>
    /// <param name="key">The unique identifier for the node.</param>
    /// <returns>A new Node instance with the specified key.</returns>
    static member node key = Node.Init(key = key) 
    
    /// <summary>
    /// Sets additional data for a node.
    /// </summary>
    /// <param name="Label">Optional label for the node.</param>
    /// <param name="Size">Optional size of the node.</param>
    /// <param name="Color">Optional color of the node.Needs HTML color codes</param>
    /// <param name="Hidden">Optional flag to hide the node.</param>
    /// <param name="ForceLabel">Optional flag to force the label visibility.</param>
    /// <param name="ZIndex">Optional z-index of the node(controles the stacking order of overlapping HTML elements, higher index appears in front of lower index).</param>
    /// <param name="StyleType">Optional style type of the node.</param>
    /// <param name="X">Optional X coordinate for the node.</param>
    /// <param name="Y">Optional Y coordinate for the node.</param>
    /// <returns>A function that updates the node with the specified data.</returns>
    static member withNodeData(
            ?Label      : string,
            ?Size       : #IConvertible,
            ?Color      : string,
            ?Hidden     : bool,
            ?ForceLabel : bool,
            ?ZIndex     : int,
            ?StyleType  : StyleParam.NodeType,
            ?X          : #IConvertible,
            ?Y          : #IConvertible
    ) =
        fun (node:Node) -> 
            let styleType = Option.map StyleParam.NodeType.toString StyleType
            let displayData = 
                    match node.TryGetTypedValue<DisplayData>("attributes") with
                    | None -> DisplayData()
                    | Some a -> a
                    |> DisplayData.Style
                        (?Label=Label,?Size=Size,?Color=Color,?Hidden=Hidden,
                         ?ForceLabel=ForceLabel,?ZIndex=ZIndex,?StyleType=styleType,?X=X,?Y=Y)
            displayData |> DynObj.setValue node "attributes"
            node
    
    
    /// <summary>
    /// Initializes a new edge with the given source and target nodes.
    /// </summary>
    /// <param name="source">The key of the source node of the edge.</param>
    /// <param name="target">The key of the target node of the edge.</param>
    /// <returns>A new Edge instance connecting the source and target nodes.</returns>
    static member edge source target = Edge.Init(source = source,target = target) 
    
    /// <summary>
    /// Sets additional data for an edge.
    /// </summary>
    /// <param name="Label">Optional label for the edge.</param>
    /// <param name="Size">Optional size of the edge.</param>
    /// <param name="Color">Optional color of the edge. Needs HTML color codes.</param>
    /// <param name="Hidden">Optional flag to hide the edge.</param>
    /// <param name="ForceLabel">Optional flag to force the label visibility.</param>
    /// <param name="ZIndex">Optional z-index of the edge(controles the stacking order of overlapping HTML elements, higher index appears in front of lower index).</param>
    /// <param name="StyleType">Optional style type of the edge.</param>
    /// <param name="X">Optional X coordinate for the edge.</param>
    /// <param name="Y">Optional Y coordinate for the edge.</param>
    /// <returns>A function that updates the edge with the specified data.</returns>
    static member withEdgeData(
            ?Label      : string,
            ?Size       : #IConvertible,
            ?Color      : string,
            ?Hidden     : bool,
            ?ForceLabel : bool,
            ?ZIndex     : int,
            ?StyleType  : StyleParam.EdgeType,
            ?X          : #IConvertible,
            ?Y          : #IConvertible
    ) =
        fun (edge:Edge) -> 
            let styleType = Option.map StyleParam.EdgeType.toString StyleType
            let displayData = 
                    match edge.TryGetTypedValue<DisplayData>("attributes") with
                    | None -> DisplayData()
                    | Some a -> a
                    |> DisplayData.Style
                        (?Label=Label,?Size=Size,?Color=Color,?Hidden=Hidden,
                         ?ForceLabel=ForceLabel,?ZIndex=ZIndex,?StyleType=styleType,?X=X,?Y=Y)
            displayData |> DynObj.setValue edge "attributes"
            edge


// Module to manipulate and sytely a graph
///<summary>
/// Module to manipulate and style a graph
/// </summary>
type VisGraph() =
    /// <summary>
    /// Creates an empty SigmaGraph instance
    /// </summary>
    /// <returns>A new empty SigmaGraph</returns>
    [<CompiledName("Empty")>]
    static member empty () = SigmaGraph()

    /// <summary>
    /// Adds a single node to a SigmaGraph.
    /// </summary>
    /// <param name="node">The node to add.</param>
    /// <param name="graph">The SigmaGraph to which the node will be added.</param>
    /// <returns>The updated SigmaGraph with the added node.</returns>
    [<CompiledName("WithNode")>]
    static member withNode (node:Node) (graph:SigmaGraph) = 
        graph.AddNode(node)
        graph       
   
    /// <summary>
    /// Adds a sequence of nodes to a SigmaGraph.
    /// </summary>
    /// <param name="nodes">The sequence of nodes to add.</param>
    /// <param name="graph">The SigmaGraph to which the nodes will be added.</param>
    /// <returns>The updated SigmaGraph with the added nodes.</returns>
    [<CompiledName("WithNodes")>]
    static member withNodes (nodes:Node seq) (graph:SigmaGraph) = 
        nodes |> Seq.iter (fun node -> graph.AddNode node) 
        graph
    
    /// <summary>
    /// Adds a single edge to a SigmaGraph.
    /// </summary>
    /// <param name="edge">The edge to add.</param>
    /// <param name="graph">The SigmaGraph to which the edge will be added.</param>
    /// <returns>The updated SigmaGraph with the added edge.</returns>
    [<CompiledName("WithEdge")>]
    static member withEdge (edge:Edge) (graph:SigmaGraph) = 
        graph.AddEdge(edge)
        graph       
    
    /// <summary>
    /// Adds a sequence of edges to a SigmaGraph.
    /// </summary>
    /// <param name="edges">The sequence of edges to add.</param>
    /// <param name="graph">The SigmaGraph to which the edges will be added.</param>
    /// <returns>The updated SigmaGraph with the added edges.</returns>
    [<CompiledName("WithEdges")>]
    static member withEdges (edges:Edge seq) (graph:SigmaGraph) = 
        edges |> Seq.iter (fun edge -> graph.AddEdge edge) 
        graph
    
    /// <summary>
    /// Assigns a random layout to a SigmaGraph.
    /// </summary>
    /// <param name="Scale">Optional scale for the random layout.</param>
    /// <param name="Center">Optional center for the random layout.</param>
    /// <param name="Dimensions">Optional dimensions for the random layout.</param>
    /// <returns>The updated SigmaGraph with the random layout.</returns>
    [<CompiledName("WithRandomLayout")>] 
    static member withRandomLayout(
        [<Optional; DefaultParameterValue(null)>] ?Scale,
        [<Optional; DefaultParameterValue(null)>] ?Center,
        [<Optional; DefaultParameterValue(null)>] ?Dimensions    
        ) = 
            fun (graph:SigmaGraph) -> 
                graph.Layout <- Layout.Random (RandomOptions.Init(?Dimensions=Dimensions,?Center=Center,?Scale=Scale))
                graph   
    
    /// <summary>
    /// Assigns a circular layout to a SigmaGraph.
    /// </summary>
    /// <param name="Scale">Optional scale for the circular layout.</param>
    /// <param name="Center">Optional center for the circular layout.</param>
    /// <returns>The updated SigmaGraph with the circular layout.</returns>
    [<CompiledName("WithCircularLayout")>]
    static member withCircularLayout(
        [<Optional; DefaultParameterValue(null)>] ?Scale,
        [<Optional; DefaultParameterValue(null)>] ?Center
        ) = 
            fun (graph:SigmaGraph) -> 
                graph.Layout <- Layout.Circular (CircularOptions.Init(?Scale=Scale,?Center=Center))
                graph   
 

    /// <summary>
    /// Assigns a ForceAtlas2 layout to a SigmaGraph.
    /// </summary>
    /// <param name="Iterations">Optional number of iterations for the layout algorithm.</param>
    /// <param name="Settings">Optional settings for the layout algorithm.</param>
    /// <param name="GetEdgeWeight">Optional function to get edge weight.</param>
    /// <returns>The updated SigmaGraph with the ForceAtlas2 layout.</returns>
    [<CompiledName("WithForceAtlas2")>]
    static member withForceAtlas2(
        [<Optional; DefaultParameterValue(null)>] ?Iterations, 
        [<Optional; DefaultParameterValue(null)>] ?Settings,
        [<Optional; DefaultParameterValue(null)>] ?GetEdgeWeight) = 
        fun (graph:SigmaGraph) ->
            graph.Layout <- Layout.FA2 (FA2Options.Init(?Iterations=Iterations,?GetEdgeWeight=GetEdgeWeight,?Settings=Settings))
            graph
    /// <summary>
    /// Assigns a no-overlap layout to a SigmaGraph.
    /// </summary>
    /// <param name="MaxIterations">Optional maximum number of iterations for the layout algorithm.</param>
    /// <param name="Settings">Optional settings for the layout algorithm.</param>
    /// <returns>The updated SigmaGraph with the no-overlap layout.</returns>
    [<CompiledName("WithNoverlap")>]
    static member withNoverlap(
        [<Optional; DefaultParameterValue(null)>] ?MaxIterations,
        [<Optional; DefaultParameterValue(null)>] ?Settings) = 
        fun (graph:SigmaGraph) ->
            graph.Layout <- Layout.Noverlap (NoverlapOptions.Init(?MaxIterations = MaxIterations,?Settings=Settings))
            graph



    /// <summary>
    /// Sets the size of the SigmaGraph canvas.
    /// </summary>
    /// <param name="Width">Optional width of the canvas.</param>
    /// <param name="Height">Optional height of the canvas.</param>
    /// <returns>The updated SigmaGraph with the specified canvas size.</returns>
    [<CompiledName("WithSize")>]
    static member withSize
        (
            [<Optional; DefaultParameterValue(null)>] ?Width: CssLength,
            [<Optional; DefaultParameterValue(null)>] ?Height: CssLength
        ) =

        fun (graph:SigmaGraph) ->
            graph.Width <- Option.defaultValue Defaults.DefaultWidth Width
            graph.Height <- Option.defaultValue Defaults.DefaultHeight Height
            
            graph

    /// <summary>
    /// Sets the renderer settings for the SigmaGraph.
    /// </summary>
    /// <param name="settings">The renderer settings to apply.</param>
    /// <returns>The updated SigmaGraph with the specified renderer settings.</returns>.
    [<CompiledName("WithRenderer")>]
    static member withRenderer(settings:Render.Settings) = 
        fun (graph:SigmaGraph) ->
            graph.Settings <- settings
            graph
    
    /// <summary>
    /// Adds a hover selector widget to the SigmaGraph.
    /// </summary>
    /// <param name="enable">Optional flag to enable or disable the hover selector (default is true).</param>
    /// <returns>The updated SigmaGraph with the hover selector widget.</returns>
    [<CompiledName("WithHoverSelector")>]
    static member withHoverSelector(?enable:bool) = 
        fun (graph:SigmaGraph) ->
            let enable = Option.defaultValue true enable
            if enable then
                graph.Widgets.Add("""const state={};function setHoveredNode(e){e?(state.hoveredNode=e,state.hoveredNeighbors=new Set(graph.neighbors(e))):(state.hoveredNode=void 0,state.hoveredNeighbors=void 0),renderer.refresh()}renderer.on("enterNode",({node:e})=>{setHoveredNode(e)}),renderer.on("leaveNode",()=>{setHoveredNode(void 0)}),renderer.setSetting("nodeReducer",(e,t)=>{let o=t;return state.hoveredNeighbors&&!state.hoveredNeighbors.has(e)&&state.hoveredNode!==e&&(o.label="",o.color="#f6f6f6"),o}),renderer.setSetting("edgeReducer",(e,t)=>{let o=t;return state.hoveredNode&&!graph.hasExtremity(e,state.hoveredNode)&&(o.hidden=!0),o});""")            
            graph
    
    /// <summary>
    /// Shows the SigmaGraph as an HTML document.
    /// </summary>
    /// <param name="graph">The SigmaGraph to display.</param>
    /// <returns>The path to the temporary HTML file created and opened.</returns>
    [<CompiledName("Show")>] 
    static member show() (graph:SigmaGraph) = 
        HTML.show(graph)