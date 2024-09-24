module BasicTests

open Expecto
open DynamicObj
open Sigma.NET
open Sigma.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting



[<Tests>]
let nodeTests =
    testList "Nodes" [
        let node = Node.Init("CorrectKey", DisplayData.Init())
        testCase "Key_Test" <| fun () ->
            let keyName = Expect.wantSome (node.TryGetTypedValue<string>("key")) "ErrorMessage"
            Expect.equal keyName "CorrectKey" $"The Key of the Node was not set correctly. The Key should be called -CorrectKey- but it is {keyName} ."

        testCase "Type_Test" <| fun () ->
            let nodeType = node.GetType()

            Expect.equal nodeType typeof<Node> $"The type of the node is expected to be -Sigma.NET.Node- but is {nodeType}"

        testCase "DisplayData_Test" <| fun () ->
            let displayDataType = ((node.TryGetTypedValue<DisplayData>("attributes")).Value).GetType()

            Expect.equal displayDataType typeof<DisplayData> $"There was added either no DisplayData at all or the added DisplayData is not of the type -DisplayData- . The Type that was added is {displayDataType} "

    ]
[<Tests>]
let edgeTests =
    testList "Edges" [
        let sourceNode = Node.Init("sourceNode")
        let targetNode = Node.Init("targetNode")
        let edge = Edge.Init("sourceNode","targetNode","testEdge", DisplayData.Init())
        testCase "Source_Test" <| fun () ->
            let sourceNodeKey = Expect.wantSome (sourceNode.TryGetTypedValue<string>("key")) "ErrorMessage"
            let sourceNodeKeyInEdge = Expect.wantSome (edge.TryGetTypedValue<string>("source")) "ErrorMessage"
            
            Expect.equal sourceNodeKeyInEdge sourceNodeKey $"The SourceNode was expected to be {sourceNodeKey} but is {sourceNodeKeyInEdge}"
        testCase "Target_Test" <| fun () ->
            let targetNodeKey = Expect.wantSome (targetNode.TryGetTypedValue<string>("key")) "ErrorMessage"
            let targetNodeKeyInEdge = Expect.wantSome (edge.TryGetTypedValue<string>("target")) "ErrorMessage"

            Expect.equal targetNodeKeyInEdge targetNodeKey $"The target Node was expected to be {targetNodeKey} but is {targetNodeKeyInEdge}" 
        testCase "Key_Test" <| fun () ->
            let edgeKey = Expect.wantSome (edge.TryGetTypedValue<string>("key")) "ErrorMessage"

            Expect.equal edgeKey "testEdge" $"The Key of the edge should be -testEdge- but it is {edgeKey}" 
        testCase "DisplayData_Test" <| fun () ->
            let edgeDDType = ((edge.TryGetTypedValue<DisplayData>("attributes")).Value).GetType()
            
            Expect.equal edgeDDType typeof<DisplayData> $"There was added either no DisplayData at all or the added DisplayData is not of the type -DisplayData- . The Type that was added is {edgeDDType} "
                
    ]
