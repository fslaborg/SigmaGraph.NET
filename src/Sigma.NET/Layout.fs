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

/// <summary>
/// FA2Settings type for configuring the Force Atlas 2 algorithm settings.
/// </summary>
type FA2Settings() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of FA2Settings with optional parameters.
    /// </summary>
    /// <param name="AdjustSizes">Optional boolean to indicate if node sizes should be considered.</param>
    /// <param name="BarnesHutOptimize">Optional boolean to use Barnes-Hut approximation for repulsion calculation.</param>
    /// <param name="BarnesHutTheta">Optional float to set the Barnes-Hut approximation theta parameter.</param>
    /// <param name="EdgeWeightInfluence">Optional integer to specify the influence of edge weights on the layout.</param>
    /// <param name="Gravity">Optional integer to set the strength of the layout’s gravity.</param>
    /// <param name="LinLogMode">Optional boolean to use Noack’s LinLog model.</param>
    /// <param name="OutboundAttractionDistribution">Optional boolean to control outbound attraction distribution.</param>
    /// <param name="ScalingRatio">Optional integer to set the scaling ratio.</param>
    /// <param name="SlowDown">Optional integer to set the dampening factor for slowing down node movements.</param>
    /// <param name="StrongGravityMode">Optional boolean to enable strong gravity mode.</param>
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
    
    /// <summary>
    /// Applies updates to the FA2Settings instance based on the optional parameters.
    /// </summary>
    /// <param name="AdjustSizes">Optional boolean to indicate if node sizes should be considered.</param>
    /// <param name="BarnesHutOptimize">Optional boolean to use Barnes-Hut approximation for repulsion calculation.</param>
    /// <param name="BarnesHutTheta">Optional float to set the Barnes-Hut approximation theta parameter.</param>
    /// <param name="EdgeWeightInfluence">Optional integer to specify the influence of edge weights on the layout.</param>
    /// <param name="Gravity">Optional integer to set the strength of the layout’s gravity.</param>
    /// <param name="LinLogMode">Optional boolean to use Noack’s LinLog model.</param>
    /// <param name="OutboundAttractionDistribution">Optional boolean to control outbound attraction distribution.</param>
    /// <param name="ScalingRatio">Optional integer to set the scaling ratio.</param>
    /// <param name="SlowDown">Optional integer to set the dampening factor for slowing down node movements.</param>
    /// <param name="StrongGravityMode">Optional boolean to enable strong gravity mode.</param>
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

/// <summary>
/// NoverlapSettings type for configuring the Noverlap algorithm settings.
/// </summary>
type NoverlapSettings() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of NoverlapSettings with optional parameters.
    /// </summary>
    /// <param name="GridSize">Optional integer specifying the number of grid cells horizontally and vertically.</param>
    /// <param name="Margin">Optional integer to set the margin between nodes.</param>
    /// <param name="Expansion">Optional float to specify the percentage of space nodes could move outside of.</param>
    /// <param name="Ratio">Optional float to set the scaling ratio for node sizes.</param>
    /// <param name="Speed">Optional integer to set the dampening factor for slowing down node movements.</param>
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
    
    
    /// <summary>
    /// Applies updates to the NoverlapSettings instance based on the optional parameters.
    /// </summary>
    /// <param name="GridSize">Optional integer specifying the number of grid cells horizontally and vertically.</param>
    /// <param name="Margin">Optional integer to set the margin between nodes.</param>
    /// <param name="Expansion">Optional float to specify the percentage of space nodes could move outside of.</param>
    /// <param name="Ratio">Optional float to set the scaling ratio for node sizes.</param>
    /// <param name="Speed">Optional integer to set the dampening factor for slowing down node movements.</param>
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

/// <summary>
/// FA2Options type for configuring the Force Atlas 2 layout options.
/// </summary>
type FA2Options() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of FA2Options with optional parameters.
    /// </summary>
    /// <param name="Iterations">Optional integer to specify the number of iterations to perform.</param>
    /// <param name="GetEdgeWeight">Optional string specifying the edge weight attribute or a getter function.</param>
    /// <param name="Settings">Optional FA2Settings to configure the Force Atlas 2 algorithm settings.</param>
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

    /// <summary>
    /// Applies updates to the FA2Options instance based on the optional parameters.
    /// </summary>
    /// <param name="Iterations">Optional integer to specify the number of iterations to perform.</param>
    /// <param name="GetEdgeWeight">Optional string specifying the edge weight attribute or a getter function.</param>
    /// <param name="Settings">Optional FA2Settings to configure the Force Atlas 2 algorithm settings.</param>
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

/// <summary>
/// NoverlapOptions type for configuring the Noverlap layout options.
/// </summary>
type NoverlapOptions() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of NoverlapOptions with optional parameters.
    /// </summary>
    /// <param name="MaxIterations">Optional integer to specify the maximum number of iterations before stopping.</param>
    /// <param name="InputReducer">Optional string specifying the function to reduce each node’s attributes.</param>
    /// <param name="OutputReducer">Optional string specifying the function to reduce node positions as computed by the layout algorithm.</param>
    /// <param name="Settings">Optional NoverlapSettings to configure the Noverlap algorithm settings.</param>
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
    /// <summary>  
    /// Applies updates to the NoverlapOptions instance based on the optional parameters.
    /// </summary>
    /// <param name="MaxIterations">Optional integer to specify the maximum number of iterations before stopping.</param>
    /// <param name="InputReducer">Optional string specifying the function to reduce each node’s attributes.</param>
    /// <param name="OutputReducer">Optional string specifying the function to reduce node positions as computed by the layout algorithm.</param>
    /// <param name="Settings">Optional NoverlapSettings to configure the Noverlap algorithm settings.</param>
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

/// <summary>
/// CircularOptions type for configuring the Circular layout options
/// </summary>
type CircularOptions() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of CircularOptions with optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions of the layout.</param>
    /// <param name="Center">Optional float to set the center of the layout.</param>
    /// <param name="Scale">Optional float to set the scale of the layout.</param>
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
    ///<summary>    
    /// Applies updates to the CircularOptions instance based on the optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions of the layout.</param>
    /// <param name="Center">Optional float to set the center of the layout.</param>
    /// <param name="Scale">Optional float to set the scale of the layout.</param>
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

/// <summary>
/// RandomOptions type for configuring the Random layout options
/// </summary>
type RandomOptions() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of RandomOptions with optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions of the layout.
    /// <param name="Center">Optional float to set the center of the layout.
    /// <param name="Scale">Optional integer to set the scale of the layout.
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

    /// <summary>    
    /// Applies updates to the RandomOptions instance based on the optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions of the layout.</param>
    /// <param name="Center">Optional float to set the center of the layout.</param>
    /// <param name="Scale">Optional integer to set the scale of the layout.</param>
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

/// <summary>
/// RotationOptions contains options for configuring a Rotation layout algorithm.
///</summary>
type RotationOptions() =
    inherit DynamicObj ()

    /// <summary>
    /// Initializes a new instance of RotationOptions with optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions for rotation.</param>
    /// <param name="Degrees">Optional boolean to indicate if the angle is in degrees.</param>
    /// <param name="CenteredOnZero">Optional boolean to rotate the graph around 0 instead of the graph’s center.</param>
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
    /// <summary>
    /// Applies updates to the RotationOptions instance based on the optional parameters.
    /// </summary>
    /// <param name="Dimensions">Optional string array specifying the dimensions for rotation.</param>
    /// <param name="Degrees">Optional boolean to indicate if the angle is in degrees.</param>
    /// <param name="CenteredOnZero">Optional boolean to rotate the graph around 0 instead of the graph’s center.</param>
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

/// <sumary>
/// Layout defines different layout algorithms for a graph and serializes them into JavaScript commands.
/// </summary>
type Layout =
    | FA2 of FA2Options
    | Noverlap of NoverlapOptions
    | Circular of CircularOptions
    | Random of RandomOptions
    | Rotation of RotationOptions
    with 
        
        /// <summary>
        /// Serializes the selected layout into a JavaScript command string.
        /// </summary>
        /// <param name="layout">The Layout instance to serialize.</param>
        /// <returns>A string representing the JavaScript command to apply the layout.</returns>
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


