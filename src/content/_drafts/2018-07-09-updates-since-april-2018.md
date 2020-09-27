---
 layout: post 
 title: Updates since April 2018
 comments: true
 tags: [Ranorex, Webtestit, Selenium, TypeScript, UITest, XAF, Windows10, DesktopBridge, APPX, Deployment, Misc, Chromely, Blaster, Blazor, Electron, Patreon, Chromium, Scissors, OpenSource, GitHub, Personal, Retro, Recap]
---
Since my [last post]() a few months past by, so I wanted to provide a recap what happened since i [joined Ranorex](). This is in no particular order, or importance, it's more a recap and update for my [patreons]() and my other readers. Also it's a rather long time frame to get the order of things happened right. I think it's more important *what* changed and *why* not *when* (but maybe this is worth it's own blog post).  

So let's look at some aspects. I'll try to use a kinda retrospective approach:

<table width="100%">
    <tr><th>Key</th><th>Why</th><th>Description</th></tr>
    <tr><td>WNC</td><td>What I like</td><td>Would not change</td></tr>
    <tr><td>SC</td><td>what we should change in my opinion</td><td>Should change</td></tr>
    <tr><td>A</td><td>what we can do in my opinion</td><td>Action</td></tr>
</table>

So this will give you some direction what I am thinking on my own, and where I am heading into next.

## Ranorex / My daily bread

Okay, this is a huge topic, but I'll keep it short: It's awesome \m/.  
Working on tools for developers and testers was always a dream of mine. Doing this with a team of awesome people is even more enjoyable and also challenging. Working with designers, testers, other developers, marketing, corporate leaders all at once can be challenging, but it's amazing what you can achieve with people collaborating in a open and own paced way.  

### General

### Technical

#### TypeScript

Okay it's now about 3 months since i [joined Ranorex]() and since then I develop in [TypeScript](). *Leaving* .NET land was hard, but it was worth the effort. I like a lot about TS, cause C# lacks some functional programing approaches I know from F# and other functional brothers. But of course it's not perfect. I stumbled upon [PureScript]() a few days ago, so I will definitely look into that.  
Sometimes i miss C# though. :)

* WNC
  * TypeScript's control flow based type analysis!
  * 'Script like' but type safe by default!
  * NPM is **HUGE**!
  * Ultra easy patching of external dependencies (yes i look at you [nuget]())!
  * Pattern matching!
  * Runtime performance, cause it's JavaScript at the end!
* SC
  * More functional type safety (yes i look at you lodash)
  * TypeScripts type system sometimes gets in your way, especially with external libraries
  * Force immutability in some way like null checks
  * Npm is sometimes briddle. It breaks, when you don't need it to break (for example on release day)
  * Quality of external dependencies / Semver does not work all the time
* A
  * Be aware of dependency hell
  * Be careful about dependencies (and look out for dependencies of dependencies)
  * For libraries written in JavaScript: Add the type definitions beforehand you use the library. Don't be lazy, it's *really expensive* to do it later. Do it upfront, this will save you. From **yourself**.
  * Contribute more to open source directly. Don't be shy, they don't bite.

Most of the things I *complain* about, is more on a ecosystem level. [NPM](), [Node]() and the whole ecosystem around it is rather new to me, so it's maybe more a lack of experience by me. A lot in this ecosystem is very lean and pragmatic though, which I like, but it's not that mature yet, so there are *dragons* sometimes.

#### Selenium

I did some work with [selenium]() in the past, but was rather disappointed after some time. UI tests are *hard*. Test the right things, the right way is *even harder*. But time goes by, and the web changes fast. We passed the [jQuery age]() and entered [bootstrap]() and [accessibility age](). We got CSS selectors that are [sane](). Selenium client libraries are available in almost every language. Some more [sane](), some [fewer](). So let's recap.

* WNC
  * It's mature, but it's reliable!
  * It's fast!
  * **HUGE** [language support]()!
  * All browsers are on board for WebDriver, even [Edge]() and [Windows]()!
* SC
  * Almost every language has found **different** more or less different characteristic flavors of some patterns. If something works with java, it does not work in protractor for example.
  * Drivers behave weird sometimes
  * Debugging is not baked in
* A
  * Community should work hard together if it still care about selenium
  * More open source utility libraries that easy the use of selenium, for example of using it with various frontend frameworks (bootstrap, vue, ect...)
  * Driver ports to different platforms?

