namespace Sigma.NET

open System

// Internal utility functions and types for handling JavaScript references
module InternalUtils =

    // Defines a record type to hold paths or URLs for JavaScript libraries.
    type JSRefGroup = {
        Sigma         : string  // Path or URL for sigma.js
        Graphology    : string  // Path or URL for graphology.umd.js
        GraphologyLib : string  // Path or URL for graphology-library.min.js
    } 

    open System.Reflection
    open System.IO

    /// Reads the content of an embedded resource from the assembly.
    let readFromManifestResource (resourceName: string) =
        let assembly = Assembly.GetExecutingAssembly()
        use stream = assembly.GetManifestResourceStream(resourceName)
        use reader = new StreamReader(stream)
        reader.ReadToEnd()

    /// Retrieves the source code for JavaScript libraries from embedded resources.
    let getSourceCodeJS () =
        {
            Sigma = readFromManifestResource "Sigma.NET.sigma.min.js";          // Embedded sigma.js
            Graphology = readFromManifestResource "Sigma.NET.graphology.umd.min.js"; // Embedded graphology.umd.js
            GraphologyLib = readFromManifestResource "Sigma.NET.graphology-library.min.js" // Embedded graphology-library.min.js
        }

    /// Returns the URLs for JavaScript libraries from a CDN, using versions specified in Globals.
    let getUriJS () =
        {
            Sigma = $"https://cdnjs.cloudflare.com/ajax/libs/sigma.js/{Globals.SIGMAJS_VERSION}/sigma.min.js"; // sigma.js URL
            Graphology = $"https://cdnjs.cloudflare.com/ajax/libs/graphology/{Globals.GRAPHOLOGY_VERSION}/graphology.umd.min.js"; // graphology.umd.js URL
            GraphologyLib = $"https://cdn.jsdelivr.net/npm/graphology-library@{Globals.GRAPHOLOGY_LIB_VERSION}/dist/graphology-library.min.js" // graphology-library.min.js URL
        }

    /// Returns the paths for JavaScript libraries based on NuGet package location in the user's profile directory.
    let getNugetPathJS () =
        let home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace("\\", "/")
        {
            Sigma = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/sigma.min.js"; // Path to sigma.min.js in NuGet packages
            Graphology = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/graphology.umd.min.js"; // Path to graphology.umd.min.js in NuGet packages
            GraphologyLib = $"{home}/.nuget/packages/Sigma.NET/{Globals.NUGET_VERSION}/content/graphology-library.min.js"; // Path to graphology-library.min.js in NuGet packages
        }
