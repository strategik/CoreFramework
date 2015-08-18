#region License

//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// Author:  Dr Adrian Colquhoun
//

#endregion License

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace Strategik.Definitions.UserInterface
{
    public static partial class STKCustomActionExtensions
    {
        public static CustomActions GeneratePnPTemplate(this STKCustomActions customActions)
        {
            CustomActions customActionsTemplate = new CustomActions();

            foreach (STKCustomAction siteCustomAction in customActions.SiteCustomActions)
            {
                customActionsTemplate.SiteCustomActions.Add(siteCustomAction.GeneratePnPTemplate());
            }

            foreach (STKCustomAction webCustomAction in customActions.WebCustomActions)
            {
                customActionsTemplate.WebCustomActions.Add(webCustomAction.GeneratePnPTemplate());
            }

            return customActionsTemplate;
        }

        public static CustomAction GeneratePnPTemplate(this STKCustomAction customAction)
        {
            CustomAction customActionTemplate = new CustomAction()
            {
                //  CommandUIExtension = customAction.CommandUIExtension,
                Description = customAction.Description,
                Enabled = customAction.Enabled,
                Group = customAction.Group,
                ImageUrl = customAction.ImageUrl,
                Location = customAction.Location,
                Name = customAction.Name,
                RegistrationId = customAction.RegistrationId,
                RegistrationType = (UserCustomActionRegistrationType)customAction.RegistrationType,
                Remove = customAction.DeleteIfPresent,
                //TODO
                //Rights
                //RightsValue
                ScriptBlock = customAction.ScriptBlock,
                ScriptSrc = customAction.ScriptSource,
                Sequence = customAction.Sequence,
                Title = customAction.Title,
                Url = customAction.Url
            };

            return customActionTemplate;
        }

        public static ComposedLook GeneratePnPTemplate(this STKComposedLook composedLook)
        {
            ComposedLook composedLookTemplate = new ComposedLook()
            {
                AlternateCSS = composedLook.AlternateCSS,
                BackgroundFile = composedLook.BackgroundFile,
                ColorFile = composedLook.ColorFile,
                FontFile = composedLook.FontFile,
                MasterPage = composedLook.MasterPage,
                Name = composedLook.Name,
                SiteLogo = composedLook.SiteLogo,
                Version = composedLook.Version
            };

            return composedLookTemplate;
        }
    }
}