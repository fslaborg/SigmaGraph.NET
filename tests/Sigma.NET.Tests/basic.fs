module BasicTests

open Expecto
open DynamicObj
//open helperFunctions

[<Tests>]
let creation =
    testList "graph add" [
        testCase "node" <| fun () ->
            
            Expect.isTrue false "test"
    ]