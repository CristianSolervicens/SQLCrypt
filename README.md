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
more developer and some day to day admin features (and also a lightweight tool), I usualy need to find a
Stored procedure or function by their content, sometimes by a table name, or a function inside...

Sometimes I need to run a script to realize what's going inside the DB Server (to find open transactions,
queries in execution, etc. and NOW!!)... Yes with this app I can easily catalog my most used queries and
execute them with a shortcut from the app.

This app is here to help me!!!

It's simple and lighteight, and has what I need, I can save in a folder my Life Savers scripts, who's
connected, how is the PLE?, running queries, blocking queries, and if I need, with parameters!!
I can find procedures by content or name, functions, views, triggers (I hate them).

["Excel"](https://www.epplussoftware.com/) (with EPPLUS) very often I need to send the result of a query,
with this app I can save the resultsets as Excel or json, and I can extract usefull information
with a small python app or in a jupiter notebook _(I've used json output to analize SQL Server Audits,
to review and alalize 4 month of audits, even with excel is not an easy task!!!)_.

And now, with Scintilla Control is also a good SQL editor, it includes ***code snippets***, copy table
names and column names (when more than one column it includes then separated with comas) to the editor,
parenthesis matching, auto close parenthesis and other stuff.

You can edit your file while you're executing a query, query output is very fast, and support large
resultsets and many resultsets. While a query is running you can still use your stored queries and object
navigation.

The sentences separated by **GO** are executed separatelly.


Cristian Solervicéns.
