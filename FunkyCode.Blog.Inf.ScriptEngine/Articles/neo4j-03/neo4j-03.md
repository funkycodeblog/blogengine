# Neo4j (#3) .NET Driver

<!-- Id: neo4j-03  -->
<!-- Categories: NoSql, neo4j  -->
<!-- Date: 20200319  -->

<!-- #header -->
In this post I will connect with Neo4j from .NET application.
<!-- #endheader -->


In this post I will connect with Neo4j from .NET application.

I will try to generate social network structure – one of most exemplary use case for applying Neo4j.

We will use Neo4j.Driver:

- officially supported by Neo4j
- connects to the database using the binary protocol
- it aims to be minimal – suports query execution, materialize results into .NET objects, sessions etc. – something like ADO.NET

So, let’s start.

1. Create Social database with social password.

![01](C:/Projects/Tools/BlogEngine/FunkyCode.Blog.Inf.ScriptEngine/Articles/neo4j-03/01.png)

2. Click Manage and take look at Bolt, you will need password and Bolt while connection from .NET application.

![02](C:/Projects/Tools/BlogEngine/FunkyCode.Blog.Inf.ScriptEngine/Articles/neo4j-03/02.png)

3. Open Visual Studio and install Neo4j.Driver nuget package.

![03](C:/Projects/Tools/BlogEngine/FunkyCode.Blog.Inf.ScriptEngine/Articles/neo4j-03/03.png)

4. I separated generating and executing Cypher statements. Here is generation:

