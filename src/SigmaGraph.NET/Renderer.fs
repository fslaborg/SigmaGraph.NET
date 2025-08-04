namespace SigmaGraph.NET

// https://www.sigmajs.org/docs/interfaces/settings.Settings.html

open DynamicObj
open System
open Newtonsoft.Json

// CssLength Units

/// <summary>
/// Represents CSS length units, either in pixels (PX) or percentage (Percent)
///</summary>
type CssLength =

/// <summary>Represents a length in pixels.</summary>
| PX of int

/// <summary>Represents a length as a percentage of the containing element.</summary>
| Percent of int 
with 
    /// <summary>Serializes a CssLength value to its string representation.</summary>
    /// <param name= "v">The CssLength value to serialize.</param>
    /// <returns>A string representing the CssLength value in pixels or percentage.</returns>
    static member serialize (v:CssLength) =
        match v with
        | PX px -> sprintf "%ipx" px
        | Percent p -> sprintf "%i%%" p


/// <summary>Module for rendering-related types and methods</summary>
module Render =

    /// <summary>A type representing either a color or a reference to a color</summary>
    type ColorOrReference() =
        inherit DynamicObj ()

        /// <summary>Initializes a new instance of ColorOrReference with optional color and reference</summary>
        static member Init 
            ( 
                ?Color : string,
                ?Reference : string
            ) =
                ColorOrReference()
                |> ColorOrReference.Style
                    (
                        ?Color = Color,
                        ?Reference = Reference
                    )

        /// <summary>Applies the provided color and reference to the ColorOrReference object</summary>            
        static member Style
            ( 
                ?Color,
                ?Reference
            ) =
                (fun (colorOrReference:ColorOrReference) -> 
                    Color |> DynObj.setValueOpt colorOrReference "color"
                    Reference |> DynObj.setValueOpt colorOrReference "reference"
                    // out ->
                    colorOrReference
                )

    /// <summary>A type representing various settings for rendering</summary>
    type Settings() =
        inherit DynamicObj ()

        /// <summary>Initializes a new instance of Settings with optional rendering settings</summary>
        static member Init(
            ?HideEdgesOnMove : bool,
            ?HideLabelsOnMove : bool,
            ?RenderLabels : bool,
            ?RenderEdgeLabels : bool,
            ?EnableEdgeClickEvents : bool,
            ?EnableEdgeWheelEvents : bool,
            ?EnableEdgeHoverEvents : bool,
            ?DefaultNodeColor : string,
            ?DefaultNodeType : StyleParam.NodeType,
            ?DefaultEdgeColor : string,
            ?DefaultEdgeType : StyleParam.EdgeType,    
            ?LabelFont : string,
            ?LabelSize : int,
            ?LabelWeight : string,
            ?LabelColor : ColorOrReference,
            ?EdgeLabelFont : string,
            ?EdgeLabelSize : int,
            ?EdgeLabelWeight : string,
            ?EdgeLabelColor : ColorOrReference,
            ?StagePadding : int,
            ?LabelDensity : int,
            ?LabelGridCellSize : int,
            ?LabelRenderedSizeThreshold : int,
            ?NodeReducer : string,
            ?EdgeReducer : string,
            ?ZIndex : bool,
            ?MinCameraRatio : int,
            ?MaxCameraRatio : int,
            ?LabelRenderer : string,
            ?HoverRenderer : string,
            ?EdgeLabelRenderer : string
        ) =
            Settings()
            |> Settings.Style
                (
                    ?HideEdgesOnMove = HideEdgesOnMove,
                    ?HideLabelsOnMove = HideLabelsOnMove,  
                    ?RenderLabels = RenderLabels,
                    ?RenderEdgeLabels = RenderEdgeLabels,
                    ?EnableEdgeClickEvents = EnableEdgeClickEvents,
                    ?EnableEdgeWheelEvents = EnableEdgeWheelEvents,
                    ?EnableEdgeHoverEvents = EnableEdgeHoverEvents,
                    ?DefaultNodeColor = DefaultNodeColor,
                    ?DefaultNodeType = DefaultNodeType,
                    ?DefaultEdgeColor = DefaultEdgeColor,
                    ?DefaultEdgeType = DefaultEdgeType,
                    ?LabelFont = LabelFont,
                    ?LabelSize = LabelSize,
                    ?LabelWeight = LabelWeight,
                    ?LabelColor = LabelColor,
                    ?EdgeLabelFont = EdgeLabelFont,
                    ?EdgeLabelSize = EdgeLabelSize,
                    ?EdgeLabelWeight = EdgeLabelWeight,
                    ?EdgeLabelColor = EdgeLabelColor,
                    ?StagePadding = StagePadding,
                    ?LabelDensity = LabelDensity,
                    ?LabelGridCellSize = LabelGridCellSize,
                    ?LabelRenderedSizeThreshold = LabelRenderedSizeThreshold,
                    ?NodeReducer = NodeReducer,
                    ?EdgeReducer = EdgeReducer,
                    ?ZIndex = ZIndex,
                    ?MinCameraRatio = MinCameraRatio,
                    ?MaxCameraRatio = MaxCameraRatio,
                    ?LabelRenderer = LabelRenderer,
                    ?HoverRenderer = HoverRenderer,
                    ?EdgeLabelRenderer = EdgeLabelRenderer
                )

        /// <summary>Applies the provided styling options to the Settings object</summary>
        static member Style
            ( 
                ?HideEdgesOnMove,
                ?HideLabelsOnMove,
                ?RenderLabels,
                ?RenderEdgeLabels,
                ?EnableEdgeClickEvents,
                ?EnableEdgeWheelEvents,
                ?EnableEdgeHoverEvents,
                ?DefaultNodeColor,
                ?DefaultNodeType,
                ?DefaultEdgeColor,
                ?DefaultEdgeType,
                ?LabelFont,
                ?LabelSize,
                ?LabelWeight,
                ?LabelColor,
                ?EdgeLabelFont,
                ?EdgeLabelSize,
                ?EdgeLabelWeight,
                ?EdgeLabelColor,
                ?StagePadding,
                ?LabelDensity,
                ?LabelGridCellSize,
                ?LabelRenderedSizeThreshold,
                ?NodeReducer,
                ?EdgeReducer,
                ?ZIndex,
                ?MinCameraRatio,
                ?MaxCameraRatio,
                ?LabelRenderer,
                ?HoverRenderer,
                ?EdgeLabelRenderer
            ) =
                (fun (settings:Settings) -> 
                    HideEdgesOnMove |> DynObj.setValueOpt settings "hideEdgesOnMove"
                    HideLabelsOnMove |> DynObj.setValueOpt settings "hideLabelsOnMove"
                    RenderLabels |> DynObj.setValueOpt settings "renderLabels"
                    RenderEdgeLabels |> DynObj.setValueOpt settings "renderEdgeLabels"
                    EnableEdgeClickEvents |> DynObj.setValueOpt settings "enableEdgeClickEvents"
                    EnableEdgeWheelEvents |> DynObj.setValueOpt settings "enableEdgeWheelEvents"
                    EnableEdgeHoverEvents |> DynObj.setValueOpt settings "enableEdgeHoverEvents"
                    DefaultNodeColor |> DynObj.setValueOpt settings "defaultNodeColor"
                    
                    DefaultNodeType |> DynObj.setValueOptBy settings "defaultNodeType" StyleParam.NodeType.toString
                    
                    DefaultEdgeColor |> DynObj.setValueOpt settings "defaultEdgeColor"
                    DefaultEdgeType |> DynObj.setValueOptBy settings "defaultEdgeType" StyleParam.EdgeType.toString
                    
                    LabelFont |> DynObj.setValueOpt settings "labelFont"
                    LabelSize |> DynObj.setValueOpt settings "labelSize"
                    LabelWeight |> DynObj.setValueOpt settings "labelWeight"
                    LabelColor |> DynObj.setValueOpt settings "labelColor"
                    EdgeLabelFont |> DynObj.setValueOpt settings "edgeLabelFont"
                    EdgeLabelSize |> DynObj.setValueOpt settings "edgeLabelSize"
                    EdgeLabelWeight |> DynObj.setValueOpt settings "edgeLabelWeight"
                    EdgeLabelColor |> DynObj.setValueOpt settings "edgeLabelColor"
                    StagePadding |> DynObj.setValueOpt settings "stagePadding"
                    LabelDensity |> DynObj.setValueOpt settings "labelDensity"
                    LabelGridCellSize |> DynObj.setValueOpt settings "labelGridCellSize"
                    LabelRenderedSizeThreshold |> DynObj.setValueOpt settings "labelRenderedSizeThreshold"
                    NodeReducer |> DynObj.setValueOpt settings "nodeReducer"
                    EdgeReducer |> DynObj.setValueOpt settings "edgeReducer"
                    ZIndex |> DynObj.setValueOpt settings "zIndex"
                    MinCameraRatio |> DynObj.setValueOpt settings "minCameraRatio"
                    MaxCameraRatio |> DynObj.setValueOpt settings "maxCameraRatio"
                    LabelRenderer |> DynObj.setValueOpt settings "labelRenderer"
                    HoverRenderer |> DynObj.setValueOpt settings "hoverRenderer"
                    EdgeLabelRenderer |> DynObj.setValueOpt settings "edgeLabelRenderer"
                    
                    // out ->
                    settings
                )
            
