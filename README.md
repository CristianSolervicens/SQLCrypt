# SQLCrypt
## Cliente SQL para Desarrolladores

***

### **Autor:** _Cristian Solervicéns C._

***

[Wiki del proyecyo](https://github.com/CristianSolervicens/SQLCrypt/wiki)

***

### **NOTE:**
The Application requires .NET Framework 4.8 otherwise it will fail !
Best way is to install the program in a personal folder due to config files, snippets and Immediate Comands

### **Comments:** 

This is a very old project for a SQL Server Client, I started it in VB with C dll's and some years ago I 
ported it to C#, a few time ago I add to it Scintilla, and I fall in love again with it...
[Scintilla.NET](https://github.com/desjarlais/Scintilla.NET) from jacobslusser and now desjarlais and now
captured my attention again.

### Description:
From my devaloper and MS SQL Server DBA perspective I think SSMS is a fantastic tool, but many times I need
more features (and a lightweight tool), I usualy need to find a Stored procedure or function by their
content, sometimes by a table name, or a function inside...
Sometimes I need to run a script to realize what's going inside the DB Server (to find open transactions,
queries in execution, etc.)...

An then this app is here to help me!!!
It's simple and lighteight, but has what I need, I can save in a folder my Life Savers scripts, who's
connected, how is the PLE?, running queries, blocking queries, and with parameters!!
I can find procedures by content or name, functions, views, triggers (I hate them) and I can see them
really fast and easily. 

["Excel"](https://www.epplussoftware.com/) (with EPPLUS) very often I need to send information from the
database, with this app I can save the resultsets as Excel or json, and I can extract usefull information
with a small python app or in a jupiter notebook _(I've use this to analize SQL Server Audits, checking 4 
month audits is not an easy task!!!)_.

And now, with scintilla is algo a good SQL editor, and now it include ***code snippets***, copy table
names and column names to the editor, parenthesis matching, auto close parenthesis and other stuff.

You can edit your file while you're executing a query, query output is very fast, and support large
resultsets and many resultsets. While a query is running you can still use your stored queries and object
navigation.

The sentences separated by **GO** are executed together.


Cristian Solervicéns.
