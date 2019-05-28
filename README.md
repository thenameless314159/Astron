# ![](https://www.shareicon.net/data/32x32/2016/11/09/851508_planet_512x512.png) Astron [![Build Status](https://travis-ci.com/thenameless314159/Astron.svg?branch=master)](https://travis-ci.com/thenameless314159/Astron) [![NuGet Badge](https://buildstats.info/nuget/astron)](https://www.nuget.org/packages/Astron/)
**.NET Core 2.2** from scratch set of library using last feature of C# language. This project was made by myself under the direction of [*DrBrook*](https://github.com/DrBrooklyn "DrBrook") for learning purpose. 

The main goal of this project is to provide some **scalable**, **easy-to-use**, **easily configurable** and **extandable *APIs*** with very little to implement by yourself in order to have a proper application structure with only from-scratch implementations that aren't blackbox like manythings in the BCL. There are many library provided within this project such as *binary reader/writers*, *auto-generated serializers*, *packet size calculation* and even more. Everything have been implemented using the last features from the .NET Core framework.

## Table of Contents ##
- [Overview](#overview)
- [Get started](#get-started)
- [Contributing](#contributing)
- [Contact](#contact)
- [Credits](#credits)
- [License](#license)


## Overview
These libraries were firstly designed to fit a game back-end server therefore we didn't wanted any dependency on our project and tried to implement as much as we could by ourselves. Having them **easily maintenable** was the point for this project. This is why each assembly have its own matching unit-test project in the *tests/* folder.

Most of the features you need are **already implemented** (such as logger, IoC container, auto-generated serializer/size-calculation expression from a POCO class etc...) but if you have any advanced serialization logic such as custom var, custom types etc.. you won't have to implement many things to have a working serialization logic.
  
#### Unit tests  
![UnitTests](https://i.imgur.com/KvknsXi.png)
  
#### Project structure  
| Assembly Name        	|                       Description                       	| Dependencies                     	|
|----------------------	|:-------------------------------------------------------:	|----------------------------------	|
| Astron.Binary        	| Binary reader & writer extandable API with offset logic 	|                     Memory, Size 	|
| Astron.Expressions   	|        Expression tree wrapper with some helpers        	|                             none 	|
| Astron.IoC           	|       IoC container for dependency injection logic      	|                          Logging 	|
| Astron.Logging       	|                 Lightweight logging API                 	|                             none 	|
| Astron.Memory        	|         Memory policy with base implementations         	|                             none 	|
| Astron.Serialization 	|     Auto-generated serializer/deserializer from POCO    	|              Binary, Expressions 	|
| Astron.Size          	|    Auto-generated size-calculation methods from POCO    	|                      Expressions 	|
  
## Get Started

If you need any more informations about how to setup and implement each library, [**the full documentation**](https://github.com/thenameless314159/Astron/wiki) is an awesome place to start.

Some **sample projects** will be released in the future, I highly recommend you to *follow me* on [my github page](https://github.com/thenameless314159) if you want to be notified once they'll be released, also you should consider *starring* this repo, because it'll be really appreciable. A network library using the last Pipeline API from .NET Core 2.2 is also planned to be released !

Each assembly have been uploaded on nuget separetely, but there is an all-in-one package which you can find at the top of this document near the build status badge. Here are the badges for each seperate assembly :

##### Astron.Binary
[![NuGet Badge](https://buildstats.info/nuget/astron.binary)](https://www.nuget.org/packages/Astron.Binary/)
##### Astron.Expressions
[![NuGet Badge](https://buildstats.info/nuget/astron.expressions)](https://www.nuget.org/packages/Astron.Expressions/)
##### Astron.IoC
[![NuGet Badge](https://buildstats.info/nuget/astron.ioc)](https://www.nuget.org/packages/Astron.IoC/)
##### Astron.Logging
[![NuGet Badge](https://buildstats.info/nuget/astron.logging)](https://www.nuget.org/packages/Astron.Logging/)
##### Astron.Memory
[![NuGet Badge](https://buildstats.info/nuget/astron.memory)](https://www.nuget.org/packages/Astron.Memory/)
##### Astron.Serialization
[![NuGet Badge](https://buildstats.info/nuget/astron.serialization)](https://www.nuget.org/packages/Astron.Serialization/)
##### Astron.Size
[![NuGet Badge](https://buildstats.info/nuget/astron.size)](https://www.nuget.org/packages/Astron.Size/)

## Contributing
Please, be aware that there is a [**Code of Conduct**](https://dotnetfoundation.org/code-of-conduct) that have been defined by the .NET foundation and adopted by this repository.
  
Be sure to look at [*currently openened issues*](https://github.com/thenameless314159/AstronCore/issues) to know what is going on. If you want to make a contribution, you must contact me before !

## Contact
**Discord:** NamelessK1NG#3577  
**Mail:** thenameless314159@gmail.com

## Credits
[**DrBrook**](https://github.com/DrBrooklyn "DrBrook") - My teacher and also a long-time friend, this project would never happen without him.  
[**McGravell**](https://github.com/mgravell "McGravell") - For his works with the new "Pipelines" API of .NET Core 2.0

## License
Take a look at [the license](https://github.com/thenameless314159/Astron/blob/master/LICENSE) for further informations.
