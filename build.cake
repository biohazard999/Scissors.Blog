var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");
var pretzelVersion = "0.7.1";

Task("Clean")
  .Does(() =>
{
  if(FileExists("Tools/Pretzel.zip"))
    DeleteFile("Tools/Pretzel.zip");
  if(DirectoryExists("Tools/Pretzel"))
    DeleteDirectory("Tools/Pretzel", new DeleteDirectorySettings {
      Recursive = true
    });
  if(DirectoryExists("_site"))
    DeleteDirectory("_site", new DeleteDirectorySettings {
      Recursive = true
    });
});


Task("DownloadPretzel")
  .IsDependentOn("Clean")
  .Does(() => DownloadFile($"https://github.com/Code52/pretzel/releases/download/v{pretzelVersion}/Pretzel.{pretzelVersion}.zip", "Tools/Pretzel.zip"));

Task("UnzipPretzel")
  .IsDependentOn("DownloadPretzel")
  .Does(() =>
{
   Unzip("Tools/Pretzel.zip","Tools/Pretzel");
   DeleteFile("Tools/Pretzel.zip");
});

Task("Only-Bake")
  .Does(() =>
{
   using(var process = StartAndReturnProcess("Tools/Pretzel/Pretzel.exe", new ProcessSettings
   {
      Arguments = "bake"
   }))
   {
        process.WaitForExit();
        var result = process.GetExitCode();
        Information("Exit code: {0}", result);
        
        if(result != 0){
            throw new Exception("Pretzel did not bake correctly: Error-Code: " + result); 
        }
   }
});

Task("Bake")
  .IsDependentOn("UnzipPretzel")
  .IsDependentOn("Only-Bake");

Task("Only-Taste")
  .Does(() =>
{
   using(var process = StartAndReturnProcess("Tools/Pretzel/Pretzel.exe", new ProcessSettings
   {
      Arguments = "taste"
   }))
   {
        process.WaitForExit();
        var result = process.GetExitCode();
        Information("Exit code: {0}", result);
        
        if(result != 0){
            throw new Exception("Pretzel did not taste correctly: Error-Code: " + result); 
        }
   }
});

Task("Taste")
  .IsDependentOn("UnzipPretzel")
  .IsDependentOn("Only-Taste");

Task("Draft")
  .Does(() =>
{
   using(var process = StartAndReturnProcess("Tools/Pretzel/Pretzel.exe", new ProcessSettings
   {
      Arguments = "ingredient --drafts"
   }))
   {
        process.WaitForExit();
        var result = process.GetExitCode();
        Information("Exit code: {0}", result);
        
        if(result != 0){
            throw new Exception("Pretzel did not ingredient correctly: Error-Code: " + result); 
        }
   }
});

Task("Ingredient")
  .Does(() =>
{
   using(var process = StartAndReturnProcess("Tools/Pretzel/Pretzel.exe", new ProcessSettings
   {
      Arguments = "ingredient"
   }))
   {
        process.WaitForExit();
        var result = process.GetExitCode();
        Information("Exit code: {0}", result);
        
        if(result != 0){
            throw new Exception("Pretzel did not ingredient correctly: Error-Code: " + result); 
        }
   }
});


Task("Default")
  .IsDependentOn("Bake");

RunTarget(target);