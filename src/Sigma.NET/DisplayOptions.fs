namespace Sigma.NET

open DynamicObj
open System.Runtime.InteropServices
open Giraffe.ViewEngine

// Adapted from Plotly.NET (https://github.com/plotly/Plotly.NET/blob/dev/src/Plotly.NET/DisplayOptions/DisplayOptions.fs)

/// Sets how the JavaScript library is referenced in the head of HTML documents.
type JSlibReference =
    /// Local reference to the JavaScript library.
    | Local of InternalUtils.JSRefGroup 
    /// The URL for a script tag that references the JavaScript library CDN.
    | CDN of InternalUtils.JSRefGroup
    /// Full JavaScript library source code (~100KB) included in the output. HTML files generated with this option are fully self-contained and can be used offline.
    | Full of InternalUtils.JSRefGroup
    /// Use requirejs to reference the JavaScript library from a URL.
    | Require of InternalUtils.JSRefGroup
    /// Include no JavaScript library script at all. Useful when embedding the output into a document that already references the JavaScript library.
    | NoReference

/// Represents display options for the graph visualization.
type DisplayOptions() =
    inherit DynamicObj()

    /// <summary>
    /// Returns a new DisplayOptions object with the given styles.
    /// </summary>
    /// <param name="AdditionalHeadTags">Additional tags that will be included in the document's head.</param>
    /// <param name="Description">HTML tags that appear below the chart in HTML documents.</param>
    /// <param name="SigmaJSReference">Sets how sigma.js is referenced in the head of HTML documents. When CDN, a script tag that references a CDN is included in the output. When Full, a script tag containing the cytoscape.js source code (~400KB) is included in the output. HTML files generated with this option are fully self-contained and can be used offline.</param>
    static member init
        (
            [<Optional; DefaultParameterValue(null)>] ?AdditionalHeadTags: XmlNode list,
            [<Optional; DefaultParameterValue(null)>] ?Description: XmlNode list,
            [<Optional; DefaultParameterValue(null)>] ?SigmaJSRef: JSlibReference
        ) =
        DisplayOptions()
        |> DisplayOptions.style (
            ?AdditionalHeadTags = AdditionalHeadTags,
            ?Description = Description,
            ?SigmaJSRef = SigmaJSRef
        )

    /// <summary>
    /// Returns a function that applies the given styles to a DisplayOptions object.
    /// </summary>
    /// <param name="AdditionalHeadTags">Additional tags that will be included in the document's head.</param>
    /// <param name="Description">HTML tags that appear below the chart in HTML documents.</param>
    /// <param name="SigmaJSReference">Sets how sigma.js is referenced in the head of HTML documents.</param>
    static member style
        (
            [<Optional; DefaultParameterValue(null)>] ?AdditionalHeadTags: XmlNode list,
            [<Optional; DefaultParameterValue(null)>] ?Description: XmlNode list,
            [<Optional; DefaultParameterValue(null)>] ?SigmaJSRef: JSlibReference
        ) =
        (fun (displayOpts: DisplayOptions) ->

            AdditionalHeadTags |> DynObj.setValueOpt displayOpts "AdditionalHeadTags"
            Description |> DynObj.setValueOpt displayOpts "Description"
            SigmaJSRef |> DynObj.setValueOpt displayOpts "SigmaJSRef"

            displayOpts)

    /// <summary>
    /// Returns a DisplayOptions object with the CDN set to Globals.SIGMAJS_VERSION and Globals.GRAPHOLOGY_VERSION.
    /// </summary>
    static member initCDNOnly() =
        DisplayOptions()
        |> DisplayOptions.style (
            SigmaJSRef = CDN (InternalUtils.getUriJS())
        )

    /// <summary>
    /// Returns a DisplayOptions object with the CDN set to Globals.SIGMAJS_VERSION and additional head tags.
    /// </summary>
    static member initDefault() =
        DisplayOptions.init (
            SigmaJSRef = CDN (InternalUtils.getUriJS()),
            AdditionalHeadTags =
                [
                    title [] [ str "Sigma.NET Datavisualization" ]
                    meta [ _charset "UTF-8" ]
                    meta
                        [
                            _name "description"
                            _content "A sigma.js graph generated with Sigma.NET"
                        ]
                    link
                        [
                            _id "favicon"
                            _rel "shortcut icon"
                            _type "image/png"
                            _href $"data:image/png;base64,{Globals.LOGO_BASE64}"
                        ]
                ]
        )

    /// Sets additional head tags for the display options.
    static member setAdditionalHeadTags(additionalHeadTags: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            additionalHeadTags |> DynObj.setValue displayOpts "AdditionalHeadTags"
            displayOpts)

    /// Tries to get additional head tags from the display options.
    static member tryGetAdditionalHeadTags(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<XmlNode list>("AdditionalHeadTags")

    /// Gets additional head tags from the display options, defaulting to an empty list if not found.
    static member getAdditionalHeadTags(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetAdditionalHeadTags |> Option.defaultValue []

    /// Adds additional head tags to the display options.
    static member addAdditionalHeadTags(additionalHeadTags: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            displayOpts
            |> DisplayOptions.setAdditionalHeadTags (
                List.append (DisplayOptions.getAdditionalHeadTags displayOpts) additionalHeadTags
            ))

    /// Sets the description for the display options.
    static member setDescription(description: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            description |> DynObj.setValue displayOpts "Description"
            displayOpts)

    /// Tries to get the description from the display options.
    static member tryGetDescription(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<XmlNode list>("Description")

    /// Gets the description from the display options, defaulting to an empty list if not found.
    static member getDescription(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetDescription |> Option.defaultValue []

    /// Adds a description to the display options.
    static member addDescription(description: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            displayOpts
            |> DisplayOptions.setDescription (List.append (DisplayOptions.getDescription displayOpts) description))

    /// Sets the sigma.js reference for the display options.
    static member setSigmaReference(sigmaJSReference: JSlibReference) =
        (fun (displayOpts: DisplayOptions) ->
            sigmaJSReference |> DynObj.setValue displayOpts "SigmaJSRef"
            displayOpts)

    /// Tries to get the sigma.js reference from the display options.
    static member tryGetSigmaReference(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<JSlibReference>("SigmaJSRef")

    /// Gets the sigma.js reference from the display options, defaulting to NoReference if not found.
    static member getSigmaReference(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetSigmaReference |> Option.defaultValue (JSlibReference.NoReference)
