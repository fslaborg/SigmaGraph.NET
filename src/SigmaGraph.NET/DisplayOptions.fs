namespace SigmaGraph.NET

open DynamicObj
open System.Runtime.InteropServices
open Giraffe.ViewEngine

// adapted from Plotly.NET (https://github.com/plotly/Plotly.NET/blob/dev/src/Plotly.NET/DisplayOptions/DisplayOptions.fs)

/// <summary>
/// Represents how the JavaScript library is included in the HTML document's head section.
/// </summary>
type JSlibReference =
    
    /// <summary>
    /// A local reference to the JavaScript library.
    /// </summary>
    /// <param name="InternalUtils.JSRefGroup">The internal group of JavaScript references.</param>
    | Local of InternalUtils.JSRefGroup 
    
    /// <summary>
    /// The URL for a script tag that references the JavaScript library CDN.
    /// </summary>
    /// <param name="InternalUtils.JSRefGroup">The internal group of JavaScript references.</param>
    | CDN of InternalUtils.JSRefGroup
    
    /// <summary>
    /// The full JavaScript library source code (~100KB) is included in the output. 
    /// HTML files generated with this option are fully self-contained and can be used offline.
    /// </summary>
    /// <param name="InternalUtils.JSRefGroup">The internal group of JavaScript references.</param>
    | Full of InternalUtils.JSRefGroup
    
    /// <summary>
    /// Uses RequireJS to reference the JavaScript library from a URL.
    /// </summary>
    /// <param name="InternalUtils.JSRefGroup">The internal group of JavaScript references.</param>
    | Require of InternalUtils.JSRefGroup
    
    /// <summary>
    /// Includes no JavaScript library script at all. 
    /// This can be helpful when embedding the output into a document that already references the JavaScript library.
    /// </summary>
    | NoReference