[<Tests>]
let visGraphTests =
    testList "VisGraph" [
        testCase "Type_Test" <| fun () ->
            let x = (VisGraph.empty().GetType())
            let y = typeof<SigmaGraph>

            Expect.equal x y $"The type of the Visgraph was expected to be {y} but it is {x}"
        testCase "Empty_Test" <| fun () ->
            
            Expect.isTrue ((VisGraph.empty().GraphData.Nodes.Count = 0 && VisGraph.empty().GraphData.Edges.Count = 0)) "The initialized graph should be empty but it isnt."
        testCase "WithNode_Test" <| fun () ->
            let graph = VisGraph.withNode (Node.Init("1")) (VisGraph.empty())
            let numberOfNodesAdded = graph.GraphData.Nodes.Count
            
            Expect.equal numberOfNodesAdded 1 $"It was expected that 1 Node gets added to the graph but {numberOfNodesAdded} Node/Nodes have been added to the Graph"
        testCase "WithNodes_Test" <| fun () ->
            let graph = VisGraph.withNodes ([Node.Init("1");Node.Init("2")]) (VisGraph.empty())
            let numberOfNodesAdded = graph.GraphData.Nodes.Count

            Expect.equal numberOfNodesAdded 2 $"It was expected that 2 Nodes are added to the graph but {numberOfNodesAdded} Nodes have been added to the graph"
        testCase "WithEdge_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1");Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "edge1"))
            let numberOfEdgesAdded = graph.GraphData.Edges.Count

            Expect.equal numberOfEdgesAdded 1 $"1 edge shouldve been added to the graph but {numberOfEdgesAdded} edges were added." 
        testCase "WithEdges_Test" <| fun () ->
            let nodes = [1 .. 10] |> List.map (fun key -> Node.Init(string(key),DisplayData.Init()))
            let edges = 
                let list = List.zip3 [1 .. 2 .. 10] [2 .. 2 .. 10] [1 .. 5]
                list |> List.map (fun (x,y,z) -> (Edge.Init(string x,string y,String.concat "" ["edge"; string z],DisplayData.Init())))
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes(nodes)
                |> VisGraph.withEdges(edges)
            let numberOfEdgesAdded = graph.GraphData.Edges.Count

            Expect.equal numberOfEdgesAdded 5 $"5 edges should have been added to the graph but instead {numberOfEdgesAdded} edges have been added." 
        testCase "WithCircularLayout_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withCircularLayout()
            let layoutType = string(graph.Layout.GetType())

            Expect.equal layoutType "Sigma.NET.Layout+Circular" $"The Layout type was expected to be -Sigma.NET.Layout+Circular- but it is {layoutType}"  
        testCase "WithRandomLayout_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRandomLayout()
            let layoutType = string(graph.Layout.GetType())

            Expect.equal layoutType  "Sigma.NET.Layout+Random" $"The layout was expected to be -Sigma.NET.Layout+Random- but it is {layoutType}"    

        testCase "WithHoverSelectorTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withHoverSelector(true)
            let widgedCount = graph.Widgets.Count
            Expect.equal widgedCount 2 $"The Hoverselector was not set to true correctly. If the Hoverselector is set to true the widgedCount should be 2, if it is set to false the widgedCount is 1. The actual widgedcount is {widgedCount} ."
        testCase "WithHoverSelectorFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withHoverSelector(false)
            let widgedCount = graph.Widgets.Count
            Expect.equal widgedCount 1 $"The Hoverselector was not set to false correctly.If the Hoverselector is set to true the widgedCount should be 2, if it is set to false the widgedCount is 1. The actual widgedcount is {widgedCount} . "
            
        testCase "WithForceAtlas2_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withForceAtlas2(1)
            let appliedLayout = graph.Layout.ToString()

            Expect.equal appliedLayout "FA2 Sigma.NET.FA2Options" $"The Layout was expected to be -FA2 Sigma.NET.FA2Options- but is {appliedLayout}"
        testCase "WithNoverlap_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNoverlap(1)
            let appliedLayout = graph.Layout.ToString()

            Expect.equal appliedLayout "Noverlap Sigma.NET.NoverlapOptions" $"The Layout was expected to be -Noverlap Sigma.NET.NoverlapOptions- but it is {appliedLayout}."
        testCase "WithSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withSize(CssLength.Percent(80), CssLength.Percent(80))
            let width = graph.Width.ToString()
            let height = graph.Height.ToString()

            Expect.equal width "Percent 80" $"The width was expected to be -Percent 80- but it is {width}"
            Expect.equal height "Percent 80" $"The height was expected to be -Percent 80- but it is {height}" 


    ]


[<Tests>]
let displayDataTests =
    testList "NodeDisplayData" [
        
        testCase "Label_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Label = "Node1"))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?label: Node1" $"The Label was not set to -Node1- correctly. {nodeasString}"
        testCase "Size_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Size = 10))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?size: 10" $"The Size was not set to 10 correctly. {nodeasString}"
        testCase "Color_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Color = "#0070b8"))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?color: #0070b8" $"The Color was not set to -#0070b8- correctly. {nodeasString}"
        testCase "HiddenTrue_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Hidden = true))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?hidden: True" $"The -Hidden- function was not set to true correctly. {nodeasString}"
        testCase "HiddenFalse_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Hidden = false))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?hidden: False" $"The -Hidden- function was not set to false correctly. {nodeasString}"
        testCase "ForceLabelTrue_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ForceLabel = true))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?forceLabel: True" $"The -ForceLabel- function was not set to true correctly. {nodeasString}"
        testCase "ForceLabelFalse_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ForceLabel = false))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?forceLabel: False" $"The -ForceLabel- function was not set to false correctly. {nodeasString}"
        testCase "ZIndex_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ZIndex = 10))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?zIndex: 10" $"The ZIndex was not set to 10 correctly. {nodeasString}"
        testCase "Styletype_Test" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(StyleType = "default"))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?type: default" $"The Styletype was not set to default correctly. {nodeasString}"
            
            
        
        
        

        

    ]
