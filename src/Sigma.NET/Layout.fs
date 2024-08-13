namespace Sigma.NET

open DynamicObj
open Newtonsoft.Json

// adjustSizes ?boolean false: should the node’s sizes be taken into account?
// barnesHutOptimize ?boolean false: whether to use the Barnes-Hut approximation to compute repulsion in O(n*log(n)) rather than default O(n^2), n being the number of nodes.
// barnesHutTheta ?number 0.5: Barnes-Hut approximation theta parameter.
// edgeWeightInfluence ?number 1: influence of the edge’s weights on the layout. To consider edge weight, don’t forget to pass weighted as true when applying the synchronous layout or when instantiating the worker.
// gravity ?number 1: strength of the layout’s gravity.
// linLogMode ?boolean false: whether to use Noack’s LinLog model.
// outboundAttractionDistribution ?boolean false
// scalingRatio ?number 1
// slowDown ?number 1
// strongGravityMode ?boolean false
/// FA2Settings type for configuring the Force Atlas 2 algorithm settings
type FA2Settings() =
    inherit DynamicObj ()
    /// Initializes FA2Settings with optional parameters
    static member Init 
        (
            ?AdjustSizes : bool,
            ?BarnesHutOptimize : bool,
            ?BarnesHutTheta : float,
            ?EdgeWeightInfluence : int,
            ?Gravity : int,
            ?LinLogMode : bool,
            ?OutboundAttractionDistribution : bool,
            ?ScalingRatio : int,
            ?SlowDown : int,
            ?StrongGravityMode : bool
        ) =    
            FA2Settings()
            |> FA2Settings.Style
                (
                    ?AdjustSizes = AdjustSizes,
                    ?BarnesHutOptimize = BarnesHutOptimize,
                    ?BarnesHutTheta = BarnesHutTheta,
                    ?EdgeWeightInfluence = EdgeWeightInfluence,
                    ?Gravity = Gravity,
                    ?LinLogMode = LinLogMode,
                    ?OutboundAttractionDistribution = OutboundAttractionDistribution,
                    ?ScalingRatio = ScalingRatio,
                    ?SlowDown = SlowDown,
                    ?StrongGravityMode = StrongGravityMode
                                    )

        // Applies updates to FA2Settings()
    /// Applies updates to FA2Settings instance based on the optional parameters
    static member Style
        (  
            ?AdjustSizes,
            ?BarnesHutOptimize,
            ?BarnesHutTheta,
            ?EdgeWeightInfluence,
            ?Gravity,
            ?LinLogMode,
            ?OutboundAttractionDistribution,
            ?ScalingRatio,  
            ?SlowDown,
            ?StrongGravityMode
        ) =
            (fun (opt:FA2Settings) -> 
            
                AdjustSizes        |> DynObj.setValueOpt opt "adjustSizes"
                BarnesHutOptimize  |> DynObj.setValueOpt opt "barnesHutOptimize"
                BarnesHutTheta     |> DynObj.setValueOpt opt "barnesHutTheta"
                EdgeWeightInfluence|> DynObj.setValueOpt opt "edgeWeightInfluence"
                Gravity            |> DynObj.setValueOpt opt "gravity"
                LinLogMode         |> DynObj.setValueOpt opt "linLogMode"
                OutboundAttractionDistribution |> DynObj.setValueOpt opt "outboundAttractionDistribution"
                ScalingRatio       |> DynObj.setValueOpt opt "scalingRatio"
                SlowDown           |> DynObj.setValueOpt opt "slowDown"
                StrongGravityMode  |> DynObj.setValueOpt opt "strongGravityMode"
                // out ->
                opt
                
            )


//gridSize ?number 20: number of grid cells horizontally and vertically subdivising the graph’s space. This is used as an optimization scheme. Set it to 1 and you will have O(n²) time complexity, which can sometimes perform better with very few nodes.
//margin ?number 5: margin to keep between nodes.
//expansion ?number 1.1: percentage of current space that nodes could attempt to move outside of.
//ratio ?number 1.0: ratio scaling node sizes.
//speed ?number 3: dampening factor that will slow down node movements to ease the overall process.
/// NoverlapSettings type for configuring the Noverlap algorithm settings
type NoverlapSettings() =
    inherit DynamicObj ()
    /// Initializes NoverlapSettings with optional parameters
    static member Init 
        (
            ?GridSize : int,
            ?Margin : int,
            ?Expansion : float,
            ?Ratio : float,
            ?Speed : int
        ) =    
            NoverlapSettings()
            |> NoverlapSettings.Style
                (
                    ?GridSize = GridSize,
                    ?Margin = Margin,
                    ?Expansion = Expansion,
                    ?Ratio = Ratio,
                    ?Speed = Speed
                )

        // Applies updates to NoverlapSettings()
    /// Applies updates to NoverlapSettings instance based on the optional parameters    
    static member Style
        (    
            ?GridSize,
            ?Margin,
            ?Expansion,
            ?Ratio,
            ?Speed
        ) =
            (fun (opt:NoverlapSettings) -> 

                GridSize        |> DynObj.setValueOpt opt "gridSize"
                Margin          |> DynObj.setValueOpt opt "margin"
                Expansion       |> DynObj.setValueOpt opt "expansion"
                Ratio           |> DynObj.setValueOpt opt "ratio"
                Speed           |> DynObj.setValueOpt opt "speed"
                // out ->
                opt
                
            )   


