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


### Property Editor

The Property Editor is configured via JSON and exposes certain variables to your themes.  You may expose several or no variables for editing.

A sample configuration would look like this:

    [
        {
            "name": "Theme 1",
            "vars": [
                { "alias": "brand-info", "name": "Brand Info Color", "description": "Enter a hex value beginning with a '#'."},
                { "alias": "brand-primary", "name": "Brand Primary Color", "description": "Enter a hex value beginning with a '#'." },
                { "alias": "brand-success", "name": "Brand Success", "description": "Enter a hex value beginning with a '#'." },
                { "alias": "link-color", "name": "Link Color", "description": "Enter a hex value beginning with a '#'." }
            ]
        },
        {
            "name": "Theme 2",
            "vars": [
                
            ]
        }
    ]
    
The above configuration would create a property editor like the image below:

![alt tag](https://github.com/imulus/Imuless/blob/master/Assets/propertyeditor.png)

### Render the Theme

On publish of the node containing this property editor, all domains attached to the branch this node is on will compile into a CSS file named after the domain.

Therefore you will need to add a reference on your base template like so:

    var domainThemeFile = HttpContext.Current.Request.Url.Host + ".css";
    ...
    <link rel="stylesheet" href="/css/@domainThemeFile">`

