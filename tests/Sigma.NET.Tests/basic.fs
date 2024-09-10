module BasicTests

open Expecto
open DynamicObj
open Sigma.NET
open Sigma.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting

//open helperFunctions

[<Tests>]
let nodeTests =
    testList "Nodes" [
        testCase "Key_Test" <| fun () ->
            let node = Node.Init("1")
            
            Expect.isTrue (string(node.TryGetValue("key").Value) = "1") "The initiated node has the wrong/no key"

        testCase "Type_Test" <| fun () ->
            let node = Node.Init("1")

            Expect.isTrue (node.GetType() = typeof<Node>) "The initiated node has the wrong type(not type : Node)"

        testCase "Displaydata_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init())
            

            Expect.isTrue (string(node.TryGetValue("attributes").Value) = "Sigma.NET.DisplayData") "The DisplayData was not added when initializing the node./ The node has no DisplayData."

    ]
[<Tests>]
let edgeTests =
    testList "Edges" [
        testCase "Source_Test" <| fun () ->
            let nodes = [Node.Init("1");Node.Init("2")]
            let edge = Edge.Init("1","2","edge1")
            
            Expect.isTrue (string(edge.TryGetValue("source").Value) = "1") "The source node is wrong"

        testCase "Target_Test" <| fun () ->
            let nodes = [Node.Init("1");Node.Init("2")]
            let edge = Edge.Init("1","2","edge1")
            
            Expect.isTrue (string(edge.TryGetValue("target").Value) = "2") "The target-Node is wrong."  

        testCase "Key_Test" <| fun () ->
            let nodes = [Node.Init("1");Node.Init("2")]
            let edge = Edge.Init("1","2","edge1")
            
            Expect.isTrue (string(edge.TryGetValue("key").Value) = "edge1") "The key of the edge is wrong."  
        testCase "DisplayData_Test" <| fun () ->
            let nodes = [Node.Init("1");Node.Init("2")]
            let edge = Edge.Init("1","2","edge1", DisplayData.Init())
            
            Expect.isTrue (string(edge.TryGetValue("attributes").Value) = "Sigma.NET.DisplayData") "The edge has no Displaydata."    
    ]
[<Tests>]
let visGraphTests =
    testList "VisGraph" [
        testCase "Type_Test" <| fun () ->
            
            Expect.isTrue ((VisGraph.empty().GetType()) = typeof<SigmaGraph>) "VisGraph.Empty did not initializy a graph of type SigmaGraph."
        testCase "Empty_Test" <| fun () ->
            
            Expect.isTrue ((VisGraph.empty().GraphData.Nodes.Count = 0 && VisGraph.empty().GraphData.Edges.Count = 0)) "The initialized graph is not empty."

        testCase "WithNode_Test" <| fun () ->
            let emptygraph = VisGraph.empty()
            let node = Node.Init("1")
            let graph = VisGraph.withNode node emptygraph
            
            
            Expect.isTrue (graph.GraphData.Nodes.Count = 1) "The node was not added to the graph."

        testCase "WithNodes_Test" <| fun () ->
            let emptygraph = VisGraph.empty()
            let nodes = [Node.Init("1");Node.Init("2")]
            let graph =
                emptygraph
                |> VisGraph.withNodes(nodes)

            Expect.isTrue (graph.GraphData.Nodes.Count = 2) "The wrong number of nodes(or no nodes) have been added to the graph."

        testCase "WithEdge_Test" <| fun () ->
            let emptygraph = VisGraph.empty()
            let nodes = [Node.Init("1");Node.Init("2")]
            let edge = Edge.Init("1", "2", "edge1")
            let graph =
                emptygraph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdge(edge)

            Expect.isTrue (graph.GraphData.Edges.Count = 1) "The edge was not added to the graph" 

        testCase "WithEdges_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)

            Expect.isTrue (graph.GraphData.Edges.Count = 5) "The wrong number of edges(or no edges) were added to the graph." 

        testCase "WithCircularLayout_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withCircularLayout()

            Expect.isTrue (string(graph.Layout.GetType()) = "Sigma.NET.Layout+Circular") "The circular Layout was not applied."  

        testCase "WithRandomLayout_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withRandomLayout()

            Expect.isTrue (string(graph.Layout.GetType()) = "Sigma.NET.Layout+Random") "The random Layout was not applied."    

        testCase "WithHoverSelectorTrue_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withHoverSelector(true)

            Expect.isTrue (graphWithData.Widgets.Count = 2) "The Hoverselector was not applied Properly"

        testCase "WithHoverSelectorFalse_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withHoverSelector(false)

            Expect.isTrue (graphWithData.Widgets.Count = 1) "The Hoverselector was not applied(turned off) Properly"
            
        testCase "WithForceAtlas2_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withForceAtlas2(1)

            Expect.isTrue (graphWithData.Layout.ToString() = "FA2 Sigma.NET.FA2Options") "The ForceAtlas2 Layout was not applied."
        testCase "WithNoverlap_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withNoverlap(1)

            Expect.isTrue (graphWithData.Layout.ToString() = "Noverlap Sigma.NET.NoverlapOptions") "The Noverlap Layout was not applied."
        testCase "WithSize_Test" <| fun () ->
            let graph = VisGraph.empty()
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graphWithData =
                graph
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
                |> VisGraph.withSize(CssLength.Percent(80), CssLength.Percent(80))

            Expect.isTrue (graphWithData.Width.ToString() = "Percent 80" && graphWithData.Height.ToString() = "Percent 80") "The size was not set correctly."
        

    ]

