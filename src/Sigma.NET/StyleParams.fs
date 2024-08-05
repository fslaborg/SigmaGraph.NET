namespace Sigma.NET

module StyleParam =

    /// Defines different types of edge styles for graph visualization.
    [<RequireQualifiedAccess>]
    type EdgeType = 
        | Line         // A simple line connecting nodes.
        | Arrow        // A line with an arrowhead.
        //| Curve        // A curved line connecting nodes.
        //| CurvedArrow  // A curved line with an arrowhead.
        //| Dashed       // A dashed line.
        //| Dotted       // A dotted line.
        //| Parallel     // Parallel lines.
        //| Tapered      // A tapered line.
        | Custom of string // A custom edge style specified by a string.

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
        | Circle       // A circular node shape.
        //| Triangle     // A triangular node shape.
        //| Square       // A square node shape.
        //| Pentagon     // A pentagon node shape.
        //| Star         // A star node shape.
        //| Hexagon      // A hexagonal node shape.
        //| Heart        // A heart-shaped node.
        //| Cloud        // A cloud-shaped node.
        | Custom of string // A custom node shape specified by a string.

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
