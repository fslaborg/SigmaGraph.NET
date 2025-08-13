namespace SigmaGraph.NET

open DynamicObj
open System
open Newtonsoft.Json

  

//{key: 'Thomas', attributes: { x: 0,  y: 10, size: 1, label: 'A'}},
// type: "image", image: "./user.svg", color: RED

// DisplayData = (for notes and edges) 
  //label: string | null;
  //size: number;
  //color: string;
  //hidden: boolean;
  //forceLabel: boolean;
  //zIndex: number;
  //type: string; 

/// <summary>
/// Represents data for display elements such as nodes and edges.
/// </summary>
type DisplayData() =
    inherit DynamicObj ()
    
    /// <summary>
    /// Initializes a new instance of DisplayData with optional parameters.
    /// </summary>
    /// <param name="Label">Optional label for the display data.</param>
    /// <param name="Size">Optional size of the display data.</param>
    /// <param name="Color">Optional color for the display data.</param>
    /// <param name="Hidden">Optional flag indicating if the display data is hidden.</param>
    /// <param name="ForceLabel">Optional flag to force display the label.</param>
    /// <param name="ZIndex">Optional z-index value for the display data.</param>
    /// <param name="StyleType">Optional style type for the display data.</param>
    /// <param name="X">Optional X coordinate for the display data.</param>
    /// <param name="Y">Optional Y coordinate for the display data.</param>
    static member Init 
        (           
            ?Label      : string,
            ?Size       : #IConvertible,
            ?Color      : string,
            ?Hidden     : bool,
            ?ForceLabel : bool,
            ?ZIndex     : int,
            ?StyleType  : string,
            ?X          : #IConvertible,
            ?Y          : #IConvertible

        ) =    
            DisplayData()
            |> DisplayData.Style
                (
                    ?Label = Label,
                    ?Size = Size,                    
                    ?Color = Color,
                    ?Hidden     = Hidden,
                    ?ForceLabel = ForceLabel,
                    ?ZIndex     = ZIndex,
                    ?StyleType =  StyleType,
                    ?X = X,
                    ?Y = Y
                )

    // Applies updates to Attributes()
    /// <summary>
    /// Applies style updates to a DisplayData object.
    /// </summary>
    /// <param name="Label">Optional label to update.</param>
    /// <param name="Size">Optional size to update.</param>
    /// <param name="Color">Optional color to update.</param>
    /// <param name="Hidden">Optional flag to update hidden status.</param>
    /// <param name="ForceLabel">Optional flag to update force label status.</param>
    /// <param name="ZIndex">Optional z-index value to update.</param>
    /// <param name="StyleType">Optional style type to update.</param>
    /// <param name="X">Optional X coordinate to update.</param>
    /// <param name="Y">Optional Y coordinate to update.</param>

    static member Style
        (    
            ?Label,
            ?Size,
            ?Color,
            ?Hidden,
            ?ForceLabel,
            ?ZIndex,
            ?StyleType,
            ?X,
            ?Y
        ) =
            (fun (displayData:DisplayData) -> 

                displayData|> DynObj.setOptionalProperty "label"        Label              
                displayData|> DynObj.setOptionalProperty "size"         Size                           
                displayData|> DynObj.setOptionalProperty "color"        Color              
                displayData|> DynObj.setOptionalProperty "hidden"       Hidden             
                displayData|> DynObj.setOptionalProperty "forceLabel"   ForceLabel             
                displayData|> DynObj.setOptionalProperty "zIndex"       ZIndex             
                displayData|> DynObj.setOptionalProperty "type"         StyleType       
                displayData|> DynObj.setOptionalProperty "x"            X              
                displayData|> DynObj.setOptionalProperty "y"            Y                              
                // out ->
                displayData
            )


