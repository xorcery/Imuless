Imuless
=======

A way to integrate the http://lesscss.org/ NodeJs compiler into Umbraco Themeing.

Create themes the usual way with Less syntax.

Use a property editor to expose override Less variables.

On publish of the node with the Imuless property editor, the NodeJs lessc process runs.

### Configurable items in the web.config

    <add key="imuless:doctypeAlias" value="SiteSettings"/>
    <add key="imuless:propertyAlias" value="theme"/>
    <add key="imuless:debug" value="true"/>
    <add key="imuless:workingDirectory" value="~/assets/stylesheets/themes"/>
    <add key="imuless:outputDirectory" value="~/css"/>
    <add key="imuless:domainVarsDirectory" value="Domain Vars"/>
    <add key="imuless:rootLessFile" value="application.less"/>
    <add key="imuless:pathToLessc" value="<serverPathToLess>/lessc.cmd"/>