Selenium and especially WebDriver are awesome pieces of software. They are based on open standards. But testers *are not developers* (take this with a gran of salt). Testing is *hard*. Blogging about testing is *even harder*. Writing testing libraries and apply testing patterns on a library level is *holy grail hard*.  
But we should move forward as an industry. Testing **[is part of production]()**. So let's treat test code (esp UI tests) not as the [little fat cousin]() of production.  
Write libraries, put them on [github]()! Talk on conferences about it. There is nothing to be ashamed of being a tester :)

#### Webtestit Beta

This should not be a commercial, but my heart and my *soul* is in this product. 💓💓💓

Short introduction first. [Ranorex Webtestit]() is an IDE for writing E2E (web) tests.  
It's lean, cross platform, based on the [shoulder of giants](). It's code and keyboard centric. It's designed to remove boilerplate and let you focus on the problem.  

*Think of it like VSCode for e2e tests.**

 You can see an example of the tests created on [github](). I've gone the extra mile to write those tests in [java]() and in [typescript with protractor](). And what's the best about this? I've found a bug in my blog.

As you maybe know, this is written in [pretzel](). I've added some features about [tag search]() and [searching]() in [the archive](). Naive implemented, but it does the job. An it was broken on half the browsers. I **did** test it on all browsers when I implemented it first, but **guess**, after adding some small *tweaks* it broke. And I didn't know cause I did not test again.  
Using [Webtestit]() I was able to test it on [all browsers]() with ease and found out it worked only in Chrome (sorry other 40% of my readers, yes the number is correct). I've integrated in into my CI/CD pipeline in minutes, so this will never happen again cause of a *tweak* I've done in my code.

Stop complaining. Start testing. Yes even you are an [alpha-dev](). So let's recap.

* WNC
  * Cross platform, so everybody can use it (backend, frontend, ect. no excused)
  * Multiple languages supported, no excuse for [leaving your comfort zone]()
  * Fast, reliable, carefully designed
  * Drives you to use [patterns]() but does not force you to
  * [PageObject's]() build in
  * Open, customizable, flexible.
  * Code centric, but not code only.
  * Almost weekly updates / new stuff at your fingertips
  * Community driven
* SC
  * ???
  * Don't know it's perfect
  * Just kidding, still ???
  * <div style="width:100%;height:0;padding-bottom:56%;position:relative;"><iframe src="https://giphy.com/embed/3o7btPCcdNniyf0ArS" width="100%" height="100%" style="position:absolute" frameBorder="0" class="giphy-embed" allowFullScreen></iframe></div><p><a href="https://giphy.com/gifs/cbc-comedy-what-3o7btPCcdNniyf0ArS"></a></p>
* A
  * You opinion counts & decides!

I really 💓 this product. But I was part of building this beauty. So I really care what you think about it.  
For the language part: **More languages** are coming! Hang out C# guys! ;)

> If you want to try out the product for free, head over to [our website]() and grab a copy!  
I'd love to hear your feedback💓

## OpenSource

I was more active on github in the last the last year than ever!  
To be precise **273** contributions!

![Github graph with 273 contributions](/img/posts/2018/2018-07-12-updates-since-april-2018-github-graph.png)

But this year was a little bit different, cause I didn't do *own* projects, but also filed a lot of issues, discussed a lot with the community and did a bunch of pull requests as well!
So let's have a look at some projects I'm currently focusing on.

### Chromium/Electron

The web is moving fast and desktop developments is hard. Especially when you try to focus on all major desktop platforms (Windows, MacOs and Linux). [PWA's]() are on the horizon, but not ready for primetime yet. But developing desktop applications with web technology is not impossible. So that's where electron and the chromium project jumps in.  
Electron works very well, but it's rather resource intensive. With [dotnet core]() we now have cross platform [dotnet]() and with the power of [WebAssembly]() we have [blazor](), which enables dotnet in the browser.  
So let's look at some new projects I am working on.

#### Chromely

[Chromely]() is a light weight wrapper for chromium. It's cross platform cause it's based on dotnet core. It's lean by design, trying to remove as much boilerplate as possible.

* WNC
  * Cross platform
  * Super fast
  * Lean
  * Easy API
  * Not so inflated than [Electron]() or [Electron.net]()
* SC
  * Small number of contributors
  * Build server
  * Getting it in a "nuget install" start working experience
  * Get MacOS running
* A
  * Get more people involved
  * Write some real world products on top of it

I'll write a follow up post on Chromely in the future!

#### Blazor

[Blazor]() is an experimental UI framework that combines the power of Mono, .NET Standard and WebAssembly 

#### Blaster

## DevExpress/XAF/XPO

### Release 18.1.15

### Scissors.FeatureCenter


## Community

### MVP-Program

### Patreon


## Personal

### Birthday

### Vacation

## Recap/Focus