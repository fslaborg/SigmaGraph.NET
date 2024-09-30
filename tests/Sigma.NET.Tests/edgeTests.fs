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

[<Tests>]
let edgeDisplayDataTests =
    testList "EdgeDisplayData" [
        let edge = Edge.Init("1", "2", "edgeKey", DisplayData.Init(Label = "edgeLabel",Size = 20,Color = "#1910e3",ZIndex = 20, StyleType = "default",X = 10, Y=30))
        let edgeAsString = DynObj.format edge
        let edgeWithBoolsTrue = Edge.Init("3", "4", "key1", DisplayData.Init(Hidden = true, ForceLabel = true))
        let edgeWithBoolsTrueAsString = DynObj.format edgeWithBoolsTrue
        let edgeWithBoolsFalse = Edge.Init("5", "6", "key2", DisplayData.Init(Hidden = false, ForceLabel = false))
        let edgeWithBoolsFalseAsString = DynObj.format edgeWithBoolsFalse
        testCase "Label_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "label:" $"Label was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "edgeLabel" $"The added Label was expected to be -edgeLabel- but it is {edgeAsString} ."
        testCase "Size_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "size:" $"Size was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "20" $"The added Size was expected to be -20- but it is {edgeAsString} ."
        testCase "Color_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "color" $"Color was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "#1910e3" $"The added Color was expected to be -#1910e3- but it is {edgeAsString} ."
        testCase "ZIndex_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "zIndex" $"ZIndex was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "20" $"The added ZIndex was expected to be -20- but it is {edgeAsString} ."
        testCase "StyleType_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "type" $"StyleType was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "default" $"The added StyleType was expected to be -default- but it is {edgeAsString} ."
        testCase "X_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "x" $"X was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "10" $"The added X-value was expected to be -10- but it is {edgeAsString} ."
        testCase "Y_Test_E" <| fun () ->
            Expect.stringContains edgeAsString "y" $"Y was not added as a DisplayOption of the edge. {edgeAsString}"
            Expect.stringContains edgeAsString "30" $"The added Y-value was expected to be -30- but it is {edgeAsString} ."
        testCase "HiddenTrue_Test_E" <| fun () ->
            Expect.stringContains edgeWithBoolsTrueAsString "hidden" $"Hidden was not added as a DisplayOption of the edge. {edgeWithBoolsTrueAsString}"
            Expect.stringContains edgeWithBoolsTrueAsString "True" $"Hidden was expected to be true but it is {edgeWithBoolsTrueAsString} ."
        testCase "ForceLabelTrue_Test_E" <| fun () ->
            Expect.stringContains edgeWithBoolsTrueAsString "forceLabel" $"ForceLabel was not added as a DisplayOption of the edge. {edgeWithBoolsTrueAsString}"
            Expect.stringContains edgeWithBoolsTrueAsString "True" $"ForceLabel was expected to be true but it is {edgeWithBoolsTrueAsString} ."
        testCase "HiddenFalse_Test_E" <| fun () ->
            Expect.stringContains edgeWithBoolsFalseAsString "hidden" $"Hidden was not added as a DisplayOption of the edge. {edgeWithBoolsFalseAsString}"
            Expect.stringContains edgeWithBoolsFalseAsString "False" $"Hidden was expected to be true but it is {edgeWithBoolsFalseAsString} ."
        testCase "ForceLabelFalse_Test_E" <| fun () ->
            Expect.stringContains edgeWithBoolsFalseAsString "forceLabel" $"ForceLabel was not added as a DisplayOption of the edge. {edgeWithBoolsFalseAsString}"
            Expect.stringContains edgeWithBoolsFalseAsString "False" $"ForceLabel was expected to be true but it is {edgeWithBoolsFalseAsString} ."

    ]