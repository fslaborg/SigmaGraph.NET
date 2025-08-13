module RenderSettingsTest

open Expecto
open DynamicObj
open SigmaGraph.NET
open SigmaGraph.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting



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
            let valueOfLabelSize = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("labelSize")) "The graph does not have a labelsize."

            Expect.stringContains dynamicMembers "labelSize:" $"The label size was not set as a Dynamic Member of the Graph.Settings correctly.{dynamicMembers}"
            Expect.equal valueOfLabelSize 20 $"The lablesize was set to the wrong number. The labelsize should be 20 but it is {valueOfLabelSize}."
        testCase "HideEdgesOnMoveTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("hideEdgesOnMove")) "The graph does not have a hideEdgesOnMove value."

            Expect.stringContains dynamicMembers "hideEdgesOnMove:" $"HideEdgesOnMove was not set as a dynamic member of Graph.Settings correctly.{dynamicMembers}"    
            Expect.isTrue value $"The HideEdgesOnMove function was not set to true as expected, instead it was set to {value}"
        testCase "HideEdgesOnMoveFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1"); Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "Edge1"))
                |> VisGraph.withRenderer(Render.Settings.Init(HideEdgesOnMove = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("hideEdgesOnMove")) "The graph does not have a hideEdgesOnMove value."
            Expect.stringContains dynamicMembers "hideEdgesOnMove:" $"HideEdgesOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The HideEdgesOnMove function was not set to false as expected, instead it was set to {value}"
        testCase "HideLabelsOnMoveTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("hideLabelsOnMove")) "The graph does not have a hideLabelsOnMove value."

            Expect.stringContains dynamicMembers "hideLabelsOnMove:" $"HideLabelsOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The HideLabelsOnMove function was not set to true as expected, instead it was set to {value}"
        testCase "HideLabelsOnMoveFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HideLabelsOnMove = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("hideLabelsOnMove")) "The graph does not have a hideLabelsOnMove value."

            Expect.stringContains dynamicMembers "hideLabelsOnMove:" $"HideLabelsOnMove was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The HideLabelsOnMove function was not set to false as expected, instead it was set to {value}"
        testCase "RenderLabelsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("renderLabels")) "The graph does not have renderLabels value."

            Expect.stringContains dynamicMembers "renderLabels:" $"renderLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The renderLabels function was not set to true as expected, instead it was set to {value}"
        testCase "RenderLabelsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderLabels = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("renderLabels")) "The graph does not have a renderLabels value."

            Expect.stringContains dynamicMembers "renderLabels:" $"renderLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The renderLabels function was not set to false as expected, instead it was set to {value}"
        testCase "RenderEdgeLabelsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("renderEdgeLabels")) "The graph does not have a renderEdgeLabels value."

            Expect.stringContains dynamicMembers "renderEdgeLabels:" $"RenderEdgeLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue value $"The renderEdgeLabels function was not set to true as expected, instead it was set to {value}"
        testCase "RenderEdgeLabelsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(RenderEdgeLabels = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("renderEdgeLabels")) "The graph does not have a renderEdgeLabels value."

            Expect.stringContains dynamicMembers "renderEdgeLabels:" $"RenderEdgeLabels was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse value $"The renderEdgeLabels function was not set to false as expected, instead it was set to {value}"
        testCase "EnableEdgeClickEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeClickEvents")) "The graph does not have a enableEdgeClickEvents value."

            Expect.isTrue value $"The enableEdgeClickEvents function was not set to true as expected, instead it was set to {value}"
            Expect.stringContains dynamicMembers "enableEdgeClickEvents:" $"EnableEdgeClickEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
        testCase "EnableEdgeClickEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeClickEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeClickEvents")) "The graph does not have a enableEdgeClickEvents value."

            Expect.isFalse value $"The enableEdgeClickEvents function was not set to false as expected, instead it was set to {value}"
            Expect.stringContains dynamicMembers "enableEdgeClickEvents:" $"EnableEdgeClickEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"    
        testCase "EnableEdgeWheelEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeWheelEvents")) "The graph does not have a enableEdgeWheelEvents value."

            Expect.stringContains dynamicMembers "enableEdgeWheelEvents:" $"EnableEdgeWheelEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isTrue value $"The EnableEdgeWheelEvents function was not set to true as expected, instead it was set to {value}"
        testCase "EnableEdgeWheelEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeWheelEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeWheelEvents")) "The graph has no enableEdgeWheelEvents value."

            Expect.stringContains dynamicMembers "enableEdgeWheelEvents:" $"EnableEdgeWheelEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isFalse value $"The EnableEdgeWheelEvents function was not set to false as expected, instead it was set to {value}"
        testCase "EnableEdgeHoverEventsTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeHoverEvents")) "The graph has no enableEdgeHoverEvents value."

            Expect.stringContains dynamicMembers "enableEdgeHoverEvents:" $"EnableEdgeHoverEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isTrue value $"The EnableEdgeHoverEvents function was not set to true as expected, instead it was set to {value}"
        testCase "EnableEdgeHoverEventsFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EnableEdgeHoverEvents = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("enableEdgeHoverEvents")) "The graph has no enableEdgeHoverEvents value."

            Expect.stringContains dynamicMembers "enableEdgeHoverEvents:" $"EnableEdgeHoverEvents was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"        
            Expect.isFalse value $"The EnableEdgeHoverEvents function was not set to false as expected, instead it was set to {value}"
        testCase "DefaultNodeColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeColor = "#D20103"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let color = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("defaultNodeColor")) "The graph has no defaultNodeColor value."

            Expect.stringContains dynamicMembers "defaultNodeColor:" $"The defaultNodeColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}" 
            Expect.equal color "#D20103" $"The DefaultNodeColor was not set to #D20103 as expected. Instead it was set to {color} ."
        testCase "DefaultNodeType_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultNodeType = StyleParam.NodeType.Circle))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let value = Expect.wantSome (graph.Settings.TryGetPropertyValue("defaultNodeType")) "The graph has no defaultNodeType value."

            Expect.stringContains dynamicMembers "defaultNodeType:" $"The DefaultNodeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal value "circle" $"The defaultNodeType was not set to Circle as expected, instead it was set to {value}"
        testCase "DefaultEdgeColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeColor = "#D20103"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let color = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("defaultEdgeColor")) "The graph has no defaultEdgeColor value."

            Expect.stringContains dynamicMembers "defaultEdgeColor:" $"The defaultNodeColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}" 
            Expect.equal color "#D20103" $"The DefaultEdgeColor was not set to #D20103 as expected. Instead it was set to {color} ."
        testCase "DefaultEdgeTypeLine_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Line))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let napoleonsInfanterie = Expect.wantSome (graph.Settings.TryGetPropertyValue("defaultEdgeType")) "The graph has no defaultEdgeType value."

            Expect.stringContains dynamicMembers "defaultEdgeType:" $"The DefaultEdgeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal napoleonsInfanterie "line" $"The defaultEdgeType was not set to line as expected, instead it was set to {napoleonsInfanterie}"
        testCase "DefaultEdgeTypeArrow_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(DefaultEdgeType = StyleParam.EdgeType.Arrow))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let bowAnd = Expect.wantSome (graph.Settings.TryGetPropertyValue("defaultEdgeType")) "The graph has no defaultEdgeType value."

            Expect.stringContains dynamicMembers "defaultEdgeType:" $"The DefaultEdgeType was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal bowAnd "arrow" $"The defaultEdgeType was not set to arrow as expected, instead it was set to {bowAnd}"
        testCase "LabelFont_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelFont = "Arial"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let font = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("labelFont")) "The graph has no labelFont value."

            Expect.stringContains dynamicMembers "labelFont:" $"The LabelFont was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal font "Arial" $"The Labelfont was not set to Arial as expected. It was set to {font} instead"
        testCase "EdgeLabelFont_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelFont = "Arial"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let font = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("edgeLabelFont")) "The graph has no edgeLabelFont value."

            Expect.stringContains dynamicMembers "edgeLabelFont:" $"The EdgeLabelFont was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal font "Arial" $"The EdgeLabelfont was not set to Arial as expected. It was set to {font} instead"
        testCase "EdgeLabelSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelSize = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let size = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("edgeLabelSize")) "The graph has no edgeLabelSize value."

            Expect.stringContains dynamicMembers "edgeLabelSize:" $"The EdgeLabelSize was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal size 20 $"The size was not set to 20 as expected instead it was set to {size} ."
        testCase "LabelWeight_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelWeight = "Bold"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let weight = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("labelWeight")) "The graph has no labelWeight value."

            Expect.stringContains dynamicMembers "labelWeight:" $"The LabelWeight was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal weight "Bold" $"The Labelweight was not set to Bold it was instead set to {weight}. "
        testCase "EdgeLabelWeight_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelWeight = "Normal"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let weight = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("edgeLabelWeight")) "The graph has no edgeLabelWeight value."

            Expect.stringContains dynamicMembers "edgeLabelWeight" $"The EdgeLabelWeight was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal weight "Normal" $"The EdgeLabelWeight was not set to normal correctly, instead it was set to {weight} ."
        testCase "LabelColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelColor = Render.ColorOrReference.Init("#ffd700")))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let colorType = (Expect.wantSome (graph.Settings.TryGetPropertyValue("labelColor")) "Test").ToString()

            Expect.stringContains dynamicMembers "labelColor:" $"The Labelcolor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal colorType "SigmaGraph.NET.Render+ColorOrReference" $"The Labelcolor was not set correctly. The Color was suppossed to be of the type -SigmaGraph.NET.Render+ColorOrReference- but it is of the type {colorType}"
        testCase "EdgeLabelColor_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelColor = Render.ColorOrReference.Init("#ffd700")))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let colorType = (Expect.wantSome (graph.Settings.TryGetPropertyValue("edgeLabelColor")) "Test").ToString()

            Expect.stringContains dynamicMembers "edgeLabelColor:" $"The EdgeLabelColor was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal colorType "SigmaGraph.NET.Render+ColorOrReference" $"The EdgeLabelcolor was not set correctly. The Color was suppossed to be of the type -SigmaGraph.NET.Render+ColorOrReference- but it is of the type {colorType}"
        testCase "StagePadding_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(StagePadding = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let paddingValue = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("stagePadding")) "The graph has no stagePadding value."

            Expect.stringContains dynamicMembers "stagePadding: 20" $"The StagePadding was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal paddingValue 20 $"The stagepadding Value was expected to be 20 but it is {paddingValue} ."
        testCase "LabelDensity_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelDensity = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let density = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("labelDensity")) "The graph has no labelDensity value"

            Expect.stringContains dynamicMembers "labelDensity:" $"The LabelDensity was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal density 20 $"The LabelDensity was expected to be 20 but it is {density} ."
        testCase "LabelGridCellSize_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelGridCellSize = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let gridSize = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("labelGridCellSize")) "The graph has no labelGridCellSize value."


            Expect.stringContains dynamicMembers "labelGridCellSize:" $"The LabelGridCellSize was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal gridSize 20 $"The LabelGridCellSize was expected to be 20 but it is {gridSize} ."
        testCase "LabelRenderedSizeThreshold_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderedSizeThreshold = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let sizeThresh = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("labelRenderedSizeThreshold")) "The graph has no labelRenderedSizeThreshold value."

            Expect.stringContains dynamicMembers "labelRenderedSizeThreshold:" $"The LabelRenderedSizeThreshold was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal sizeThresh 20 $"The LabelRenderedSizeThreshold was expected to be set to 20 but it was set to {sizeThresh} ."
        testCase "ZIndexTrue_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = true))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let zI = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("zIndex")) "The graph has no zIndex value."

            Expect.stringContains dynamicMembers "zIndex:" $"The ZIndex was not set  as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isTrue zI $"The Zindex was expected to be set to true but instead it was set to {zI} ."
        testCase "ZIndexFalse_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(ZIndex = false))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let zI = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<bool>("zIndex")) "The graph has no zIndex value."

            Expect.stringContains dynamicMembers "zIndex:" $"The ZIndex was not set  as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.isFalse zI $"The Zindex was expected to be set to false but instead it was set to {zI} ."
        testCase "MinCameraRatio_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(MinCameraRatio = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let ratio = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("minCameraRatio")) "The graph has no minCameraRatio value."

            Expect.stringContains dynamicMembers "minCameraRatio:" $"The MinCameraRatio was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal ratio 20 $"The MinCameraRatio was expected to be 20 but it is {ratio} ."
        testCase "MaxCameraRatio_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(MaxCameraRatio = 20))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let ratio = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<int>("maxCameraRatio")) "Thegraph has no maxCameraRatio value."

            Expect.stringContains dynamicMembers "maxCameraRatio:" $"The MaxCameraRatio was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal ratio 20 $"The MaxCameraRatio was expected to be 20 but it is {ratio} ."
        testCase "LabelRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(LabelRenderer = "customLabelRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("labelRenderer")) "The graph has no labelRenderer value"

            Expect.stringContains dynamicMembers "labelRenderer:" $"The LabelRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customLabelRenderer" $"The LabelRenderer was expected to be set to 'customLabelRenderer' but it was set to {renderer} ."
        testCase "EdgeLabelRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(EdgeLabelRenderer = "customEdgeLabelRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("edgeLabelRenderer")) "The graph has no edgeLabelRenderer value."

            Expect.stringContains dynamicMembers "edgeLabelRenderer:" $"The EdgeLabelRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customEdgeLabelRenderer" $"The EdgeLabelRenderer was expected to be 'customEdgeLabelRenderer' but it is {renderer} ."
        testCase "HoverRenderer_Test" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRenderer(Render.Settings.Init(HoverRenderer = "customHoverRenderer"))
            let dynamicMembers = graph.Settings.GetDynamicMemberNames().ToDisplayString()
            let renderer = Expect.wantSome (graph.Settings.TryGetTypedPropertyValue<string>("hoverRenderer")) "The graph has no hoverRenderer value."

            Expect.stringContains dynamicMembers "hoverRenderer:" $"The HoverRenderer was not set as a dynamic member of Graph.Settings correctly. {dynamicMembers}"
            Expect.equal renderer "customHoverRenderer" $"The HoverRenderer is expected to be 'customHoverRenderer' but it is {renderer} ."

    ]

