# ![](https://www.shareicon.net/data/32x32/2016/11/09/851508_planet_512x512.png) AstronCore [![Build Status](https://travis-ci.com/thenameless314159/AstronCore.svg?token=r6UpDpoyLDxvQzKZj12r&branch=master)](https://travis-ci.com/thenameless314159/AstronCore)
**.NET Core 2.2** from scratch set of library using last feature of C# language. This project was made under the direction of [*DrBrook*](https://github.com/DrBrooklyn "DrBrook") for learning purpose. 

The main goal of this project is to provide **scalable**, **easy-to-use** and **easily configurable** implementations for a networking infrastructure such as binary reader/writers, auto-generated serializers, packet size calculation and more. All of that with the last features from the .NET Core framework.

**Warning : This project is a WIP !**

## Table of Contents ##
- [Overview](#overview)
- [Get started](#get-started)
- [Contributing](#contributing)
- [Contact](#contact)
- [Credits](#credits)
- [License](#license)


## Overview
These libraries were firstly designed to fit a game back-end server therefore we didn't wanted any dependency on our project. Each assembly have its matching unit-test project in the *tests/* folder.

| Assembly Name        	|                       Description                      	| Dependencies        	|
|----------------------	|:------------------------------------------------------:	|---------------------	|
| Astron.Binary        	| Provides binary reader & writer with position handling 	| Memory, Size        	|
| Astron.Expressions   	|            Some helpers for expression trees           	| none                	|
| Astron.Immutables    	|               Some helpers for immutables              	| none                	|
| Astron.IoC           	|      IoC container to handle dependency injection      	| Immutables, Logging 	|
| Astron.Logging       	|                    Logging utilities                   	| none                	|
| Astron.Memory        	|         Memory policy with base implementations        	| none                	|
| Astron.Serialization 	|    Auto-generated serializer/deserializer from POCO    	| Binary, Expressions 	|
| Astron.Size          	|                    Size calculation                    	| Expressions         	|

## Get Started

**Todo:** create page for each assembly with docs

## Contributing
Please, be aware that there is a [**Code of Conduct**](https://dotnetfoundation.org/code-of-conduct) that have been defined by the .NET foundation and adopted by this repository.
  
Be sure to look at [*currently openened issues*](https://github.com/thenameless314159/AstronCore/issues) to know what is going on. If you want to make a contribution, you must contact me before !
## Contact
**Discord:** NamelessK1NG#3577
## Credits
[**DrBrook**](https://github.com/DrBrooklyn "DrBrook") - My teacher and also a long-time friend, this project would never happen without him.  
[**McGravell**](https://github.com/mgravell "McGravell") - For his works with the new "Pipelines" API of .NET Core 2.0

## License
See [the license](https://github.com/thenameless314159/Astron/blob/master/license.md).