//iterations number: number of iterations to perform.
//getEdgeWeight ?string|function weight: name of the edge weight attribute or getter function. Defaults to weight.
//settings ?object: the layout’s settings (see #settings).
/// FA2Options type for configuring the Force Atlas 2 layout options
type FA2Options() =
    inherit DynamicObj ()
    /// Initializes FA2Options with optional parameters
    static member Init 
        (
            ?Iterations : int,
            ?GetEdgeWeight : string,
            ?Settings : FA2Settings
        ) =    
            FA2Options()
            |> FA2Options.Style
                (
                    ?Iterations = Iterations,
                    ?GetEdgeWeight = GetEdgeWeight,
                    ?Settings = Settings
                )

        // Applies updates to FA2Options()
    /// Applies updates to FA2Options instance based on the optional parameters
    static member Style
        (    
            ?Iterations,
            ?GetEdgeWeight,
            ?Settings
        ) =
            (fun (opt:FA2Options) -> 

                Iterations        |> DynObj.setValueOpt opt "iterations"
                GetEdgeWeight     |> DynObj.setValueOpt opt "getEdgeWeight"
                Settings          |> DynObj.setValueOpt opt "settings"
                // out ->
                opt
                
            )   


//maxIterations ?number 500: maximum number of iterations to perform before stopping. Note that the algorithm will also stop as soon as converged.
//inputReducer ?function: a function reducing each node attributes. This can be useful if the rendered positions/sizes of your graph are stored outside of the graph’s data. This is the case when using sigma.js for instance.
//outputReducer ?function: a function reducing node positions as computed by the layout algorithm. This can be useful to map back to a previous coordinates system. This is the case when using sigma.js for instance.
//settings ?object: the layout’s settings (see #settings).
/// NoverlapOptions type for configuring the Noverlap layout options
type NoverlapOptions() =
    inherit DynamicObj ()
    /// Initializes NoverlapOptions with optional parameters
    static member Init 
        (
            ?MaxIterations : int,
            ?InputReducer : string,
            ?OutputReducer : string,
            ?Settings : NoverlapSettings
        ) =    
            NoverlapOptions()
            |> NoverlapOptions.Style
                (
                    ?MaxIterations = MaxIterations,
                    ?InputReducer = InputReducer,
                    ?OutputReducer = OutputReducer,
                    ?Settings = Settings
                )

        // Applies updates to NoverlapOptions()
    /// Applies updates to NoverlapOptions instance based on the optional parameters
    static member Style
        (    
            ?MaxIterations,
            ?InputReducer,
            ?OutputReducer,
            ?Settings
        ) =
            (fun (opt:NoverlapOptions) -> 

                MaxIterations        |> DynObj.setValueOpt opt "maxIterations"
                InputReducer         |> DynObj.setValueOpt opt "inputReducer"
                OutputReducer        |> DynObj.setValueOpt opt "outputReducer"
                Settings             |> DynObj.setValueOpt opt "settings"
                // out ->
                opt
                
            )   

//dimensions ?array [‘x’, ‘y’]: dimensions of the layout. Cannot work with dimensions != 2.
//center ?number 0.5: center of the layout.
//scale ?number 1: scale of the layout.
/// CircularOptions type for configuring the Circular layout options
type CircularOptions() =
    inherit DynamicObj ()
    /// Initializes CircularOptions with optional parameters
    static member Init 
        (
            ?Dimensions : string,
            ?Center : float,
            ?Scale : float
        ) =    
            CircularOptions()
            |> CircularOptions.Style
                (
                    ?Dimensions = Dimensions,
                    ?Center = Center,
                    ?Scale = Scale
                )

        // Applies updates to CircularOptions()
    /// Applies updates to CircularOptions instance based on the optional parameters    
    static member Style
        (    
            ?Dimensions,
            ?Center,
            ?Scale
        ) =
            (fun (opt:CircularOptions) -> 

                Dimensions        |> DynObj.setValueOpt opt "dimensions"
                Center            |> DynObj.setValueOpt opt "center"
                Scale             |> DynObj.setValueOpt opt "scale"
                // out ->
                opt
                
            )   
