namespace Sigma.NET

open System.Runtime.InteropServices
open System
open DynamicObj

// https://www.bsimard.com/2018/04/25/graph-viz-with-sigmajs.html


[<AutoOpen>]
type VisGraphElement() = 
    /// Initializes a new node with the given key.
    /// Parameters:
    ///   - key: The unique identifier for the node.
    /// Returns: A new Node instance with the specified key.
    static member node key = Node.Init(key = key) 
    /// Sets additional data for a node.
    /// Parameters:
    ///   - Label: Optional label for the node.
    ///   - Size: Optional size of the node.
    ///   - Color: Optional color of the node.
    ///   - Hidden: Optional flag to hide the node.
    ///   - ForceLabel: Optional flag to force the label visibility.
    ///   - ZIndex: Optional z-index of the node.
    ///   - StyleType: Optional style type of the node.
    ///   - X: Optional X coordinate for the node.
    ///   - Y: Optional Y coordinate for the node.
    /// Returns: A function that updates the node with the specified data.
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
    
    /// Initializes a new edge with the given source and target nodes.
    /// Parameters:
    ///   - source: The source node of the edge.
    ///   - target: The target node of the edge.
    /// Returns: A new Edge instance connecting the source and target nodes.
    static member edge source target = Edge.Init(source = source,target = target) 
    
    /// Sets additional data for an edge.
    /// Parameters:
    ///   - Label: Optional label for the edge.
    ///   - Size: Optional size of the edge.
    ///   - Color: Optional color of the edge.
    ///   - Hidden: Optional flag to hide the edge.
    ///   - ForceLabel: Optional flag to force the label visibility.
    ///   - ZIndex: Optional z-index of the edge.
    ///   - StyleType: Optional style type of the edge.
    ///   - X: Optional X coordinate for the edge.
    ///   - Y: Optional Y coordinate for the edge.
    /// Returns: A function that updates the edge with the specified data.
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
type VisGraph() =
    /// Creates an empty SigmaGraph instance.
    /// Returns: A new empty SigmaGraph.
    [<CompiledName("Empty")>]
    static member empty () = SigmaGraph()

    /// Adds a single node to a SigmaGraph.
    /// Parameters:
    ///   - node: The node to add.
    ///   - graph: The SigmaGraph to which the node will be added.
    /// Returns: The updated SigmaGraph with the added node.
    [<CompiledName("WithNode")>]
    static member withNode (node:Node) (graph:SigmaGraph) = 
        graph.AddNode(node)
        graph       
    /// Adds a sequence of nodes to a SigmaGraph.
    /// Parameters:
    ///   - nodes: The sequence of nodes to add.
    ///   - graph: The SigmaGraph to which the nodes will be added.
    /// Returns: The updated SigmaGraph with the added nodes.
    [<CompiledName("WithNodes")>]
    static member withNodes (nodes:Node seq) (graph:SigmaGraph) = 
        nodes |> Seq.iter (fun node -> graph.AddNode node) 
        graph
    /// Adds a single edge to a SigmaGraph.
    /// Parameters:
    ///   - edge: The edge to add.
    ///   - graph: The SigmaGraph to which the edge will be added.
    /// Returns: The updated SigmaGraph with the added edge.
    [<CompiledName("WithEdge")>]
    static member withEdge (edge:Edge) (graph:SigmaGraph) = 
        graph.AddEdge(edge)
        graph       
    /// Adds a sequence of edges to a SigmaGraph.
    /// Parameters:
    ///   - edges: The sequence of edges to add.
    ///   - graph: The SigmaGraph to which the edges will be added.
    /// Returns: The updated SigmaGraph with the added edges.
    [<CompiledName("WithEdges")>]
    static member withEdges (edges:Edge seq) (graph:SigmaGraph) = 
        edges |> Seq.iter (fun edge -> graph.AddEdge edge) 
        graph
    /// Assigns a random layout to a SigmaGraph.
    /// Parameters:
    ///   - Scale: Optional scale for the random layout.
    ///   - Center: Optional center for the random layout.
    ///   - Dimensions: Optional dimensions for the random layout.
    /// Returns: The updated SigmaGraph with the random layout.
    [<CompiledName("WithRandomLayout")>] 
    static member withRandomLayout(
        [<Optional; DefaultParameterValue(null)>] ?Scale,
        [<Optional; DefaultParameterValue(null)>] ?Center,
        [<Optional; DefaultParameterValue(null)>] ?Dimensions    
        ) = 
            fun (graph:SigmaGraph) -> 
                graph.Layout <- Layout.Random (RandomOptions.Init(?Dimensions=Dimensions,?Center=Center,?Scale=Scale))
                graph   
    /// Assigns a circular layout to a SigmaGraph.
    /// Parameters:
    ///   - Scale: Optional scale for the circular layout.
    ///   - Center: Optional center for the circular layout.
    /// Returns: The updated SigmaGraph with the circular layout.
    [<CompiledName("WithCircularLayout")>]
    static member withCircularLayout(
        [<Optional; DefaultParameterValue(null)>] ?Scale,
        [<Optional; DefaultParameterValue(null)>] ?Center
        ) = 
            fun (graph:SigmaGraph) -> 
                graph.Layout <- Layout.Circular (CircularOptions.Init(?Scale=Scale,?Center=Center))
                graph   
 

    /// Assigns a ForceAtlas2 layout to a SigmaGraph.
    /// Parameters:
    ///   - Iterations: Optional number of iterations for the layout algorithm.
    ///   - Settings: Optional settings for the layout algorithm.
    ///   - GetEdgeWeight: Optional function to get edge weight.
    /// Returns: The updated SigmaGraph with the ForceAtlas2 layout.
    [<CompiledName("WithForceAtlas2")>]
    static member withForceAtlas2(
        [<Optional; DefaultParameterValue(null)>] ?Iterations, 
        [<Optional; DefaultParameterValue(null)>] ?Settings,
        [<Optional; DefaultParameterValue(null)>] ?GetEdgeWeight) = 
        fun (graph:SigmaGraph) ->
            graph.Layout <- Layout.FA2 (FA2Options.Init(?Iterations=Iterations,?GetEdgeWeight=GetEdgeWeight,?Settings=Settings))
            graph
    /// Assigns a no-overlap layout to a SigmaGraph.
    /// Parameters:
    ///   - MaxIterations: Optional maximum number of iterations for the layout algorithm.
    ///   - Settings: Optional settings for the layout algorithm.
    /// Returns: The updated SigmaGraph with the no-overlap layout.
    [<CompiledName("WithNoverlap")>]
    static member withNoverlap(
        [<Optional; DefaultParameterValue(null)>] ?MaxIterations,
        [<Optional; DefaultParameterValue(null)>] ?Settings) = 
        fun (graph:SigmaGraph) ->
            graph.Layout <- Layout.Noverlap (NoverlapOptions.Init(?MaxIterations = MaxIterations,?Settings=Settings))
            graph



    /// Sets the size of the SigmaGraph canvas.
    /// Parameters:
    ///   - Width: Optional width of the canvas.
    ///   - Height: Optional height of the canvas.
    /// Returns: The updated SigmaGraph with the specified canvas size.
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

    /// Sets the renderer settings for the SigmaGraph.
    /// Parameters:
    ///   - settings: The renderer settings to apply.
    /// Returns: The updated SigmaGraph with the specified renderer settings.
    [<CompiledName("WithRenderer")>]
    static member withRenderer(settings:Render.Settings) = 
        fun (graph:SigmaGraph) ->
            graph.Settings <- settings
            graph
    /// Adds a hover selector widget to the SigmaGraph.
    /// Parameters:
    ///   - enable: Optional flag to enable or disable the hover selector (default is true).
    /// Returns: The updated SigmaGraph with the hover selector widget.
    [<CompiledName("WithRenderer")>]
    static member withHoverSelector(?enable:bool) = 
        fun (graph:SigmaGraph) ->
            let enable = Option.defaultValue true enable
            if enable then
                graph.Widgets.Add("""const state={};function setHoveredNode(e){e?(state.hoveredNode=e,state.hoveredNeighbors=new Set(graph.neighbors(e))):(state.hoveredNode=void 0,state.hoveredNeighbors=void 0),renderer.refresh()}renderer.on("enterNode",({node:e})=>{setHoveredNode(e)}),renderer.on("leaveNode",()=>{setHoveredNode(void 0)}),renderer.setSetting("nodeReducer",(e,t)=>{let o=t;return state.hoveredNeighbors&&!state.hoveredNeighbors.has(e)&&state.hoveredNode!==e&&(o.label="",o.color="#f6f6f6"),o}),renderer.setSetting("edgeReducer",(e,t)=>{let o=t;return state.hoveredNode&&!graph.hasExtremity(e,state.hoveredNode)&&(o.hidden=!0),o});""")            
            graph
    /// Shows the SigmaGraph as an HTML document.
    /// Parameters:
    ///   - graph: The SigmaGraph to display.
    /// Returns: The path to the temporary HTML file created and opened.
    [<CompiledName("Show")>] 
    static member show() (graph:SigmaGraph) = 
        HTML.show(graph)