[<Tests>]
let renderSettingsTest =
    testList "Renderer" [
        testCase "LabelSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(LabelSize = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let valueOfLabelSize = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("labelSize")) "Test_LabelSize"

            Expect.stringContains dynamicMembers "labelSize:" $"The label size was not set as a Dynamic Member of the Graph.Settings correctly.{dynamicMembers}"
            Expect.equal valueOfLabelSize 20 $"The lablesize was set to the wrong number. The labelsize should be 20 but it is {valueOfLabelSize}."
        testCase "HideEdgesOnMoveTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("hideEdgesOnMove")) "Test_hideEOnMove"

            Expect.stringContains dynamicMembers "hideEdgesOnMove:" $"HideEdgesOnMove was not set as a dynamic member of Graph.Settings correctly.{dynamicMembers}"    
            Expect.isTrue value $"The HideEdgesOnMove function was not set to true as expected, instead it was set to {value}"
        testCase "HideEdgesOnMoveFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("hideEdgesOnMove")) "Test"
            Expect.stringContains dynamicMembers "hideEdgesOnMove:" $"HideEdgesOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The HideEdgesOnMove function was not set to false as expected, instead it was set to {value}"
        testCase "HideLabelsOnMoveTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("hideLabelsOnMove")) "Test"

            Expect.stringContains dynamicMembers "hideLabelsOnMove:" $"HideLabelsOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The HideLabelsOnMove function was not set to true as expected, instead it was set to {value}"
        testCase "HideLabelsOnMoveFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("hideLabelsOnMove")) "Test"

            Expect.stringContains dynamicMembers "hideLabelsOnMove:" $"HideLabelsOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The HideLabelsOnMove function was not set to false as expected, instead it was set to {value}"
        testCase "RenderLabelsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("renderLabels")) "Test"

            Expect.stringContains dynamicMembers "renderLabels:" $"renderLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The renderLabels function was not set to true as expected, instead it was set to {value}"
        testCase "RenderLabelsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("renderLabels")) "Test"

            Expect.stringContains dynamicMembers "renderLabels:" $"renderLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The renderLabels function was not set to false as expected, instead it was set to {value}"
        testCase "RenderEdgeLabelsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("renderEdgeLabels")) "Test"

            Expect.stringContains dynamicMembers "renderEdgeLabels:" $"RenderEdgeLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The renderEdgeLabels function was not set to true as expected, instead it was set to {value}"
        testCase "RenderEdgeLabelsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("renderEdgeLabels")) "Test"

            Expect.stringContains dynamicMembers "renderEdgeLabels:" $"RenderEdgeLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The renderEdgeLabels function was not set to false as expected, instead it was set to {value}"
        testCase "EnableEdgeClickEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeClickEvents")) "Test"

            Expect.isTrue value $"The enableEdgeClickEvents function was not set to true as expected, instead it was set to {value}"
            Expect.stringContains dynamicMembers "enableEdgeClickEvents:" $"EnableEdgeClickEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
        testCase "EnableEdgeClickEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeClickEvents")) "Test"

            Expect.isFalse value $"The enableEdgeClickEvents function was not set to false as expected, instead it was set to {value}"
            Expect.stringContains dynamicMembers "enableEdgeClickEvents:" $"EnableEdgeClickEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"    
        testCase "EnableEdgeWheelEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeWheelEvents")) "Test"

            Expect.stringContains dynamicMembers "enableEdgeWheelEvents:" $"EnableEdgeWheelEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isTrue value $"The EnableEdgeWheelEvents function was not set to true as expected, instead it was set to {value}"
        testCase "EnableEdgeWheelEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeWheelEvents")) "Test"

            Expect.stringContains dynamicMembers "enableEdgeWheelEvents:" $"EnableEdgeWheelEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isFalse value $"The EnableEdgeWheelEvents function was not set to false as expected, instead it was set to {value}"
        testCase "EnableEdgeHoverEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeHoverEvents")) "Test"

            Expect.stringContains dynamicMembers "enableEdgeHoverEvents:" $"EnableEdgeHoverEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isTrue value $"The EnableEdgeHoverEvents function was not set to true as expected, instead it was set to {value}"
        testCase "EnableEdgeHoverEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("enableEdgeHoverEvents")) "Test"

            Expect.stringContains dynamicMembers "enableEdgeHoverEvents:" $"EnableEdgeHoverEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isFalse value $"The EnableEdgeHoverEvents function was not set to false as expected, instead it was set to {value}"
        testCase "DefaultNodeColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeColor = "#D20103"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let color = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("defaultNodeColor")) "Test"

            Expect.stringContains dynamicMembers "defaultNodeColor:" $"The defaultNodeColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}" 
            Expect.equal color "#D20103" $"The DefaultNodeColor was not set to #D20103 as expected. Instead it was set to {color} ."
        testCase "DefaultNodeType_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeType = StyleParam.NodeType.Circle))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let ricardaLang = Expect.wantSome (graph.Settings.TryGetValue("defaultNodeType")) "Test"

            Expect.stringContains dynamicMembers "defaultNodeType:" $"The DefaultNodeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal ricardaLang "circle" $"The defaultNodeType was not set to Circle as expected, instead it was set to {ricardaLang}"
        testCase "DefaultEdgeColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeColor = "#D20103"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let color = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("defaultEdgeColor")) "Test"

            Expect.stringContains dynamicMembers "defaultEdgeColor:" $"The defaultNodeColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}" 
            Expect.equal color "#D20103" $"The DefaultEdgeColor was not set to #D20103 as expected. Instead it was set to {color} ."
        testCase "DefaultEdgeTypeLine_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Line))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let napoleonsInfanterie = Expect.wantSome (graph.Settings.TryGetValue("defaultEdgeType")) "Test"

            Expect.stringContains dynamicMembers "defaultEdgeType:" $"The DefaultEdgeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal napoleonsInfanterie "line" $"The defaultEdgeType was not set to line as expected, instead it was set to {napoleonsInfanterie}"
        testCase "DefaultEdgeTypeArrow_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Arrow))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let bowAnd = Expect.wantSome (graph.Settings.TryGetValue("defaultEdgeType")) "Test"

            Expect.stringContains dynamicMembers "defaultEdgeType:" $"The DefaultEdgeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal bowAnd "arrow" $"The defaultEdgeType was not set to arrow as expected, instead it was set to {bowAnd}"
        testCase "LabelFont_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelFont = "Arial"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let font = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("labelFont")) "Test"

            Expect.stringContains dynamicMembers "labelFont:" $"The LabelFont was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal font "Arial" $"The Labelfont was not set to Arial as expected. It was set to {font} instead"
        testCase "EdgeLabelFont_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelFont = "Arial"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let font = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("edgeLabelFont")) "Test"

            Expect.stringContains dynamicMembers "edgeLabelFont:" $"The EdgeLabelFont was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal font "Arial" $"The EdgeLabelfont was not set to Arial as expected. It was set to {font} instead"
        testCase "EdgeLabelSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelSize = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let size = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("edgeLabelSize")) "Test"

            Expect.stringContains dynamicMembers "edgeLabelSize:" $"The EdgeLabelSize was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal size 20 $"The size was not set to 20 as expected instead it was set to {size} ."
        testCase "LabelWeight_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelWeight = "Bold"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let weight = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("labelWeight")) "Test"

            Expect.stringContains dynamicMembers "labelWeight:" $"The LabelWeight was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal weight "Bold" $"The Labelweight was not set to Bold it was instead set to {weight}. "
        testCase "EdgeLabelWeight_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelWeight = "Normal"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let weight = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("edgeLabelWeight")) "Test"

            Expect.stringContains dynamicMembers "edgeLabelWeight" $"The EdgeLabelWeight was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal weight "Normal" $"The EdgeLabelWeight was not set to normal correctly, instead it was set to {weight} ."
        testCase "LabelColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelColor = Render.ColorOrReference.Init("#ffd700")))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let colorType = (Expect.wantSome (graph.Settings.TryGetValue("labelColor")) "Test").ToString()

            Expect.stringContains dynamicMembers "labelColor:" $"The Labelcolor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal colorType "Sigma.NET.Render+ColorOrReference" $"The Labelcolor was not set correctly. The Color was suppossed to be of the type -Sigma.NET.Render+ColorOrReference- but it is of the type {colorType}"
        testCase "EdgeLabelColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelColor = Render.ColorOrReference.Init("#ffd700")))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let colorType = (Expect.wantSome (graph.Settings.TryGetValue("edgeLabelColor")) "Test").ToString()

            Expect.stringContains dynamicMembers "edgeLabelColor:" $"The EdgeLabelColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal colorType "Sigma.NET.Render+ColorOrReference" $"The EdgeLabelcolor was not set correctly. The Color was suppossed to be of the type -Sigma.NET.Render+ColorOrReference- but it is of the type {colorType}"
        testCase "StagePadding_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(StagePadding = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let paddingValue = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("stagePadding")) "Test"

            Expect.stringContains dynamicMembers "stagePadding: 20" $"The StagePadding was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal paddingValue 20 $"The stagepadding Value was expected to be 20 but it is {paddingValue} ."
        testCase "LabelDensity_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelDensity = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let density = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("labelDensity")) "Test"

            Expect.stringContains dynamicMembers "labelDensity:" $"The LabelDensity was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal density 20 $"The LabelDensity was expected to be 20 but it is {density} ."
        testCase "LabelGridCellSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelGridCellSize = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let gridSize = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("labelGridCellSize")) "Test"


            Expect.stringContains dynamicMembers "labelGridCellSize:" $"The LabelGridCellSize was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal gridSize 20 $"The LabelGridCellSize was expected to be 20 but it is {gridSize} ."
        testCase "LabelRenderedSizeThreshold_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderedSizeThreshold = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let sizeThresh = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("labelRenderedSizeThreshold")) "Test"

            Expect.stringContains dynamicMembers "labelRenderedSizeThreshold:" $"The LabelRenderedSizeThreshold was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal sizeThresh 20 $"The LabelRenderedSizeThreshold was expected to be set to 20 but it was set to {sizeThresh} ."
        testCase "ZIndexTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let zI = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("zIndex")) "Test"

            Expect.stringContains dynamicMembers "zIndex:" $"The ZIndex was not set  as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue zI $"The Zindex was expected to be set to true but instead it was set to {zI} ."
        testCase "ZIndexFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let zI = Expect.wantSome (graph.Settings.TryGetTypedValue<bool>("zIndex")) "Test"

            Expect.stringContains dynamicMembers "zIndex:" $"The ZIndex was not set  as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse zI $"The Zindex was expected to be set to false but instead it was set to {zI} ."
        testCase "MinCameraRatio_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(MinCameraRatio = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let ratio = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("minCameraRatio")) "Test"

            Expect.stringContains dynamicMembers "minCameraRatio:" $"The MinCameraRatio was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal ratio 20 $"The MinCameraRatio was expected to be 20 but it is {ratio} ."
        testCase "MaxCameraRatio_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(MaxCameraRatio = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let ratio = Expect.wantSome (graph.Settings.TryGetTypedValue<int>("maxCameraRatio")) "Test"

            Expect.stringContains dynamicMembers "maxCameraRatio:" $"The MaxCameraRatio was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal ratio 20 $"The MaxCameraRatio was expected to be 20 but it is {ratio} ."
        testCase "LabelRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderer = "customLabelRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("labelRenderer")) "Test"

            Expect.stringContains dynamicMembers "labelRenderer:" $"The LabelRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customLabelRenderer" $"The LabelRenderer was expected to be set to 'customLabelRenderer' but it was set to {renderer} ."
        testCase "EdgeLabelRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelRenderer = "customEdgeLabelRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("edgeLabelRenderer")) "Test"

            Expect.stringContains dynamicMembers "edgeLabelRenderer:" $"The EdgeLabelRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customEdgeLabelRenderer" $"The EdgeLabelRenderer was expected to be 'customEdgeLabelRenderer' but it is {renderer} ."
        testCase "HoverRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HoverRenderer = "customHoverRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedValue<string>("hoverRenderer")) "Test"

            Expect.stringContains dynamicMembers "hoverRenderer:" $"The HoverRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customHoverRenderer" $"The HoverRenderer is expected to be 'customHoverRenderer' but it is {renderer} ."

    ]

