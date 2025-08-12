# Clean up the previously-cached NuGet packages.
# Lower-case is intentional (that's how nuget stores those packages).
rm -rf ~\.nuget\packages\SigmaGraph.NET.interactive* 
rm -rf ~\.nuget\packages\SigmaGraph.NET* 

# build and pack Plotly.NET.Interactive
cd ../../
sh ./build.sh
dotnet pack -c Release -p:PackageVersion=0.0.0-dev -o "./pkg"
cd src/SigmaGraph.NET.Interactive
