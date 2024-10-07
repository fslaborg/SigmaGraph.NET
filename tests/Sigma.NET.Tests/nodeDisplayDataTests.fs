module NodeDisplayDataTests

open Expecto
open DynamicObj
open Sigma.NET
open Sigma.NET.Interactive
open Giraffe.ViewEngine
open Microsoft.DotNet.Interactive.Formatting



[<Tests>]
let nodeDisplayDataTests =
    testList "NodeDisplayData" [
        
        testCase "Label_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Label = "Node1"))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?label: Node1" $"The Label was not set to -Node1- correctly. {nodeasString}"
        testCase "Size_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Size = 10))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?size: 10" $"The Size was not set to 10 correctly. {nodeasString}"
        testCase "Color_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Color = "#0070b8"))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?color: #0070b8" $"The Color was not set to -#0070b8- correctly. {nodeasString}"
        testCase "HiddenTrue_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Hidden = true))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?hidden: True" $"The -Hidden- function was not set to true correctly. {nodeasString}"
        testCase "HiddenFalse_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(Hidden = false))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?hidden: False" $"The -Hidden- function was not set to false correctly. {nodeasString}"
        testCase "ForceLabelTrue_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ForceLabel = true))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?forceLabel: True" $"The -ForceLabel- function was not set to true correctly. {nodeasString}"
        testCase "ForceLabelFalse_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ForceLabel = false))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?forceLabel: False" $"The -ForceLabel- function was not set to false correctly. {nodeasString}"
        testCase "ZIndex_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(ZIndex = 10))
            let nodeasString = DynObj.format node
            
            Expect.stringContains nodeasString "?zIndex: 10" $"The ZIndex was not set to 10 correctly. {nodeasString}"
        testCase "Styletype_Test_N" <| fun () ->
            let node = Node.Init("1", DisplayData.Init(StyleType = "default"))
            let nodeasString = DynObj.format node

            Expect.stringContains nodeasString "?type: default" $"The Styletype was not set to default correctly. {nodeasString}"
                    
    ]

