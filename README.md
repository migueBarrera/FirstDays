# Base project for Xamarin Forms

## Este repositorio tiene un proyecto base de Xamarin.Forms que ayuda a iniciar el desarrollo de un proyecto más rápido en forma de Template para Visual Studio.
------


### Como usar la platilla 
#### Método 1
* Descarga este repositorio.
* Entra a la carpeta Template y copia el zip "PlainTemplateXF".
* Pégalo en la ubicación de las templates en Visual Studio, normalmente "C:\Users\{{YourUser}}\Documents\Visual Studio 2019\Templates\ProjectTemplates"
* Entra en Visual Studio -> Crear nuevo proyecto -> Busca la plantilla "Plain Xamarin Forms Template" -> Seleciona y pulsa crear.

#### Método 2 (No disponible aún)
* Abir en Visual Studio -> Extensions
* Buscar e instalar nuestra plantilla
* Entra en Visual Studio -> Crear nuevo proyecto -> Busca la plantilla "Plain Xamarin Forms Template" -> Seleciona y pulda crear.


### Como generar una plantilla partiendo del BaseProject
* Abrir en Visual Studio el BaseProject => FirstDays
* Una vez abierto ir uno a uno por proyecto y click derecho -> exportar como plantilla.
* Descomprimir todos los proyectos que ha creado y agrupar en una carpeta.
* Entrar en los csproj de los proyectos y renombrar "FirstDays" por "$ext_safeprojectname$" para que se cambie las referencias
* Entrar a MainPage del proyecto UWP y remplazar "FirstDays.App()" por "$ext_safeprojectname$.App()". Si no se cambiara correctamente la referencia al proyecto compartido
* Crear un archivo .vstemplate que agrupe todos los .vstemplate y los referencie
* Crear un zip de todo y copiar el zip en "C:\Users\{{YourUser}}\Documents\Visual Studio 2019\Templates\ProjectTemplates"
#### Alternativa más sencilla para crear plantilla
* Como ya tenemos una plantilla generada y preparada si tiene pocos cambios o son "controlables" se puedes hacer directamente sobre la plantilla

----------

### Próximos pasos
* Añadir a la plantilla los siguientes archivos o carpetas que ya se encuentrar en el BaseProject: CodeAnalysis, Pipelines, Directory.Build.props y .gitignore
* Automatizar la compilacion del proyecto VSIX y subida