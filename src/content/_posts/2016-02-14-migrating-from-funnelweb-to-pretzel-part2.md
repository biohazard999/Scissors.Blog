---
 layout: post 
 title: "Migrating from FunnelWeb to Pretzel - Part 2"
 series: migrating-from-funnelweb-to-pretzel
 comments: true
 tags: [Pretzel, Git, Cake, VSTS, VisualStudioTeamServices]
---
In our [last post](/2016/02/13/migrating-from-funnelweb-to-pretzel-part1.html) we got up and running with pretzel.

This time we will look how we setup [Visual-Studio-Team-Services](//www.visualstudio.com/en-us/products/visual-studio-team-services-vs.aspx) to build our blog.
As a Build-Tool we will use [Cake](//cakebuild.net/) cause we can write our build scripts handy in C#.

<!-- more -->
## Let's make some Cake!

Let's navigate in our blog directory and install the `cake` bootstrapper.

```powershell
powershell
Invoke-WebRequest https://cakebuild.net/bootstrapper/windows -OutFile build.ps1
```

Add a file named `build.cake`:

```cs
var target = Argument("target", "Default");

Task("Clean")
  .Does(() =>
{
  if(FileExists("Tools/Pretzel.zip"))
    DeleteFile("Tools/Pretzel.zip");
  if(DirectoryExists("Tools/Pretzel"))
    DeleteDirectory("Tools/Pretzel", true);
  if(DirectoryExists("_site"))
    DeleteDirectory("_site", true);
});


Task("DownloadPretzel")
  .IsDependentOn("Clean")
  .Does(() =>
{
   DownloadFile("https://github.com/Code52/pretzel/releases/download/v0.4.0/Pretzel.0.4.0.zip", "Tools/Pretzel.zip");
});

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
```

This is build script got a bunch of `Targets` but we can focus for now on `Bake` and `Taste`.

```powershell
.\build.ps1
```
This will output:

```cmd
Preparing to run build script...
Running build script...
Analyzing build script...
Processing build script...
Downloading and installing Roslyn...
Installing package...
Copying files...
Copying Roslyn.Compilers.CSharp.dll...
Copying Roslyn.Compilers.dll...
Deleting installation directory...
Compiling build script...

========================================
Clean
========================================
Executing task: Clean
Deleting directory C:/tmp/myNewBlog/_site
Finished executing task: Clean

========================================
DownloadPretzel
========================================
Executing task: DownloadPretzel
Downloading file: https://github.com/Code52/pretzel/releases/download/v0.4.0/Pretzel.0.4.0.zip
Downloading file: 5%
Downloading file: 10%
Downloading file: 15%
Downloading file: 20%
Downloading file: 25%
Downloading file: 30%
Downloading file: 35%
Downloading file: 40%
Downloading file: 45%
Downloading file: 50%
Downloading file: 55%
Downloading file: 60%
Downloading file: 65%
Downloading file: 70%
Downloading file: 75%
Downloading file: 80%
Downloading file: 85%
Downloading file: 90%
Downloading file: 95%
Downloading file: 100%
Download complete, saved to: Tools/Pretzel.zip
Finished executing task: DownloadPretzel

========================================
UnzipPretzel
========================================
Executing task: UnzipPretzel
Unzipping file C:/tmp/myNewBlog/Tools/Pretzel.zip to C:/tmp/myNewBlog/Tools/Pretzel
Deleting file C:/tmp/myNewBlog/Tools/Pretzel.zip
Finished executing task: UnzipPretzel

========================================
Only-Bake
========================================
Executing task: Only-Bake
starting pretzel...
bake - transforming content into a website
Recommended engine for directory: 'liquid'
done - took 271ms
Exit code: 0
Finished executing task: Only-Bake

========================================
Bake
========================================
Executing task: Bake
Finished executing task: Bake

========================================
Default
========================================
Executing task: Default
Finished executing task: Default

Task                          Duration
--------------------------------------------------
Clean                         00:00:00.0098652
DownloadPretzel               00:00:08.4448568
UnzipPretzel                  00:00:00.1900319
Only-Bake                     00:00:00.4412274
Bake                          00:00:00.0039579
Default                       00:00:00.0040178
--------------------------------------------------
Total:                        00:00:09.0939570
```

When we launch the `Taste` Target it will look like this:

```powershell
.\build.ps1 -Target Taste
```

```cmd
Preparing to run build script...
Running build script...
Analyzing build script...
Processing build script...
Compiling build script...

========================================
Clean
========================================
Executing task: Clean
Deleting directory C:/tmp/myNewBlog/Tools/Pretzel
Deleting directory C:/tmp/myNewBlog/_site
Finished executing task: Clean

========================================
DownloadPretzel
========================================
Executing task: DownloadPretzel
Downloading file: https://github.com/Code52/pretzel/releases/download/v0.4.0/Pretzel.0.4.0.zip
Downloading file: 5%
Downloading file: 10%
Downloading file: 15%
Downloading file: 20%
Downloading file: 25%
Downloading file: 30%
Downloading file: 35%
Downloading file: 40%
Downloading file: 45%
Downloading file: 50%
Downloading file: 55%
Downloading file: 60%
Downloading file: 65%
Downloading file: 70%
Downloading file: 75%
Downloading file: 80%
Downloading file: 85%
Downloading file: 90%
Downloading file: 95%
Downloading file: 100%
Download complete, saved to: Tools/Pretzel.zip
Finished executing task: DownloadPretzel

========================================
UnzipPretzel
========================================
Executing task: UnzipPretzel
Unzipping file C:/tmp/myNewBlog/Tools/Pretzel.zip to C:/tmp/myNewBlog/Tools/Pretzel
Deleting file C:/tmp/myNewBlog/Tools/Pretzel.zip
Finished executing task: UnzipPretzel

========================================
Only-Taste
========================================
Executing task: Only-Taste
starting pretzel...
taste - testing a site locally
Recommended engine for directory: 'liquid'
Opening https://localhost:8080/ in default browser...
Press 'Q' to stop the web host...
/
/css/style.css
/img/logo.png
/img/25.png
```
> Make sure port 8080 is free and open on your machine

Okay our build pipeline is ready, so lets hop over to Visual Studio Team Services

## Visual Studio Team Services

First lets add the `tools/` folder to out `.gitignore` file.

```txt
_site/
.sass-cache/
.jekyll-metadata
tools/
```

And commit our changes

```cmd
git add .
git commit -m "added cake build script"
```

### Get started for Free

I will now cover how to get access to the VSTS. Follow the [instructions](//www.visualstudio.com/en-us/products/visual-studio-team-services-vs.aspx) to get an account.

> I found this [blog post](https://blogs.msdn.microsoft.com/visualstudioalm/2016/02/10/get-your-code-hosted-for-free-in-vsts/) to get ready and running.

Create a new Team Project and use Git for the Version Control.

![Visual Studio Team Project](/img/posts/2016/vsts1.png)

> Note i skipped that step, cause i already have a team project

To build our blog we need to store it on a remote git repository. You can use [github](//github.com) for this. I will use the TFS build in one, but only the source mapping will change.

Go to the code Tab, and add a new repository. Choose Git and a name, hit create.

![Visual Studio Team Project](/img/posts/2016/vsts2.png)
![Visual Studio Team Project](/img/posts/2016/vsts3.png)

We will see the following page.

![Visual Studio Team Project](/img/posts/2016/vsts4.png)

Grab the code from Push an exising repository and invoke it on the command line. Hit refresh in the browser:


![Visual Studio Team Project](/img/posts/2016/vsts5.png)

Awesome! Now we have our source code online and can build it using the new build engine VSTS offers.

### Build it

Hit the build setup now button:

![Visual Studio Team Project](/img/posts/2016/vsts6.png)

Lets start with an empty build hit next:

![Visual Studio Team Project](/img/posts/2016/vsts7.png)

Check `Continuous integration` and hit Create:

![Visual Studio Team Project](/img/posts/2016/vsts8.png)

Technically we got everything to build our site, but the new build system is so awesome, lets checkout the Marketplace to build it even fancier!

![Visual Studio Team Project](/img/posts/2016/vsts9.png)

Search for the cake extention, hit it and click on install:

![Visual Studio Team Project](/img/posts/2016/vsts10.png)
![Visual Studio Team Project](/img/posts/2016/vsts11.png)

Go through the installation process and go back to your build.

Hit Add build step and add 4 steps: `Delete Files` `Cake` `Copy Files` and `Publish Build Artifacts`

![Visual Studio Team Project](/img/posts/2016/vsts12.png)

#### Parameters

The Delete Files should take the following parameters:

Contents:

```cmd
_site/**
tools/**
```

The Cake step is ready to go.

For the Copy Files Step we need the following.

Source Folder

```cmd
$(build.sourcesdirectory)
```

Contents:

```cmd
_site\**
publish.ps1
```

Target Folder:

```cmd
$(build.artifactstagingdirectory)
```

For the Publish Build Artifacts step we need the following:

Path to Publish:

```cmd
$(build.artifactstagingdirectory)
```

Artifact Name:

```cmd
drop
```

Artifact Type:

```cmd
Server
```

All build steps should now look something like this:

![Visual Studio Team Project](/img/posts/2016/vsts13.png)
![Visual Studio Team Project](/img/posts/2016/vsts14.png)
![Visual Studio Team Project](/img/posts/2016/vsts15.png)
![Visual Studio Team Project](/img/posts/2016/vsts16.png)


Hit save and give the build a name:

![Visual Studio Team Project](/img/posts/2016/vsts17.png)

Queue a new Build and lets look:

![Visual Studio Team Project](/img/posts/2016/vsts18.png)

Awesome all green!

Lets check the artifacts:

![Visual Studio Team Project](/img/posts/2016/vsts19.png)

Everything is perfect. We now have a build pipeline that will trigger our builds as soon we push changes to remote repository!

Theoretically we are good to go, download the artifacts and upload the changes to some Webserver, but i'd like to have a full working pipeline to preview my changes on a real server, before i upload them.

The next time we will use some Powershell, Azure Websites and the new [Release Management](//msdn.microsoft.com/Library/vs/alm/Release/overview) from VSTS to publish our Blog to Test/Production websites.

The source-code in available on [github](//github.com/biohazard999/migrating-from-funnelweb-to-pretzel) i will provide a [seperate branch](//github.com/biohazard999/migrating-from-funnelweb-to-pretzel/tree/part2) for every post. Master contains the most recent one.

Greetings Manuel