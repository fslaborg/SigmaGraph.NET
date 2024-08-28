namespace Sigma.NET

open Newtonsoft.Json

/// <summary>
/// Represents a plain JSON string value.
/// </summary>
/// <param name="str">The JSON string value to be represented.</param>
type PlainJsonString(str:string) = 
    member val Value = str with get,set

/// <summary>
/// JSON converter for serializing and deserializing <see cref="PlainJsonString"/> instances.
/// </summary>
type PlainJsonStringConverter() =
    inherit JsonConverter()

    /// <summary>
    /// Determines if this converter can handle the given object type.
    /// </summary>
    /// <param name="objectType">The type of the object to check.</param>
    /// <returns>True if this converter can convert the object type; otherwise, false.</returns>
    override __.CanConvert(objectType) =
        typeof<PlainJsonString> = objectType
    
    /// <summary>
    /// Reads JSON and deserializes it into a <see cref="PlainJsonString"/> instance.
    /// </summary>
    /// <param name="reader">The JSON reader to read from.</param>
    /// <param name="objectType">The type of object being deserialized.</param>
    /// <param name="existingValue">Existing value of the object (not used).</param>
    /// <param name="serializer">The JSON serializer.</param>
    /// <returns>A <see cref="PlainJsonString"/> instance populated with the JSON string value.</returns>
    override __.ReadJson(reader, objectType, existingValue, serializer) =
        reader.Value
    
    /// <summary>
    /// Writes the <see cref="PlainJsonString"/> instance as JSON.
    /// </summary>
    /// <param name="writer">The JSON writer to write to.</param>
    /// <param name="value">The <see cref="PlainJsonString"/> instance to serialize.</param>
    /// <param name="serializer">The JSON serializer.</param>
    override __.WriteJson(writer, value, serializer) =
        //serializer.PreserveReferencesHandling <- PreserveReferencesHandling.Objects
        serializer.ReferenceLoopHandling <- ReferenceLoopHandling.Ignore//ReferenceLoopHandling.Serialize

        let v = value :?> PlainJsonString
        writer.WriteRawValue(string v.Value)