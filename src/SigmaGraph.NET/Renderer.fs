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
                    colorOrReference |> DynObj.setOptionalProperty "color"         Color 
                    colorOrReference |> DynObj.setOptionalProperty "reference"     Reference 
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
                    settings|> DynObj.setOptionalProperty  "hideEdgesOnMove"         HideEdgesOnMove       
                    settings|> DynObj.setOptionalProperty  "hideLabelsOnMove"        HideLabelsOnMove       
                    settings|> DynObj.setOptionalProperty  "renderLabels"            RenderLabels    
                    settings|> DynObj.setOptionalProperty  "renderEdgeLabels"        RenderEdgeLabels       
                    settings|> DynObj.setOptionalProperty  "enableEdgeClickEvents"   EnableEdgeClickEvents           
                    settings|> DynObj.setOptionalProperty  "enableEdgeWheelEvents"   EnableEdgeWheelEvents           
                    settings|> DynObj.setOptionalProperty  "enableEdgeHoverEvents"   EnableEdgeHoverEvents           
                    settings|> DynObj.setOptionalProperty  "defaultNodeColor"        DefaultNodeColor       
                    
                    settings|> DynObj.setOptionalPropertyBy "defaultNodeType" DefaultNodeType StyleParam.NodeType.toString
                    
                    settings|> DynObj.setOptionalProperty  "defaultEdgeColor" DefaultEdgeColor
                    settings|> DynObj.setOptionalPropertyBy "defaultEdgeType" DefaultEdgeType StyleParam.EdgeType.toString

                    settings|> DynObj.setOptionalProperty  "labelFont"                   LabelFont        
                    settings|> DynObj.setOptionalProperty  "labelSize"                   LabelSize        
                    settings|> DynObj.setOptionalProperty  "labelWeight"                 LabelWeight            
                    settings|> DynObj.setOptionalProperty  "labelColor"                  LabelColor        
                    settings|> DynObj.setOptionalProperty  "edgeLabelFont"               EdgeLabelFont            
                    settings|> DynObj.setOptionalProperty  "edgeLabelSize"               EdgeLabelSize            
                    settings|> DynObj.setOptionalProperty  "edgeLabelWeight"             EdgeLabelWeight                
                    settings|> DynObj.setOptionalProperty  "edgeLabelColor"              EdgeLabelColor            
                    settings|> DynObj.setOptionalProperty  "stagePadding"                StagePadding            
                    settings|> DynObj.setOptionalProperty  "labelDensity"                LabelDensity            
                    settings|> DynObj.setOptionalProperty  "labelGridCellSize"           LabelGridCellSize                
                    settings|> DynObj.setOptionalProperty  "labelRenderedSizeThreshold"  LabelRenderedSizeThreshold                        
                    settings|> DynObj.setOptionalProperty  "nodeReducer"                 NodeReducer            
                    settings|> DynObj.setOptionalProperty  "edgeReducer"                 EdgeReducer            
                    settings|> DynObj.setOptionalProperty  "zIndex"                      ZIndex    
                    settings|> DynObj.setOptionalProperty  "minCameraRatio"              MinCameraRatio            
                    settings|> DynObj.setOptionalProperty  "maxCameraRatio"              MaxCameraRatio            
                    settings|> DynObj.setOptionalProperty  "labelRenderer"               LabelRenderer            
                    settings|> DynObj.setOptionalProperty  "hoverRenderer"               HoverRenderer            
                    settings|> DynObj.setOptionalProperty  "edgeLabelRenderer"           EdgeLabelRenderer                
                    
                    // out ->
                    settings
                )
            
