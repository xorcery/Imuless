![alt tag](https://github.com/imulus/Imuless/blob/master/Assets/imuless.png)

Imuless
=======

A way to integrate the http://lesscss.org/ NodeJs compiler into Umbraco Themeing.

 - Create themes the usual way with Less syntax.
 - Use a property editor to expose override Less variables.
 - On publish of the node with the Imuless property editor, the NodeJs lessc process runs.
 - Supports multisite setups.
 - Not for the faint of heart, this repo is for persons who are medium to advanced developers.

### Install

This is a raw new package and is therefore not installable via the usual channels (NuGet/Umbraco Package).

To manually install:
- Build the solution and drop in your bin.  You may also simply download the current release DLL.
- Add an `Imuless` folder to your `App_Plugins`.  Then drop in the property editor contents (css, js, views, manifest).
- Ensure you add all web.config options described below.
- Configure your `imulus.config.js` file in the newly created `Imuless` folder.  Make sure to name your JSON alias exactly like your Less vars.
- Add `<link>` tag that points to `/css/<currentDomain>.css`.
- Setup your Less file structure as described below.
- Ensure you have Node.js installed on your server.
- Install Less `npm install -g less`.
- You will need to take note of where the `lessc.cmd` is installed on your server as you'll need to point to this in the web.confg.  It is also important make sure that file can be executed by the web process (i.e. permissions).

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

A sample configuration (`/App_Plugins/Imuless/js/imuless.config.js`) would look like this:

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