/// <summary>
/// Represents an edge in the graph with source and target nodes.
/// </summary>
type Edge() =
    inherit DynamicObj ()
    
    /// <summary>
    /// Initializes a new instance of Edge with mandatory source and target parameters.
    /// </summary>
    /// <param name="source">Source node identifier.</param>
    /// <param name="target">Target node identifier.</param>
    /// <param name="Key">Optional key for the edge.</param>
    /// <param name="DisplayData">Optional display data associated with the edge.</param>
    static member Init 
        (            
            source : string,
            target : string,
            ?Key   : string,
            ?DisplayData : DisplayData
            
        ) =    
            Edge()
            |> Edge.Style
                (                    
                    source = source,
                    target = target,
                    ?Key = Key,
                    ?DisplayData = DisplayData
                )

    // Applies updates to Edge() 
    /// <summary>
    /// Applies style updates to an Edge object.
    /// </summary>
    /// <param name="source">Source node identifier.</param>
    /// <param name="target">Target node identifier.</param>
    /// <param name="Key">Optional key to update.</param>
    /// <param name="DisplayData">Optional display data to update.</param>
    static member Style
        (    
            source,
            target,
            ?Key,
            ?DisplayData

        ) =
            (fun (edge:Edge) -> 
                
                edge        |> DynObj.setProperty   "source"  source              
                edge        |> DynObj.setProperty   "target"  target   
                edge        |> DynObj.setOptionalProperty   "key"         Key                 
                edge        |> DynObj.setOptionalProperty   "attributes"  DisplayData
                // out ->
                edge
            )

/// <summary>
/// Represents a node in the graph.
/// </summary>
type Node() =
    inherit DynamicObj ()
   
    /// <summary>
    /// Initializes a new instance of Node with a mandatory key parameter.
    /// </summary>
    /// <param name="key">Identifier for the node.</param>
    /// <param name="DisplayData">Optional display data associated with the node.</param>
    static member Init 
        (
            key    : string,
            ?DisplayData : DisplayData
        ) =    
            Node()
            |> Node.Style
                (
                    key = key,
                    ?DisplayData = DisplayData

                )

    // Applies updates to Data()
    /// <summary>
    /// Applies style updates to a Node object.
    /// </summary>
    /// <param name="key">Identifier for the node.</param>
    /// <param name="DisplayData">Optional display data to update.</param>
    static member Style
        (    
            key,
            ?DisplayData

        ) =
            (fun (node:Node) -> 

                node        |> DynObj.setProperty  "key" key
                node        |> DynObj.setOptionalProperty  "attributes" DisplayData
                
                // out ->
                node
            )

/// <summary>
/// Represents the data structure for a graph, including nodes and edges.
/// </summary>
/// <remarks>
/// The GraphData type is used to manage and manipulate the graph's nodes and edges. 
/// Nodes and edges are stored in internal `ResizeArray` collections for efficient access and modification.
/// </remarks>
type GraphData() =
    /// <summary>
    /// Internal storage for nodes.
    /// A collection of nodes in the graph.
    /// Nodes are stored in a `ResizeArray` to allow dynamic resizing and efficient access.
    /// </summary>
    let nodes = ResizeArray()
    
    /// <summary>
    /// Internal storage for edges.
    /// A collection of edges in the graph.
    /// Edges are stored in a `ResizeArray` to allow dynamic resizing and efficient access.  
    /// </summary>
    let edges = ResizeArray()

    /// <summary>
    /// Internal storage for nodes.
    /// A collection of nodes in the graph.
    /// Nodes are stored in a `ResizeArray` to allow dynamic resizing and efficient access.
    /// </summary>
    [<JsonProperty("nodes")>]
    member _.Nodes = nodes

    /// <summary>
    /// Internal storage for edges.
    /// A collection of edges in the graph.
    /// Edges are stored in a `ResizeArray` to allow dynamic resizing and efficient access.
    /// </summary>
    [<JsonProperty("edges")>]
    member _.Edges = edges

    /// <summary>
    /// Adds a node to the graph.
    /// </summary>
    /// <param name="node">Node to be added.</param>
    member _.addNode(node:Node) =
        nodes.Add(node)

    /// <summary>
    /// Adds an edge to the graph.
    /// </summary>
    /// <param name="edge">Edge to be added.</param>
    member _.addEdge(edge:Edge) =
        edges.Add(edge)
            
