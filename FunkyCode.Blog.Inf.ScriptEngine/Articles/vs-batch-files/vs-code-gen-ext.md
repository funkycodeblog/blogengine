# Running batch file from Visual Studio

<!-- Id: vs-batch-file -->
<!-- Categories: Visual Studio  -->
<!-- Date: 20200321 -->

<!-- #header -->
Executing batch file from Visual Studio isn't shipped  require some quick operations
<!-- #endheader -->

Select Tools -> External tools …

Create a tool with parameters as below:

![01](01.png)

Here’s code for you to copy:
<Table>
<tr><td>Title:</td><td>Run Batch File</td></tr>
<tr><td>Command:</td><td>C:\Windows\System32\cmd.exe</td></tr>
<tr><td>Arguments:</td><td>/C$(ItemPath)</td></tr>
<tr><td>Initial directory:</td><td>$(ItemDir)</td></tr>
</Table>

Focus on a batch file you want to launch.

New item in Tools menu will be available.

![02](02.png)



