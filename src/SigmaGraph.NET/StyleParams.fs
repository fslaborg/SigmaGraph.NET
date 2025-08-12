namespace SigmaGraph.NET

module StyleParam =

    /// <summary>Defines different types of edge styles for graph visualization.</summary>
    [<RequireQualifiedAccess>]
    type EdgeType = 
        | Line
        | Arrow        
        //| Curve                
        //| CurvedArrow
        //| Dashed
        //| Dotted
        //| Parallel
        //| Tapered
        | Custom of string

        /// <summary>Converts the edge type to a corresponding string representation.</summary>
        static member toString =
            function            
            | Line -> "line"
            | Arrow -> "arrow"
            //| Curve -> "curve"
            //| CurvedArrow -> "curvedArrow"
            //| Dashed -> "dashed"
            //| Dotted -> "dotted"
            //| Parallel -> "parallel"
            //| Tapered -> "tapered"
            | Custom str -> str 

    /// <summary>Defines different types of node shapes for graph visualization.</summary>
    [<RequireQualifiedAccess>]
    type NodeType = 
        | Circle
        //| Triangle
        //| Square
        //| Pentagon
        //| Star
        //| Hexagon
        //| Heart
        //| Cloud
        | Custom of string

        /// <summary>Converts the node type to a corresponding string representation.</summary>
        static member toString =
            function
            | Circle -> "circle"
            //| Triangle -> "triangle"
            //| Square -> "square"
            //| Pentagon -> "pentagon"
            //| Star -> "star"
            //| Hexagon -> "hexagon"
            //| Heart -> "heart"
            //| Cloud -> "cloud"
            | Custom str -> str            
            