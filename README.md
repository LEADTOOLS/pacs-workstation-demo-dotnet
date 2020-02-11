# LEADTOOLS PACS Medical Workstation Demo for .NET Framework

This demo falls under the [license located here.](./LICENSE.md)

Powered by patented artificial intelligence and machine learning algorithms, [LEADTOOLS is a collection of award-winning document, medical, multimedia, and imaging SDKs](https://www.leadtools.com)

This .NET WinForms demo showcases [LEADTOOLS PACS SDK](https://www.leadtools.com/sdk/pacs-imaging) functionality such as:

- DICOM Data Sets and Communication
  - C Cancel Request/Response
  - C Echo Request/Response
  - C Find Request/Response
  - C Get Request/Response
  - C Move Request/Response
  - C Store Request/Response
- Real-world tools used by radiologists and other health care professionals
- Modular design features fully customizable components to build an entire PACS or replace individual pieces of an existing system
- PACS Client and Server
- Database and Data Access Layer
- Media Creation Management
- Works out of the box, but source code is provided for easy customization, localization, and branding

With the PACS Workstation demo, you can test PACS SCP and SCU functions and controls, secure PACS communication, comprehensive DICOM data set support, annotation, extended grayscale image display such as window level and LUT processing, and specialized medical image processing. Other features include lossless JPEG compression, JPIP, MRTI, and signed and unsigned image data processing.

## Set Up

In order to use any LEADTOOLS functionality, you must have a valid license. You can obtain a fully functional 30-day license [from our website](https://www.leadtools.com/downloads).

Locate the `RasterSupport.SetLicense(licenseFilePath, developerKey);` line in the application and modify the code to point to use your new license and key.

Run Visual Studio as Administrator. Open the SLN file. Build the project to restore the [LEADTOOLS NuGet packages](https://www.leadtools.com/downloads/nuget). Run the project. If running for the first time, follow the prompts to create and set up the required databases and services. 

## Use

Once the setups are done, run the Workstation Demo and login with the credentials from the setup step. Search for patients in the PACS Database and select a patient to load it into the Viewer.

## Resources

Website: <https://www.leadtools.com/>

Download Full Evaluation: <https://www.leadtools.com/downloads>

Documentation: <https://www.leadtools.com/help/leadtools/v20/dh/to/introduction.html>

Technical Support: <https://www.leadtools.com/support/chat>

[nuget-profile]: https://www.nuget.org/profiles/LEADTOOLS
