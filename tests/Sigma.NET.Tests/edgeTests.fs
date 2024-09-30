module EdgeTests

open Expecto
open DynamicObj
open Sigma.NET
open Sigma.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting



[<Tests>]
let edgeTests =
    testList "Edges" [
        let sourceNode = Node.Init("sourceNode")
        let targetNode = Node.Init("targetNode")
        let edge = Edge.Init("sourceNode","targetNode","testEdge", DisplayData.Init())
        testCase "Source_Test_E" <| fun () ->
            let sourceNodeKey = Expect.wantSome (sourceNode.TryGetTypedValue<string>("key")) "The Sourcenode does not have a key."
            let sourceNodeKeyInEdge = Expect.wantSome (edge.TryGetTypedValue<string>("source")) "The edge does not have a Source."
            
            Expect.equal sourceNodeKeyInEdge sourceNodeKey $"The SourceNode was expected to be {sourceNodeKey} but is {sourceNodeKeyInEdge}"
        testCase "Target_Test_E" <| fun () ->
            let targetNodeKey = Expect.wantSome (targetNode.TryGetTypedValue<string>("key")) "The node does not have a key."
            let targetNodeKeyInEdge = Expect.wantSome (edge.TryGetTypedValue<string>("target")) "The edge does not have a target"

            Expect.equal targetNodeKeyInEdge targetNodeKey $"The target Node was expected to be {targetNodeKey} but is {targetNodeKeyInEdge}" 
        testCase "Key_Test_E" <| fun () ->
            let edgeKey = Expect.wantSome (edge.TryGetTypedValue<string>("key")) "The edge does not have a key"

            Expect.equal edgeKey "testEdge" $"The Key of the edge should be -testEdge- but it is {edgeKey}" 
        testCase "DisplayData_Test_E" <| fun () ->
            let edgeDDType = ((edge.TryGetTypedValue<DisplayData>("attributes")).Value).GetType()
            
            Expect.equal edgeDDType typeof<DisplayData> $"There was added either no DisplayData at all or the added DisplayData is not of the type -DisplayData- . The Type that was added is {edgeDDType} "
                                    
    ]
