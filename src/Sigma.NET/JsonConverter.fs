namespace Sigma.NET

open Newtonsoft.Json
/// Represents a plain JSON string value.
type PlainJsonString(str:string) = 
    member val Value = str with get,set
/// JSON converter for serializing and deserializing `PlainJsonString` instances.
type PlainJsonStringConverter() =
    inherit JsonConverter()

    /// Determines if this converter can handle the given object type.
    override __.CanConvert(objectType) =
        typeof<PlainJsonString> = objectType
    /// Reads JSON and deserializes it into a `PlainJsonString` instance.
    override __.ReadJson(reader, objectType, existingValue, serializer) =
        reader.Value
    /// Writes the `PlainJsonString` instance as JSON.
    override __.WriteJson(writer, value, serializer) =
        //serializer.PreserveReferencesHandling <- PreserveReferencesHandling.Objects
        serializer.ReferenceLoopHandling <- ReferenceLoopHandling.Ignore//ReferenceLoopHandling.Serialize

        let v = value :?> PlainJsonString
        writer.WriteRawValue(string v.Value)