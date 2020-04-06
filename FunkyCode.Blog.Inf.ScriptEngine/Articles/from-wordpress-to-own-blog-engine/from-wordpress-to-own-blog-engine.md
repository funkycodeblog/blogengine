# From Wordpress to my own Blog Engine

<!-- Id: from-wordpress-to-own-blog-engine -->
<!-- Categories: Blog, React, ASP.NET Core -->
<!-- Date: 20190510 -->

<!-- #header -->
After struggling a couple of months with Wordpress and leveraging React skills in the same time, I decided to leave Wordpress behind and at least try to build own Blog Engine.
<!-- #endheader -->

Wordpress is amazingly popular and number of Wordpress blogs probably counts in millions, but for me wordpressing experience was not fun at all.
Maybe I have choosen bad template, maybe I wasnt' skilled enough - never mind, but all what I remember from wordpress are problems:
* Insterting images was very clunky, although I installed several Wordpress plugins to have job done
* After changes in plugins configuration I found my code snippets spoilt, especially ```<``` and ```>``` characters were substituted with ```&lt;``` and ```&gt;``` character entities
* Although there was possibility to use own CSS in reality it was very hard (at least for me) to override settings of template
* I was tired with using in-build editor for writing articles

So I started to develop basic versions quite quickly I managed to create basic version with basic navigation features and quite decent presentation (at least not worse than in previous wordpress versions).

Here is current technology stack:

##### Client application
* TypeScript
* React 
* Redux
* Redux Thunk
* Axios
* React Router
* Material UI

##### Server application
* ASP.NET Core 3.0
* Entity Framework Core
* Swagger / Swasbuckle
* Autofac 
* Markdig

##### Scripting engine
* CommandLineParser
* LibGit2Sharp
* YamlDotNet

##### Testing
* NUnit
* NSubstitute
* Fluent Assertions

##### Azure components
* App Service
* Azure SQL Server / Database
* Application Insights

My current workflow as an author is as follows: 

1. I create article using markdown file format on my local machine.
Writting posts in markdown is very simple and effective, especially using [Markdown Editor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor), tool written by [Mads Kristensen](https://madskristensen.net/), master of extensions for Visual Studio.
It offers really great experience with inserting images. Having image in clipboard when paste is executed, File Save dialog is being opened and you can change file name - then link is created.
2. After blog is finished I use my Funky Script tool to post article to blog server. With command like this:

```code
funky-scripts upload -h <host-url> -p ./Articles/<articleFolder> -o 
```

Blog Engine source code will be baseline for some of blog article code snippets and place when new minor frameworks can be tested on feature branches.




