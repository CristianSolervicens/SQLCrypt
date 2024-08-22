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

### **Comentarios:** 

Este es un antigüo proyecto de Cliente SQL, a veces lo he dejado por años, pero recientemente le agregué
una versión de [Scintilla.NET](https://github.com/jacobslusser/ScintillaNET) de jacobslusser  y ha vuelto
a capturar mi atención y mi tiempo.

### Descripción:
Desde mi perspectiva de desarrollador y administrador de Bases MS - SQL Server siempre ha requerido, por
ejemplo buscar todos los SP's que hacen mención a alguna tabla o que usan una determinada función, no es
algo commplejo, pero requiere estar escribiendo algo de código y revisar el código del objeto.
Otras veces requiero revisar rápidamente si hay un bloqueo en la Base, o qué procesos se están ejecutando, o
si existen transacciones abiertas, etc.

Este pequeño proyecto viene en mi ayuda, es simple, se ejecuta muy rápido, me permite buscar rápidamente los
objetos de una Base por su nombre o texto contenido, acá puedo organizar y ejecutar mis "consultas habituales"
con sólo un par de "clicks", además la salida de las consultas SQL las puedo grabar como
["Excel"](https://www.epplussoftware.com/) (usando EPPLUS) o como json, si luego quiero trabajarlas,
por ejemplo, en un jupiter notebook de Python _(Esto lo he usado mucho para revisar las Auditorías del 
SQL Server, ya que a través de queries más complejas se demora demasiado)_.

Pero ahora, además se ha convertido en un buen editor de SQL, permite el uso y creación de ***snippets***,
el copiado rápido de las columnas de una tabla, completación y match de paréntesis, bookmarks...

Las consultas que están en el editor las resuelve en una hebra aparte y levantando una nueva conexión a la Base
de Datos, por lo que puedes seguir trabajando dentro de la ventana principal, la salida soporta
***múltiples "result-sets"*** y la separación de comandos a
través de **GO**

[SQLCrypt Download](https://github.com/CristianSolervicens/SQLCrypt/actions/runs/10336572323/artifacts/1798612737)

Cristian Solervicéns.