//dimensions ?array [‘x’, ‘y’]: dimensions of the layout.
//center ?number 0.5: center of the layout.
//rng ?function Math.random: custom RNG function to use.
//scale ?number 1: scale of the layout.
/// RandomOptions type for configuring the Random layout options
type RandomOptions() =
    inherit DynamicObj ()
    /// Initializes RandomOptions with optional parameters
    static member Init 
        (
            ?Dimensions : string,
            ?Center : float,
            ?Scale : int
        ) =    
            RandomOptions()
            |> RandomOptions.Style
                (
                    ?Dimensions = Dimensions,
                    ?Center = Center,
                    ?Scale = Scale
                )

        // Applies updates to RandomOptions()
    /// Applies updates to RandomOptions instance based on the optional parameters
    static member Style
        (    
            ?Dimensions,
            ?Center,
            ?Scale
        ) =
            (fun (opt:RandomOptions) -> 

                Dimensions        |> DynObj.setValueOpt opt "dimensions"
                Center            |> DynObj.setValueOpt opt "center"
                Scale             |> DynObj.setValueOpt opt "scale"
                // out ->
                opt
                
            )   

//dimensions ?array [‘x’, ‘y’]: dimensions to use for the rotation. Cannot work with dimensions != 2.
//degrees ?boolean false: whether the given angle is in degrees.
//centeredOnZero ?boolean false: whether to rotate the graph around 0, rather than the graph’s center.
/// RotationOptions contains options for configuring a Rotation layout algorithm.
type RotationOptions() =
    inherit DynamicObj ()
    /// Initializes an instance of RotationOptions with optional parameters.
    static member Init 
        (
            ?Dimensions : string,
            ?Degrees : bool,
            ?CenteredOnZero : bool
        ) =    
            RotationOptions()
            |> RotationOptions.Style
                (
                    ?Dimensions = Dimensions,
                    ?Degrees = Degrees,
                    ?CenteredOnZero = CenteredOnZero
                )

        // Applies updates to RotationOptions()
    /// Applies style updates to an instance of RotationOptions.    
    static member Style
        (    
            ?Dimensions,
            ?Degrees,
            ?CenteredOnZero
        ) =
            (fun (opt:RotationOptions) -> 

                Dimensions        |> DynObj.setValueOpt opt "dimensions"
                Degrees           |> DynObj.setValueOpt opt "degrees"
                CenteredOnZero    |> DynObj.setValueOpt opt "centeredOnZero"
                // out ->
                opt
                
            )   

// circlePack
//attributes ?object: attributes to map:
//x ?string x: name of the x position.
//y ?string y: name of the y position.
//center ?number 0: center of the layout.
//hierarchyAttributes ?list []: attributes used to group nodes.
//rng ?function Math.random: custom RNG function to use.
//scale ?number 1: scale of the layout.


// LayoutAlgorithmName
/// Layout defines different layout algorithms for a graph and serializes them into JavaScript commands.
type Layout =
    | FA2 of FA2Options
    | Noverlap of NoverlapOptions
    | Circular of CircularOptions
    | Random of RandomOptions
    | Rotation of RotationOptions
    with 
        /// Serializes the selected layout into a JavaScript command string.
        static member serialize (layout:Layout) =
            match layout with
            | FA2 opt ->
                let stringOpt = opt |> JsonConvert.SerializeObject 
                sprintf "graphologyLibrary.layout.random.assign(graph); graphologyLibrary.layoutForceAtlas2.assign(graph, %s);" stringOpt
            | Noverlap opt ->
                let stringOpt = opt |> JsonConvert.SerializeObject
                sprintf "graphologyLibrary.layout.random.assign(graph); graphologyLibrary.layoutNoverlap.assign(graph, %s);" stringOpt
            | Circular opt ->
                let stringOpt = opt |> JsonConvert.SerializeObject
                sprintf "graphologyLibrary.layout.circular.assign(graph, %s);" stringOpt
            | Random opt ->
                let stringOpt = opt |> JsonConvert.SerializeObject
                sprintf "graphologyLibrary.layout.random.assign(graph, %s);" stringOpt
            | Rotation opt ->
                let stringOpt = opt |> JsonConvert.SerializeObject
                sprintf "graphologyLibrary.layout.rotation.assign(graph, %s);" stringOpt


