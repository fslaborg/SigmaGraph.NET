namespace Sigma.NET

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

/// Represents data for display elements such as nodes and edges.
type DisplayData() =
    inherit DynamicObj ()
    /// Initializes a new instance of DisplayData with optional parameters.
    /// Parameters:
    ///   - Label: Optional label for the display data.
    ///   - Size: Optional size of the display data.
    ///   - Color: Optional color for the display data.
    ///   - Hidden: Optional flag indicating if the display data is hidden.
    ///   - ForceLabel: Optional flag to force display the label.
    ///   - ZIndex: Optional z-index value for the display data.
    ///   - StyleType: Optional style type for the display data.
    ///   - X: Optional X coordinate for the display data.
    ///   - Y: Optional Y coordinate for the display data.
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
    /// Applies style updates to a DisplayData object.
    /// Parameters:
    ///   - Label: Optional label to update.
    ///   - Size: Optional size to update.
    ///   - Color: Optional color to update.
    ///   - Hidden: Optional flag to update hidden status.
    ///   - ForceLabel: Optional flag to update force label status.
    ///   - ZIndex: Optional z-index value to update.
    ///   - StyleType: Optional style type to update.
    ///   - X: Optional X coordinate to update.
    ///   - Y: Optional Y coordinate to update.
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

                Label      |> DynObj.setValueOpt displayData "label"
                Size       |> DynObj.setValueOpt displayData "size"                
                Color      |> DynObj.setValueOpt displayData "color"
                Hidden     |> DynObj.setValueOpt displayData "hidden"
                ForceLabel |> DynObj.setValueOpt displayData "forceLabel"
                ZIndex     |> DynObj.setValueOpt displayData "zIndex"
                StyleType  |> DynObj.setValueOpt displayData "type" 
                X          |> DynObj.setValueOpt displayData "x"
                Y          |> DynObj.setValueOpt displayData "y"                
                // out ->
                displayData
            )


/// Represents an edge in the graph with source and target nodes.
type Edge() =
    inherit DynamicObj ()
    /// Initializes a new instance of Edge with mandatory source and target parameters.
    /// Parameters:
    ///   - source: Source node identifier.
    ///   - target: Target node identifier.
    ///   - Key: Optional key for the edge.
    ///   - DisplayData: Optional display data associated with the edge.
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
    /// Applies style updates to an Edge object.
    /// Parameters:
    ///   - source: Source node identifier.
    ///   - target: Target node identifier.
    ///   - Key: Optional key to update.
    ///   - DisplayData: Optional display data to update.
    static member Style
        (    
            source,
            target,
            ?Key,
            ?DisplayData

        ) =
            (fun (edge:Edge) -> 
                
                source      |> DynObj.setValue    edge "source"
                target      |> DynObj.setValue    edge "target"
                Key         |> DynObj.setValueOpt edge "key"
                DisplayData |> DynObj.setValueOpt    edge "attributes"
                
                // out ->
                edge
            )
/// Represents a node in the graph.
type Node() =
    inherit DynamicObj ()
   
    /// Initializes a new instance of Node with a mandatory key parameter.
    /// Parameters:
    ///   - key: Identifier for the node.
    ///   - DisplayData: Optional display data associated with the node.
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
    /// Applies style updates to a Node object.
    /// Parameters:
    ///   - key: Identifier for the node.
    ///   - DisplayData: Optional display data to update.
    static member Style
        (    
            key,
            ?DisplayData

        ) =
            (fun (node:Node) -> 

                key         |> DynObj.setValue node "key"
                DisplayData |> DynObj.setValueOpt node "attributes"
                // out ->
                node
            )
/// Represents the data structure for a graph, including nodes and edges.
///
/// The GraphData type is used to manage and manipulate the graph's nodes and edges. 
/// Nodes and edges are stored in internal `ResizeArray` collections for efficient access and modification.
type GraphData() =
    /// Internal storage for nodes.
    /// A collection of nodes in the graph.
    /// Nodes are stored in a `ResizeArray` to allow dynamic resizing and efficient access.
    let nodes = ResizeArray()
    /// Internal storage for edges.
    /// A collection of edges in the graph.
    /// Edges are stored in a `ResizeArray` to allow dynamic resizing and efficient access.  
    let edges = ResizeArray()
    
    [<JsonProperty("nodes")>]
    member _.Nodes = nodes
    [<JsonProperty("edges")>]
    member _.Edges = edges

    /// Adds a node to the graph.
    /// Parameters:
    ///   - node: Node to be added.
    member _.addNode(node:Node) =
        nodes.Add(node)

    /// Adds an edge to the graph.
    /// Parameters:
    ///   - edge: Edge to be added.
    member _.addEdge(edge:Edge) =
        edges.Add(edge)
            
