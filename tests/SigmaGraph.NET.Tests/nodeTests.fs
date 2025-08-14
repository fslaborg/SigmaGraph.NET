module NodeTests

open Expecto
open DynamicObj
open SigmaGraph.NET
open SigmaGraph.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting


[<Tests>]
let nodeTests =
    testList "Nodes" [
        let node = Node.Init("CorrectKey", DisplayData.Init())
        testCase "Key_Test_N" <| fun () ->
            let keytest = Expect.wantSome (node.TryGetTypedPropertyValue<string>("key")) $"The node does not have a key."
            Expect.equal keytest "CorrectKey" $"The Key of the Node was not set correctly. The Key should be called -CorrectKey- but it is {keytest} ."

        testCase "Type_Test_N" <| fun () ->
            let nodeType = node.GetType()

            Expect.equal nodeType typeof<Node> $"The type of the node is expected to be -SigmaGraph.NET.Node- but is {node.GetType()}"

        testCase "DisplayData_Test_N" <| fun () ->
            let displayDataType = ((node.TryGetTypedPropertyValue<DisplayData>("attributes")).Value).GetType()

            Expect.equal displayDataType typeof<DisplayData> $"There was added either no DisplayData at all or the added DisplayData is not of the type -DisplayData- . The Type that was added is {displayDataType} "

    ]



