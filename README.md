# SQLCrypt
## Cliente SQL para Desarrolladores

***

### **Autor:** _Cristian Solervic�ns C._

***

[Wiki del proyecyo](https://github.com/CristianSolervicens/SQLCrypt/wiki)

***

### **NOTE:**
The Application requires .NET Framework 4.8 otherwise it will fail !

### **Comentarios:** 

Este es un antig�o proyecto de Cliente SQL, a veces lo he dejado por a�os, pero recientemente le agregu�
una versi�n de [Scintilla.NET](https://github.com/jacobslusser/ScintillaNET) de jacobslusser  y ha vuelto
a capturar mi atenci�n y mi tiempo.

### Descripci�n:
Desde mi perspectiva de desarrollador y administrador de Bases MS - SQL Server siempre ha requerido, por
ejemplo buscar todos los SP's que hacen menci�n a alguna tabla o que usan una determinada funci�n, no es
algo commplejo, pero requiere estar escribiendo algo de c�digo y revisar el c�digo del objeto.
Otras veces requiero revisar r�pidamente si hay un bloqueo en la Base, o qu� procesos se est�n ejecutando, o
si existen transacciones abiertas, etc.

Este peque�o proyecto viene en mi ayuda, es simple, se ejecuta muy r�pido, me permite buscar r�pidamente los
objetos de una Base por su nombre o texto contenido, ac� puedo organizar y ejecutar mis "consultas habituales"
con s�lo un par de "clicks", adem�s la salida de las consultas SQL las puedo grabar como
["Excel"](https://www.epplussoftware.com/) (usando EPPLUS) o como json, si luego quiero trabajarlas,
por ejemplo, en un jupiter notebook de Python _(Esto lo he usado mucho para revisar las Auditor�as del 
SQL Server, ya que a trav�s de queries m�s complejas se demora demasiado)_.

Pero ahora, adem�s se ha convertido en un buen editor de SQL, permite el uso y creaci�n de ***snippets***,
el copiado r�pido de las columnas de una tabla, completaci�n y match de par�ntesis, bookmarks...

Las consultas que est�n en el editor las resuelve en una hebra aparte y levantando una nueva conexi�n a la Base
de Datos, por lo que puedes seguir trabajando dentro de la ventana principal, la salida soporta
***m�ltiples "result-sets"*** y la separaci�n de comandos a
trav�s de **GO**

[SQLCrypt Download](https://github.com/CristianSolervicens/SQLCrypt/actions/runs/10336572323/artifacts/1798612737)

Cristian Solervic�ns.