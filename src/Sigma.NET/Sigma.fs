namespace Sigma.NET 


open DynamicObj

/// <summary>Represents a graph within the Sigma.NET framework.</summary>    
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
    
    /// <summary>Adds a node to the graph.</summary>
    member this.AddNode (node:Node) = 
        tmpGraphData.addNode (node) 

    /// <summary>Adds an edge to the graph.</summary>
    member this.AddEdge (edge:Edge) = 
        tmpGraphData.addEdge(edge) 
    
    /// <summary>Returns a string representation of the widgets in the graph.</summary>
    member this.GetWidgetsAsString () =
        tmpWidgets |> Seq.reduce (fun acc x -> acc + x + " ")
    
    /// <summary>Gets or sets the graph data for this SigmaGraph instance.</summary>
    member val GraphData  = tmpGraphData  with get,set
    
    /// <summary>Gets or sets the layout configuration for this SigmaGraph instance.</summary>
    member val Layout     = tmpLayout  with get,set
    
    /// <summary>Gets or sets the rendering settings for this SigmaGraph instance.</summary>
    member val Settings   = tmpSetting  with get,set  
    
    /// <summary>Gets or sets the collection of widgets associated with this SigmaGraph instance.</summary>
    member val Widgets    = tmpWidgets  with get,set

    /// <summary>Gets or sets the width of the graph visualization.</summary>
    member val Width      = tmpWidth  with get,set

    /// <summary>Gets or sets the height of the graph visualization.</summary>
    member val Height     = tmpHeight  with get,set


        