/// <summary>
/// I am displayotion.
/// </summary>
type DisplayOptions() =
    inherit DynamicObj()

    /// <summary>
    /// Returns a new DisplayOptions object with the given styles
    /// </summary>
    /// <param name="AdditionalHeadTags">Additional tags that will be included in the document's head </param>
    /// <param name="Description">HTML tags that appear below the chart in HTML docs</param>
    /// <param name="SigmaJSReference">Sets how sigma.js is referenced in the head of html docs. When CDN, a script tag that references a CDN is included in the output. When Full, a script tag containing the cytoscape.js source code (~400KB) is included in the output. HTML files generated with this option are fully self-contained and can be used offline</param>
    /// <param name="GraphologyJSReference">Sets how graphology.js is referenced in the head of html docs. When CDN, a script tag that references a CDN is included in the output. When Full, a script tag containing the cytoscape.js source code (~400KB) is included in the output. HTML files generated with this option are fully self-contained and can be used offline</param>
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
    /// Returns a function sthat applies the given styles to a DisplayOptions object
    /// </summary>
    /// <param name="AdditionalHeadTags">Additional tags that will be included in the document's head </param>
    /// <param name="Description">HTML tags that appear below the chart in HTML docs</param>
    /// <param name="CytoscapeJSReference">Sets how cytoscape.js is referenced in the head of html docs. When CDN, a script tag that references a CDN is included in the output. When Full, a script tag containing the cytoscape.js source code (~400KB) is included in the output. HTML files generated with this option are fully self-contained and can be used offline</param>
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
    /// Returns a DisplayOptions Object with the cdn set to Globals.SIGMAJS_VERSION and Globals.GRAPHOLOGY_VERSION
    /// </summary>
    static member initCDNOnly() =
        DisplayOptions()
        |> DisplayOptions.style (
            SigmaJSRef               = CDN (InternalUtils.getUriJS())
        )

    /// <summary>
    /// Returns a DisplayOptions Object with the cdn set to Globals.SIGMAJS_VERSION and additional head tags 
    /// </summary>
    static member initDefault() =
        DisplayOptions.init (
            SigmaJSRef = CDN (InternalUtils.getUriJS ()),
            AdditionalHeadTags =
                [
                    title [] [ str "SigmaGraph.NET Datavisualization" ]
                    meta [ _charset "UTF-8" ]
                    meta
                        [
                            _name "description"
                            _content "A sigma.js graph generated with SigmaGraph.NET"
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

    /// <summary>
    /// Sets additional head tags for the display options.
    /// </summary>
    /// <param name="additionalHeadTags">A list of XML nodes representing the additional head tags to set.</param>
    /// <returns>A function that sets the additional head tags in the display options.</returns>
    static member setAdditionalHeadTags(additionalHeadTags: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            additionalHeadTags |> DynObj.setValue displayOpts "AdditionalHeadTags"
            displayOpts)

    /// <summary>
    /// Tries to get the additional head tags from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>An option containing a list of XML nodes if the additional head tags exist, or None.</returns>
    static member tryGetAdditionalHeadTags(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<XmlNode list>("AdditionalHeadTags")

    /// <summary>
    /// Gets the additional head tags from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>A list of XML nodes representing the additional head tags, or an empty list if not found.</returns>
    static member getAdditionalHeadTags(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetAdditionalHeadTags |> Option.defaultValue []

    /// <summary>
    /// Adds additional head tags to the display options.
    /// </summary>
    /// <param name="additionalHeadTags">A list of XML nodes representing the additional head tags to add.</param>
    /// <returns>A function that adds the additional head tags to the existing ones in the display options.</returns>
    static member addAdditionalHeadTags(additionalHeadTags: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            displayOpts
            |> DisplayOptions.setAdditionalHeadTags (
                List.append (DisplayOptions.getAdditionalHeadTags displayOpts) additionalHeadTags
            ))

    /// <summary>
    /// Sets a description for the display options.
    /// </summary>
    /// <param name="description">A list of XML nodes representing the description to set.</param>
    /// <returns>A function that sets the description in the display options.</returns>
    static member setDescription(description: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            description |> DynObj.setValue displayOpts "Description"
            displayOpts)

    /// <summary>
    /// Tries to get the description from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>An option containing a list of XML nodes if the description exists, or None.</returns>
    static member tryGetDescription(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<XmlNode list>("Description")

    /// <summary>
    /// Gets the description from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>A list of XML nodes representing the description, or an empty list if not found.</returns>
    static member getDescription(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetDescription |> Option.defaultValue []

    /// <summary>
    /// Adds a description to the display options.
    /// </summary>
    /// <param name="description">A list of XML nodes representing the description to add.</param>
    /// <returns>A function that adds the description to the existing one in the display options.</returns>
    static member addDescription(description: XmlNode list) =
        (fun (displayOpts: DisplayOptions) ->
            displayOpts
            |> DisplayOptions.setDescription (List.append (DisplayOptions.getDescription displayOpts) description))

    /// <summary>
    /// Sets the Sigma.js reference for the display options.
    /// </summary>
    /// <param name="sigmaJSReference">The JavaScript library reference to set for Sigma.js.</param>
    /// <returns>A function that sets the Sigma.js reference in the display options.</returns>
    static member setSigmaReference(sigmaJSReference: JSlibReference) =
        (fun (displayOpts: DisplayOptions) ->
            sigmaJSReference |> DynObj.setValue displayOpts "SigmaJSRef"
            displayOpts)

    /// <summary>
    /// Tries to get the Sigma.js reference from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>An option containing the JavaScript library reference for Sigma.js, or None.</returns>
    static member tryGetSigmaReference(displayOpts: DisplayOptions) =
        displayOpts.TryGetTypedValue<JSlibReference>("SigmaJSRef")

    /// <summary>
    /// Gets the Sigma.js reference from the display options.
    /// </summary>
    /// <param name="displayOpts">The display options object.</param>
    /// <returns>The JavaScript library reference for Sigma.js, or NoReference if not found.</returns>
    static member getSigmaReference(displayOpts: DisplayOptions) =
        displayOpts |> DisplayOptions.tryGetSigmaReference |> Option.defaultValue (JSlibReference.NoReference)

  