namespace Sigma.NET

// https://www.sigmajs.org/docs/interfaces/settings.Settings.html

open DynamicObj
open System
open Newtonsoft.Json

// CssLength Units
/// Represents CSS length units, either in pixels (PX) or percentage (Percent)
type CssLength =
 /// Represents a length in pixels.
| PX of int
/// Represents a length as a percentage of the containing element.
| Percent of int 
with 
    /// Serializes a CssLength value to its string representation.
    /// Parameters:
    ///   - v: The CssLength value to serialize.
    /// Returns:
    ///   - A string representing the CssLength value in pixels or percentage.
    static member serialize (v:CssLength) =
        match v with
        | PX px -> sprintf "%ipx" px
        | Percent p -> sprintf "%i%%" p


/// Module for rendering-related types and methods
module Render =
    /// A type representing either a color or a reference to a color
    type ColorOrReference() =
        inherit DynamicObj ()
        /// Initializes a new instance of ColorOrReference with optional color and reference
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
        /// Applies the provided color and reference to the ColorOrReference object            
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
    /// A type representing various settings for rendering
    type Settings() =
        inherit DynamicObj ()
        /// Initializes a new instance of Settings with optional rendering settings
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
        /// Applies the provided styling options to the Settings object
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
            
