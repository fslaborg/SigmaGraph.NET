namespace Sigma.NET

module StyleParam =
    /// Defines different types of edge styles for graph visualization.
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
        /// Converts the edge type to a corresponding string representation.
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

    /// Defines different types of node shapes for graph visualization.
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
        /// Converts the node type to a corresponding string representation.
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
            