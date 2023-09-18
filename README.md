# UI Plugin - EOIR Reflectance Map
The EOIR Reflectance Map Generator plugin allows users to generate a reflectance map to use with Ansys' Systems Tool Kit EOIR capabilities.
STK 12.5 introduced the capability to create custom reflectance, emissivity, temperature, and radiance maps to add fidelity to EOIR analyses.
This plugin allows uers to automate the creation of custom reflectance maps. It takes a screenshot of the open 2D window, processes the image and re-exports it as a .csv file readable by STK's EOIR module. Users can select whether they want the created map to be added to the scenario's EOIR configuration.

### Inputs
- Image name
- Image path

### Outputs
- Converted reflectance map in .csv format
- Automatically updated EOIR configuration in STK (if requested)

### Installation
1. To use this plugin, please copy the .xml file and the Realease folder to either:
   - STK User Config Plugins directory (usually Documents\STK 12 \Config\Plugins)
   - STK Install folder (usually C:\Program Files\AGI\STK 12\Plugins)
1. Edit the .xml "CodeBase" line to point to the Release folder containing the .dll file and the Resources folder copied in step 1.

Please ensure that the files have not been locked by your machine by right-clicking them, selection properties and ensuring that there is no secrity warning at the bottom of the General tab warning that the file has been blocked. If the files have been blocked, please click the "Unblock" checkbox.
