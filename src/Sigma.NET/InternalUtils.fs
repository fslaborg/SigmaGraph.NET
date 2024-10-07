namespace Sigma.NET

open System

//module internal InternalUtils =
/// <summary>
/// Internal utility functions and types for handling JavaScript references.
/// </summary>
module InternalUtils =
    
    /// <summary>
    /// Defines a record type to hold paths or URLs for JavaScript libraries.
    /// </summary>
    type JSRefGroup ={

      /// <summary>URL or path to the Sigma.js library.</summary>
      Sigma         : string
      
      /// <summary>URL or path to the Graphology library.</summary>
      Graphology    : string

      /// <summary>URL or path to the Graphology library (additional version).</summary>
      GraphologyLib : string
    } 

    open System.Reflection
    open System.IO

    /// <summary>
    /// Reads the content of an embedded resource from the assembly.
    /// </summary>
    /// <param name="resourceName">The name of the embedded resource.</param>
    /// <returns>The content of the embedded resource as a string.</returns>
    let readFromManifestResource (resourceName:string) =
        let assembly = Assembly.GetExecutingAssembly()
        use stream = assembly.GetManifestResourceStream(resourceName)
        use reader = new StreamReader(stream)
        reader.ReadToEnd()

    /// <summary>
    /// Retrieves the source code for JavaScript libraries from embedded resources.
    /// </summary>
    /// <returns>A record containing the paths to the JavaScript libraries.</returns>
    let getSourceCodeJS () =

        {
            Sigma = readFromManifestResource "Sigma.NET.sigma.min.js";
            Graphology = readFromManifestResource "Sigma.NET.graphology.umd.min.js";
            GraphologyLib = readFromManifestResource "Sigma.NET.graphology-library.min.js"
        }

    /// <summary>
    /// Returns the URLs for JavaScript libraries from a CDN, using versions specified in Globals.
    /// </summary>
    /// <returns>A record containing the CDN URLs for the JavaScript libraries.</returns>
    let getUriJS () =
        {
            Sigma = $"https://cdnjs.cloudflare.com/ajax/libs/sigma.js/{Globals.SIGMAJS_VERSION}/sigma.min.js";
            Graphology = $"https://cdnjs.cloudflare.com/ajax/libs/graphology/{Globals.GRAPHOLOGY_VERSION}/graphology.umd.min.js";
            GraphologyLib = $"https://cdn.jsdelivr.net/npm/graphology-library@{Globals.GRAPHOLOGY_LIB_VERSION}/dist/graphology-library.min.js" ;
        }

    /// <summary>
    /// Returns the paths for JavaScript libraries based on NuGet package location in the user's profile directory.
    /// </summary>
    /// <returns>A record containing the file paths for the JavaScript libraries.</returns>
    let getNugetPathJS () =
        //Assembly.GetExecutingAssembly().GetName().Version.ToString()
        let home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace("\\", "/")
        {
            Sigma = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/sigma.min.js"
            Graphology = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/graphology.umd.min.js"
            GraphologyLib = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/graphology-library.min.js" ;
        }