[<Tests>]
let renderSettingsTest =
    testList "Renderer" [
        testCase "LabelSize_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelSize = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelSize: 20")) $"The label size was not set correctly.{x}"
        testCase "HideEdgesOnMoveTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("hideEdgesOnMove: True")) "HideEdgesOnMove was not set to True correctly"
        testCase "HideEdgesOnMoveFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("hideEdgesOnMove: False")) "HideEdgesOnMove was not set to false correctly"
        testCase "HideLabelsOnMoveTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("hideLabelsOnMove: True")) "HideLabelsOnMove was not set to true correctly"
        testCase "HideLabelsOnMoveFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("hideLabelsOnMove: False")) "HideLabelsOnMove was not set to false correctly"
        testCase "RenderLabelsTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("renderLabels: True")) "RenderLabels was not set to true correctly"
        testCase "RenderLabelsFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("renderLabels: False")) "RenderLabels was not set to false correctly"
        testCase "RenderEdgeLabelsTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("renderEdgeLabels: True")) "RenderEdgeLabels was not set to true correctly"
        testCase "RenderEdgeLabelsFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("renderEdgeLabels: False")) "RenderEdgeLabels was not set to false correctly"
        testCase "EnableEdgeClickEventsTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeClickEvents: True")) "EnableEdgeClickEvents was not set to true correctly"
        testCase "EnableEdgeClickEventsFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeClickEvents: False")) "EnableEdgeClickEvents was not set to false correctly"
        testCase "EnableEdgeWheelEventsTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeWheelEvents: True")) "EnableEdgeWheelEvents was not set to true correctly"
        testCase "EnableEdgeWheelEventsFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeWheelEvents: False")) "EnableEdgeWheelEvents was not set to false correctly"
        testCase "EnableEdgeHoverEventsTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeHoverEvents: True")) "EnableEdgeHoverEvents was not set to true correctly"
        testCase "EnableEdgeHoverEventsFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("enableEdgeHoverEvents: False")) "EnableEdgeHoverEvents was not set to false correctly"
        testCase "DefaultNodeColor_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeColor = "#D20103"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("defaultNodeColor: #D20103")) "The Node colour was not set correctly"
        testCase "DefaultNodeType_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeType = StyleParam.NodeType.Circle))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("defaultNodeType: circle")) "The Node type was not set correctly"
        testCase "DefaultEdgeColor_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeColor = "#D20103"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("defaultEdgeColor: #D20103")) "The DefaultEdgeColor was not set correctly"
        testCase "DefaultEdgeTypeLine_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Line))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("defaultEdgeType: line")) "The DefaultEdgeType was not set to -line- correctly"
        testCase "DefaultEdgeTypeArrow_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Arrow))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("defaultEdgeType: arrow")) "The DefaultEdgeType was not set to -arrow- correctly"
        testCase "LabelFont_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelFont = "Arial"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelFont: Arial")) "The LabelFont was not set to -Arial- correctly"
        testCase "EdgeLabelFont_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelFont = "Arial"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("edgeLabelFont: Arial")) "The EdgeLabelFont was not set to -Arial- correctly"
        testCase "EdgeLabelSize_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelSize = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("edgeLabelSize: 20")) "The EdgeLabelSize was not set to 20 correctly"
        testCase "LabelWeight_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelWeight = "Bold"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelWeight: Bold")) "The LabelWeight was not set to -Bold- correctly"
        testCase "EdgeLabelWeight_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelWeight = "Normal"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("edgeLabelWeight: Normal")) "The EdgeLabelWeight was not set to -Normal- correctly"
        testCase "LabelColor_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelColor = Render.ColorOrReference.Init("#ffd700")))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelColor: Sigma.NET.Render+ColorOrReference")) "The Labelcolor was not set correctly"
        testCase "EdgeLabelColor_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelColor = Render.ColorOrReference.Init("#ffd700")))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("edgeLabelColor: Sigma.NET.Render+ColorOrReference")) "The Labelcolor was not set correctly"
        testCase "StagePadding_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(StagePadding = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("stagePadding: 20")) "The StagePadding was not set to 20 correctly"
        testCase "LabelDensity_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelDensity = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelDensity: 20")) "The LabelDensity was not set to 20 correctly"
        testCase "LabelGridCellSize_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelGridCellSize = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelGridCellSize: 20")) "The LabelGridCellSize was not set to 20 correctly"
        testCase "LabelRenderedSizeThreshold_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderedSizeThreshold = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelRenderedSizeThreshold: 20")) "The LabelRenderedSizeThreshold was not set to 20 correctly"
        testCase "ZIndexTrue_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = true))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("zIndex: True")) "The ZIndex was not set to true correctly"
        testCase "ZIndexFalse_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = false))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("zIndex: False")) "The ZIndex was not set to false correctly"
        testCase "MinCameraRatio_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(MinCameraRatio = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("minCameraRatio: 20")) "The MinCameraRatio was not set to 20 correctly"
        testCase "MaxCameraRatio_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(MaxCameraRatio = 20))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("maxCameraRatio: 20")) "The MaxCameraRatio was not set to 20 correctly"
        testCase "LabelRenderer_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderer = "customLabelRenderer"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("labelRenderer: customLabelRenderer")) "The LabelRenderer was not set to -customLabelRenderer- correctly"
        testCase "EdgeLabelRenderer_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelRenderer = "customEdgeLabelRenderer"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("edgeLabelRenderer: customEdgeLabelRenderer")) "The EdgeLabelRenderer was not set to -customEdgeLabelRenderer- correctly"
        testCase "HoverRenderer_Test" <| fun () ->
            let graphWithData =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HoverRenderer = "customHoverRenderer"))
            let x = graphWithData.Settings.GetDynamicMemberNames().ToDisplayString()

            Expect.isTrue (x.Contains("hoverRenderer: customHoverRenderer")) "The HoverRenderer was not set to -customHoverRenderer- correctly"
        
        
        
        
    ]
