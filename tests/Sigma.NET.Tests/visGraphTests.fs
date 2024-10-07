module VisGraphTests

open Expecto
open DynamicObj
open Sigma.NET
open Sigma.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting



[<Tests>]
let visGraphTests =
    testList "VisGraph" [
        testCase "Type_Test_V" <| fun () ->
            let x = (VisGraph.empty().GetType())
            let y = typeof<SigmaGraph>

            Expect.equal x y $"The type of the Visgraph was expected to be {y} but it is {x}"
        testCase "Empty_Test_V" <| fun () ->
            
            Expect.isTrue ((VisGraph.empty().GraphData.Nodes.Count = 0 && VisGraph.empty().GraphData.Edges.Count = 0)) "The initialized graph should be empty but it isnt."
        testCase "WithNode_Test_V" <| fun () ->
            let graph = VisGraph.withNode (Node.Init("1")) (VisGraph.empty())
            let numberOfNodesAdded = graph.GraphData.Nodes.Count
            
            Expect.equal numberOfNodesAdded 1 $"It was expected that 1 Node gets added to the graph but {numberOfNodesAdded} Node/Nodes have been added to the Graph"
        testCase "WithNodes_Test_V" <| fun () ->
            let graph = VisGraph.withNodes ([Node.Init("1");Node.Init("2")]) (VisGraph.empty())
            let numberOfNodesAdded = graph.GraphData.Nodes.Count

            Expect.equal numberOfNodesAdded 2 $"It was expected that 2 Nodes are added to the graph but {numberOfNodesAdded} Nodes have been added to the graph"
        testCase "WithEdge_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNodes([Node.Init("1");Node.Init("2")])
                |> VisGraph.withEdge(Edge.Init("1", "2", "edge1"))
            let numberOfEdgesAdded = graph.GraphData.Edges.Count

            Expect.equal numberOfEdgesAdded 1 $"1 edge shouldve been added to the graph but {numberOfEdgesAdded} edges were added." 
        testCase "WithEdges_Test_V" <| fun () ->
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
        testCase "WithCircularLayout_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withCircularLayout()
            let layoutType = string(graph.Layout.GetType())

            Expect.equal layoutType "Sigma.NET.Layout+Circular" $"The Layout type was expected to be -Sigma.NET.Layout+Circular- but it is {layoutType}"  
        testCase "WithRandomLayout_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withRandomLayout()
            let layoutType = string(graph.Layout.GetType())

            Expect.equal layoutType  "Sigma.NET.Layout+Random" $"The layout was expected to be -Sigma.NET.Layout+Random- but it is {layoutType}"    

        testCase "WithHoverSelectorTrue_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withHoverSelector(true)
            let widgedCount = graph.Widgets.Count
            Expect.equal widgedCount 2 $"The Hoverselector was not set to true correctly. If the Hoverselector is set to true the widgedCount should be 2, if it is set to false the widgedCount is 1. The actual widgedcount is {widgedCount} ."
        testCase "WithHoverSelectorFalse_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withHoverSelector(false)
            let widgedCount = graph.Widgets.Count
            Expect.equal widgedCount 1 $"The Hoverselector was not set to false correctly.If the Hoverselector is set to true the widgedCount should be 2, if it is set to false the widgedCount is 1. The actual widgedcount is {widgedCount} . "
            
        testCase "WithForceAtlas2_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withForceAtlas2(1)
            let appliedLayout = graph.Layout.ToString()

            Expect.equal appliedLayout "FA2 Sigma.NET.FA2Options" $"The Layout was expected to be -FA2 Sigma.NET.FA2Options- but is {appliedLayout}"
        testCase "WithNoverlap_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withNoverlap(1)
            let appliedLayout = graph.Layout.ToString()

            Expect.equal appliedLayout "Noverlap Sigma.NET.NoverlapOptions" $"The Layout was expected to be -Noverlap Sigma.NET.NoverlapOptions- but it is {appliedLayout}."
        testCase "WithSize_Test_V" <| fun () ->
            let graph =
                VisGraph.empty()
                |> VisGraph.withSize(CssLength.Percent(80), CssLength.Percent(80))
            let width = graph.Width.ToString()
            let height = graph.Height.ToString()

            Expect.equal width "Percent 80" $"The width was expected to be -Percent 80- but it is {width}"
            Expect.equal height "Percent 80" $"The height was expected to be -Percent 80- but it is {height}" 

    ]

