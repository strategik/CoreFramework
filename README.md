
<p align="right">
  <img src="https://raw.githubusercontent.com/strategik/CoreFramework/dev/Media/Images/strategik64.png" alt="Strategik Logo"/>
</p>
# CoreFramework
Strategik build complex information management solutions for SharePoint 2013 & Office 365. These solutions require definition in code. They require the ability to provision and de-provision Office 365 artefacts in response to business events throughout their lifecycle, the ability to analyze and update customer installation remotely and support for simple, scalable application lifecycle management.

At Strategik, we have taken the view that there is little commercial value in creating and maintaining a close source code based provisioning framework. A number of valuable community efforts already existing in the space, including the PnP repository and SPMeta2. Whilst both these frameworks are useful, neither currently fully satisfies the business requirements and needs of the Strategik solutions practice. As such, our approach with the Core Framework is to create a thin wrapper around those frameworks (where the functionality we require already exists) and to use best practice Client Object Model and remote API calls to create whatever functionality we require where it does not.

Thus we have created the Strategik Core Framework, our open source library for defining our solutions (in code), provisioning and helper code for SharePoint 2013 and Office 365. It is the foundation of our solution development efforts, containing much of the plumbing that we would otherwise need to create for every custom solution we build. It is available for others to use in the MIT License.

 <p align="center">
     <img src="https://raw.githubusercontent.com/strategik/CoreFramework/dev/Media/Images/StrategikStack.png" alt="Strategik Stak" />
     </p>

The Core Framework encapsulates much of the plumbing required to create solutions on Office 365. It provides a set of simple C# classes that can be used to create definitions of the solutions and all their artefacts that need to be deployed, along with extension methods and helper that wrap up client object model and Office 365 calls. 

The core framework wraps around the latest Office Dev PnP code framework, calling that code wherever possible. A PnP extension, designed to allow the Strategik models to be deployed using the PnP provisioning engine is provided.
