# Basic

This is a very basic [doLittle](http://www.dolittle.io) sample.

## Prerequisites

You will need to have [.NET Core](https://www.microsoft.com/net/download/core), [NodeJS](http://nodejs.org/) and [gulp](https://gulpjs.com) installed.

## Identity

Users are authenticated against an [Identity Server](http://identityserver4.readthedocs.io/).
This server needs to be running before you can log on as a user.

In the terminal from the root of the project, do the following:

```shell
$ cd Source/Identity
$ dotnet restore
$ dotnet run
```

## The Sample

Open the project in [Visual Studio Code](http://code.visualstudio.com/) by opening the root folder or [Visual Studio 2017](https://www.visualstudio.com/vs/) for Windows or Mac using the `Basic.sln` sitting in the root.

You will need to restore packages for both .NET and Node, do the following from the `./Source/Web` folder:

```shell
$ dotnet restore
$ npm install
```

The project has a build system based on top of [Gulp](https://gulpjs.com) which autowatches everything and compiles and transpiles
all artifacts - including C# and JavaScript code.

From a terminal from the root of the project do the following from the `./Source/Web` folder:

```shell
$ gulp
```

This will run all the tasks and get you up and running, any editing can now be done and just saved and it will recompile / transpile / copy.
Once it is running you can navigate to `http://localhost:5000` with your favorite browser.

### Logging in

In the Identity Server, there is a hard-coded user called `admin` - its password is the same (`admin`).
