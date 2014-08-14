Imuless
=======

A way to integrate the http://lesscss.org/ NodeJs compiler into Umbraco Themeing.

 - Create themes the usual way with Less syntax.
 - Use a property editor to expose override Less variables.
 - On publish of the node with the Imuless property editor, the NodeJs lessc process runs.
 - Supports multisite setups.

### File Structure

The image below shows an example Less file structure, this can be configured to use a different via the web.config:

![alt tag](https://github.com/imulus/Imuless/blob/master/Assets/lessstructure.png)

Your theme files live under a folder named after the them and use (by default) a root file called `application.less`.  Inside this file you can use common libraries by simply using `@import`'s.  You can even inherit from other themes by referencing the other theme via an `@import`.

The folder `Domain Vars` holds the overrided variables and will be included at the end of the `application.less` file dynamically.

### Configurable items in the web.config

    <add key="imuless:doctypeAlias" value="SiteSettings"/>
    <add key="imuless:propertyAlias" value="theme"/>
    <add key="imuless:debug" value="true"/>
    <add key="imuless:workingDirectory" value="~/assets/stylesheets/themes"/>
    <add key="imuless:outputDirectory" value="~/css"/>
    <add key="imuless:domainVarsDirectory" value="Domain Vars"/>
    <add key="imuless:rootLessFile" value="application.less"/>
    <add key="imuless:pathToLessc" value="<serverPathToLess>/lessc.cmd"/>
