# SQLCrypt
## SQL Client for Developers

***

### **Author:** _Cristian Solervicéns C._

***

[Project Wiki](https://github.com/CristianSolervicens/SQLCrypt/wiki)

***

### **NOTE:**
The Application requires .NET Framework 4.8 otherwise it will fail !
Best way is to install the program in a personal folder due to config files, snippets and Immediate Comands

### **Comments:** 

I started this project many years ago with Vb 6.0 and some dll's written in "C", then I ported it to C# with
Rich Text as the Editor, the past year I add to it the Scintilla Control, and I fall in love again with the
project !!!.

[Scintilla.NET](https://github.com/desjarlais/Scintilla.NET) from jacobslusser and now "Desjarlais", a great
project.

### Description:
From my developer and MS SQL Server DBA perspective I think SSMS is a fantastic tool, but many times I need
more "developer features" and also some day to day admin features (and also a lightweight tool).

**Find: Procedures, Functions, Triggers by their Content, Tables or Views by their Columns**

Sometimes I need to analize the impact of some change in the DB, and those features are really nice to have!

**What's going on with my Database!**

Why the DB is running so slow?, I can easily catalog my Life Savers scripts... who's connected?, how is the PLE?,
queries in execution ordered by time, cpu, reads?, blocking queries? I can run those queries with a shortcut within
the app.


**Outputo to ["Excel"](https://www.epplussoftware.com/) (with EPPLUS) and JSON.**

Very often I need to send the result of a query, with this app I can save the resultsets as Excel or json,
With **JSON** is easy to analize data with python app or Jupiter Notebook _(I've used json output to analize
SQL Server Audits, to review and analize 4 month of audits, even with excel is not an easy task!!!)_.


**Good SQL Editor**

Now, with **Scintilla Control** this app is also **a good SQL editor**, that includes ***code snippets*** 
(you can add yours), copy table names and column names (when more than one column it includes them separated
with comas) to the editor, parenthesis matching, column editing, auto close parenthesis and other stuff.

**Syntax Checking and SQL Formatting**

With TransactSql.ScriptDom the app checks SQL Syntax before executing or "On Demand", and it also re-format
SQL Code (the all file or just the selected text).


**Data Dictionary and Other stuff**

You Can Edit the extended properties in an Database Objects oriented way (by example Table and all their columns in one
screen), and with my other app, ["DataBaseDictionary.Net"](https://github.com/CristianSolervicens/DataBaseDictionary.Net)
using the Extended Properties you entered, you can create the "Data Dictionary" in HTML format. 

You can edit your file while you're executing a query, query output is very fast, and support large
resultsets and many resultsets. While a query is running you can still use your stored queries and object
navigation.

You can execute multiple sentences, and those separated by **GO** are executed separatelly.


_**I Hope this project can be useful for someone else.**_


Cristian Solervicéns.
