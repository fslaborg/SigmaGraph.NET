namespace Sigma.NET 


open DynamicObj

/// Represents a graph within the Sigma.NET framework.    
type SigmaGraph() = 
    inherit DynamicObj ()

    let tmpGraphData  = new GraphData()
    let tmpLayout     = Layout.Random (RandomOptions())
    let tmpSetting    = Render.Settings()
    let tmpWidgets    = 
        let tmp = ResizeArray<string>()
        tmp.Add("")
        tmp
    let tmpWidth      = Defaults.DefaultWidth 
    let tmpHeight     = Defaults.DefaultHeight
    /// Adds a node to the graph.
    member this.AddNode (node:Node) = 
        tmpGraphData.addNode (node) 
    /// Adds an edge to the graph.
    member this.AddEdge (edge:Edge) = 
        tmpGraphData.addEdge(edge) 
    /// Returns a string representation of the widgets in the graph.
    member this.GetWidgetsAsString () =
        tmpWidgets |> Seq.reduce (fun acc x -> acc + x + " ")
    /// Gets or sets the graph data for this SigmaGraph instance.
    member val GraphData  = tmpGraphData  with get,set
    /// Gets or sets the layout configuration for this SigmaGraph instance.
    member val Layout     = tmpLayout  with get,set
    /// Gets or sets the rendering settings for this SigmaGraph instance.
    member val Settings   = tmpSetting  with get,set  
    /// Gets or sets the collection of widgets associated with this SigmaGraph instance.
    member val Widgets    = tmpWidgets  with get,set

    /// Gets or sets the width of the graph visualization.
    member val Width      = tmpWidth  with get,set
    /// Gets or sets the height of the graph visualization.
    member val Height     = tmpHeight  with get,set


        
