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

#endregion License

using System;

namespace Strategik.Definitions.Fields
{
    public class STKFieldConstants
    {
        #region SharePoint Field Guids (Item)

        public const String ACCOUNT_GUID = "bfc6f32c-668c-43c4-a903-847cca2f9b3c";
        public const String ADDRESS_GUID = "fc2e188e-ba91-48c9-9dd3-16431afddd50";
        public const String APPROVAL_COMMENTS_GUID = "34ad21eb-75bd-4544-8c73-0e08330291fe";
        public const String APPROVAL_STATUS_GUID = "fdc3b2ed-5bf2-4835-a4bc-b885f3396a61";
        public const String ASSIGNEDTO_GUID = "53101f38-dd2e-458c-b245-0c236cc13d1a";
        public const String ATTACHMENT_GUID = "67df98f4-9dec-48ff-a553-29bece9c5bf4";
        public const String AUTHOR_GUID = "1df5e554-ec7e-46a6-901d-d85a3881cb18";
        public const String BASE_NAME_GUID = "7615464b-559e-4302-b8e2-8f440b913101";
        public const String BODY_GUID = "7662cd2c-f069-4dba-9e35-082cf976e170";
        public const String BUSINESS_PHONE_GUID = "fd630629-c165-4513-b43c-fdb16b86a14d";
        public const String CHECK_IN_COMMENT_GUID = "58014f77-5463-437b-ab67-eec79532da67";
        public const String CC_GUID = "ba3c27ee-4791-4867-8821-ff99000bac98";
        public const String CHECKED_OUT_TO_GUID = "3881510a-4e4a-4ee8-b102-8ee8e2d0dd4b";
        public const String CHECKED_OUT_USER_ID_GUID = "a7b731a3-1df1-4d74-a5c6-e2efba617ae2";

        //public const	String	CHECKED_OUT_TO_GUID	=	"3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const	String	CHECKED_OUT_USERID_GUID	=	"3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        public const String CIENT_ID_GUID = "6d2c4fde-3605-428e-a236-ce5f3dc2b4d4";

        public const String CITY_GUID = "6ca7bd7f-b490-402e-af1b-2813cf087b1e";
        public const String COMMENTS_GUID = "9da97a8a-1da5-4a77-98d3-4bc10456e700";
        public const String COMPANY_GUID = "038d1503-4629-40f6-adaf-b47d1ab2d4fe";
        public const String CONTENT_TYPE_GUID = "c042a256-787d-4a6f-8a8a-cf6ab767f12d";
        public const String COPY_SOURCE_GUID = "6b4e226d-3d88-4a36-808d-a129bf52bccf";
        public const String CONTENT_TYPE_ID_GUID = "03e45e84-1992-4d42-9116-26f756012634";
        public const String COUNTRY_REGION_GUID = "3f3a5c85-9d5a-4663-b925-8b68a678ea3a";
        public const String CREATED_BY_GUID = "1df5e554-ec7e-46a6-901d-d85a3881cb18";

        //CREATED ID WRONG? public const String CREATED_GUID = "998b5cff-4a35-47a7-92f3-3914aa6aa4a2";
        public const String CREATED_GUID = "8c06beca-0777-48f7-91c7-6da68bc07b69";

        public const String DATE_PICTURE_TAKEN_GUID = "a5d2f824-bc53-422e-87fd-765939d863a5";
        public const String DOCUMENT_CONCURRENCY_NUMBER_GUID = "8e69e8e8-df8a-45dc-858a-1b806dde24c0";
        public const String DOCUMENT_CREATED_BY_GUID = "4dd7e525-8d6b-4cb4-9d3e-44ee25f973eb";
        public const String DOCUMENT_MODIFIED_BY_GUID = "822c78e3-1ea9-4943-b449-57863ad33ca9";
        public const String DUE_DATE_GUID = "cd21b4c2-6841-4f9e-a23a-738a65f99889";
        public const String DUEDATE_GUID = "cd21b4c2-6841-4f9e-a23a-738a65f99889";
        public const String EDIT_GUID = "503f1caa-358e-4918-9094-4a2cdc4bc034";
        public const String EDIT_MENU_TABLE_END_GUID = "2ea78cef-1bf9-4019-960a-02c41636cb47";
        public const String EDIT_MENU_TABLE_START_GUID = "3c6303be-e21f-4366-80d7-d6d0a3b22c7a";
        public const String EDIT_MENU_TABLE_START2_GUID = "1344423c-c7f9-4134-88e4-ad842e2d723c";
        public const String ENCODED_ABSOLUTE_URL_GUID = "7177cfc7-f399-4d4d-905d-37dd51bc90bf";
        public const String EMAIL_ADDRESS_GUID = "fce16b4c-fe53-4793-aaab-b4892e736d15";

        //public const	String	ENCODED_ABSOLUTE_URL_GUID	=	"3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        public const String EXPIRES_GUID = "6a09e75b-8d17-4698-94a8-371eda1af1ac";

        public const String FAX_NUMBER_GUID = "9d1cacc8-f452-4bc1-a751-050595ad96e1";
        public const String FILE_NAME_GUID = "7615464b-559e-4302-b8e2-8f440b913101";
        public const String FILE_TYPE_GUID = "39360f11-34cf-4356-9945-25c44e68dade";
        public const String FILE_SIZE_DISPLAY_GUID = "78a07ba4-bda8-4357-9e0f-580d64487583";

        //Files SIZE ID WRONG?  8fca95c0-9b7d-456f-8dae-b41ee2728b85
        public const String FILE_SIZE_GUID = "78a07ba4-bda8-4357-9e0f-580d64487583";

        //public const	String	FILE_TYPE_GUID	= "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        public const String FIRST_NAME_GUID = "4a722dd4-d406-4356-93f9-2550b8f50dd0";

        public const String FOLDER_CHILD_COUNT_GUID = "960ff01f-2b6d-4f1b-9c3f-e19ad8927341";
        public const String FORM_RELATIVE_URL_GUID = "467e811f-0c12-4a93-bb04-42ff0c1c597c";
        public const String FORM_VERSION_GUID = "94ad6f7c-09a1-42ca-974f-d24e080160c2";
        public const String FULL_NAME_GUID = "475c2610-c157-4b91-9e2d-6855031b3538";

        //public const String GUID_GUID = "ae069f25-3ac2-4256-b9c3-15dbc15da0e0";
        public const String HAS_COPY_DESTINATIONS_GUID = "26d0756c-986a-48a7-af35-bf18ab85ff4a";

        public const String HTML_FILE_TYPE_GUID = "0c5e0085-eb30-494b-9cdd-ece1d3c649a2";
        public const String HOME_PHONE_GUID = "2ab923eb-9880-4b47-9965-ebf93ae15487";
        public const String ID_GUID = "1d22ea11-1e32-424e-89ab-9fedbadb6ce1";
        public const String ID_UNIQUE_GUID = "4b7403de-8d94-43e8-9f0f-137a3e298126";
        public const String INSTANCE_ID_GUID = "50a54da4-1528-4e67-954a-e2d24f1e9efb";
        public const String IS_CHECKED_OUT_TO_LOCAL_GUID = "cfaabd0f-bdbd-4bc2-b375-1e779e2cad08";
        public const String IS_CURRENT_VERSION_GUID = "c101c3e7-122d-4d4d-bc34-58e94a38c816";
        public const String ISSUE_STATUS_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";

        //public const String Item_Child_Count_GUID = "b824e17e-a1b3-426e-aecf-f0184d900485";
        public const String ITEM_CHILD_COUNT_GUID = "1d22ea11-1e32-424e-89ab-9fedbadb6ce1";

        public const String ITEM_TYPE_GUID = "30bb605f-5bae-48fe-b4e3-1f81d9772af9";
        public const String JOB_TITLE_GUID = "c4e0f350-52cc-4ede-904c-dd71a3d11f7d";
        public const String KEYWORDS_GUID = "b66e9b50-a28e-469b-b1a0-af0e45486874";
        public const String LEVEL_GUID = "43bdd51b-3c5b-4e78-90a8-fb2087f71e70";
        public const String MOBILE_PHONE_GUID = "2a464df1-44c1-4851-949d-fcd270f0ccf2";

        //MOFIDIED ID WRONG? 173f76c8-aebd-446a-9bc9-769a2bd2c18f INTERNAL_NAME: Last_x0020_Modified
        public const String MODIFIED_GUID = "28cf69c5-fa48-462a-b5cd-27b6f9d2bd5f";

        public const String MODIFIED_BY_GUID = "d31655d1-1d5b-4511-95a1-7a09e9b75bf2";
        public const String NAME_GUID = "8553196d-ec8d-4564-9861-3dbe931050c8";
        public const String NAME_LINKED_TO_FILENAME_GUID = "5cc6dc79-3710-4374-b433-61cb4a686c12";
        public const String NAME_LINKED_TO_FILENAME_NO_MENU_GUID = "9d30f126-ba48-446b-b8f9-83745f322ebe";
        public const String NAME_LINKED_TO_FILENAME2_GUID = "224ba411-da77-4050-b0eb-62d422f13d3e";
        public const String NAME_LINKED_TO_ITEM_WITH_EDIT_MENU_GUID = "5cc6dc79-3710-4374-b433-61cb4a686c12";
        public const String NAME_FILE_LEAF_REF_GUID = "8553196d-ec8d-4564-9861-3dbe931050c8";
        public const String NOTES_GUID = "9da97a8a-1da5-4a77-98d3-4bc10456e700";
        public const String ORDER_GUID = "ca4addac-796f-4b23-b093-d2a3f65c0774";
        public const String OWSHIDDENVERSION_GUID = "d4e44a66-ee3a-4d02-88c9-4ec5ff3f4cd5";
        public const String PATH_GUID = "56605df6-8fa1-47e4-a04c-5b384d59609f";
        public const String PARENT_LEAF_NAME_GUID = "774eab3a-855f-4a34-99da-69dc21043bec";
        public const String PARENT_VERSION_STRING_GUID = "bc1a8efb-0f4c-49f8-a38f-7fe22af3d3e0";
        public const String PICTURE_HEIGHT_GUID = "1944c034-d61b-42af-aa84-647f2e74ca70";
        public const String PICTURE_SIZE_GUID = "922551b8-c7e0-46a6-b7e3-3cf02917f68a";
        public const String PICTURE_WIDTH_GUID = "7e68a0f9-af76-404c-9613-6f82bc6dc28c";
        public const String PERCENT_COMPLETE_GUID = "d2311440-1ed6-46ea-b46d-daa643dc3886";
        public const String PERCENTCOMPLETE_GUID = "d2311440-1ed6-46ea-b46d-daa643dc3886";
        public const String PICTURE_PREVIEW_GUID = "8c0d0aac-9b76-4951-927a-2490abe13c0b";
        public const String PRIORITY_GUID = "a8eb573e-9e11-481a-a8c9-1104a54b2fbd";
        public const String PROG_ID_GUID = "c5c4b81c-f1d9-4b43-a6a2-090df32ebb68";
        public const String PROPERTY_BAG_GUID = "687c7f94-686a-42d3-9b67-2782eac4b4f8";
        public const String REQUIRED_FIELD_GUID = "de1baa4b-2117-473b-aa0c-4d824034142d";

        //public const	String	SERVER_RELATIVE_URL_GUID	=	"3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        public const String SCOPE_ID_GUID = "dddd2420-b270-4735-93b5-92b713d0944d";

        public const String SELECTION_CHECKBOX_GUID = "7ebf72ca-a307-4c18-9e5b-9d89e1dae74f";
        public const String SELECT_FILE_NAME_GUID = "5f47e085-2150-41dc-b661-442f3027f552";
        public const String SELECT_TITLE_GUID = "5f47e085-2150-41dc-b661-442f3027f552";
        public const String SERVER_RELATIVE_URL_GUID = "105f76ce-724a-4bba-aece-f81f2fce58f5";
        public const String STARTDATE_GUID = "64cd368d-2f95-4bfc-a1f9-8d4324ecb007";
        public const String SHARED_FILE_INDEX_GUID = "034998e9-bf1c-4288-bbbd-00eacfc64410";
        public const String SORT_TYPE_GUID = "423874f8-c300-4bfb-b7a1-42e2159e3b19";

        //Same as PARENT_LEAF_NAME_GUID
        //public const String SOURCE_NAME_GUID = "774eab3a-855f-4a34-99da-69dc21043bec";
        //Same as PARENT_VERSION_STRING_GUID
        //public const String SOURCE_VERSION_GUID = "bc1a8efb-0f4c-49f8-a38f-7fe22af3d3e0";
        public const String SOURCE_URL_GUID = "c63a459d-54ba-4ab7-933a-dcf1c6fadec2";

        public const String STATE_PROVINCE_GUID = "ceac61d3-dda9-468b-b276-f4a6bb93f14f";
        public const String STATUS_GUID = "c15b34c3-ce7d-490a-b133-3f4de8801b76";
        public const String TASKGROUP_GUID = "50d8f08c-8e99-4948-97bf-2be41fa34a0d";
        public const String TEMPLATE_ID_GUID = "467e811f-0c12-4a93-bb04-42ff0c1c597b";

        //public const	String	TITLE_GUCK	=	"09A6EA55-78DC-4D17-B5F3-AB7AF689D7C9";
        public const String TYPE_GUID = "081c6e4c-5c14-4f20-b23e-1a71ceb6a67c";

        public const String TITLE_GUID = "fa564e0f-0c70-4ab9-b863-0177e6ddd247";
        public const String TITLE_LINKED_TO_ITEM_GUID = "bc91a437-52e7-49e1-8c4e-4698904b2b6d";
        public const String TITLE_LINKED_TO_ITEM_WITH_EDIT_MENU_GUID = "82642ec8-ef9b-478f-acf9-31f7d45fbc31";
        public const String THUMBNAIL_GUID = "ac7bb138-02dc-40eb-b07a-84c15575b6e9";
        public const String URL_GUID = "c29e077d-f466-4d8e-8bbe-72b66c5f205c";
        public const String URL_PATH_GUID = "94f89715-e097-4e8b-ba79-ea02aa8b7adb";
        public const String UI_VERSION_GUID = "7841bf41-43d0-4434-9f50-a673baef7631";

        //VERSION  ALSO NAMED UIVersionString
        public const String VERSION_GUID = "dce8262a-3ae9-45aa-aab4-83bd75fb738a";

        public const String VIRUS_STATUS_GUID = "4a389cb9-54dd-4287-a71a-90ff362028bc";
        public const String WEB_PAGE_GUID = "a71affd2-dcc7-4529-81bc-2fe593154a5f";
        public const String WORKFLOW_INSTANCE_ID_GUID = "de8beacf-5505-47cd-80a6-aa44e7ffe2f4";
        public const String WORKFLOW_VERSION_GUID = "f1e020bc-ba26-443f-bf2f-b68715017bbc";
        public const String ZIP_POSTAL_CODE_GUID = "9a631556-3dac-49db-8d2f-fb033b0fdc24";

        // continue form Customized reports

        #endregion SharePoint Field Guids (Item)

        #region SharePoint Fields Internal Names

        public const String THUMBANIL_INTERNAL_NAME = "Thumbnail";
        public const String COPY_SOURCE_INTERNAL_NAME = "_CopySource";
        public const String DATE_PICTURE_TAKEN_INTERNAL_NAME = "ImageCreateDate";
        public const String FORM_RELAVTIVE_URL_INTERNAL_NAME = "FormRelativeUrl";
        public const String PARENT_LEAF_NAME_INTERNAL_NAME = "ParentLeafName";
        public const String PARENT_VERSION_STRING_INTERNAL_NAME = "ParentVersionString";
        public const String TEMPLATE_ID_INTERNAL_NAME = "TemplateId";
        public const String TYPE_INTERNAL_NAME = "DocIcon";

        //Created under Customised Reports - more fields
        //public const String CREATED_INTERNAL_NAME = "Created_x0020_Date";
        public const String UI_VERSION_INTERNAL_NAME = "_UIVersionString";

        public const String FORM_VERSION_INTERNAL_NAME = "FormVersion";
        public const String DOCUMENT_CONCURRENCY_INTERNAL_NAME = "DocConcurrencyNumber";
        public const String DOCUMENT_CREATED_BY_INTERNAL_NAME = "Created_x0020_By";
        public const String DOCUMENT_MODIFIED_BY_INTERNAL_NAME = "Modified_x0020_By";
        public const String EDIT_MENU_TABLE_END_INTERNAL_NAME = "_EditMenuTableEnd";
        public const String EDIT_MENU_TABLE_START2_INTERNAL_NAME = "_EditMenuTableStart2";
        public const String EDIT_MENU_TABLE_START_INTERNAL_NAME = "_EditMenuTableStart";
        public const String EFFECTIVE_PREMISSIONS_MASK_INTERNAL_NAME = "PermMask";
        public const String ENCODED_ABSOLUTE_URL_INTERNAL_NAME = "EncodedAbsUrl";
        public const String FILE_NAME_INTERNAL_NAME = "BaseName";
        public const String FILE_TYPE_INTERNAL_NAME = "File_x0020_Type";
        public const String HAS_COPY_DESTINATIONS_INTERNAL_NAME = "_HasCopyDestinations";
        public const String HTML_FILE_TYPE_INTERNAL_NAME = "HTML_x0020_File_x0020_Type";
        public const String CHECKED_OUT_USER_ID_INTERNAL_NAME = "CHECKED_OUT_USER_ID";
        public const String INSTANCE_ID_INTERNAL_NAME = "InstanceID";
        public const String IS_CHECKED_OUT_TO_LOCAL_INTERNAL_NAME = "IsCheckedoutToLocal";
        public const String IS_CURRENT_VERSION_INTERNAL_NAME = "_IsCurrentVersion";
        public const String ITEM_TYPE_INTERNAL_NAME = "FSObjType";
        public const String KEYWORDS_INTERNAL_NAME = "Keywords";
        public const String LEVEL_INTERNAL_NAME = "_Level";
        public const String NAME_LINKED_TO_ITEM_WITH_EDIT_MENU_INTERNAL_NAME = "LinkFilenameNoMenu";
        public const String NAME_FILE_LEAF_INTERNAL_NAME = "NAME_FILE_LEAF";
        public const String NAME_LINKED_TO_FILENAME2_INTERNAL_NAME = "LinkFilename2";
        public const String NAME_LINKED_TO_FILENAM_INTERNAL_NAME = "LinkFilename";
        public const String ORDER_INTERNAL_NAME = "Order";
        public const String OWSHIDDENVERSION_INTERNAL_NAME = "owshiddenversion";
        public const String PROG_ID_INTERNAL_NAME = "ProgId";
        public const String PROPERTY_BAG_INTERNAL_NAME = "MetaInfo";
        public const String PICTURE_HEIGHT_INTERNAL_NAME = "ImageHeight";
        public const String PICTURE_SIZE_INTERNAL_NAME = "ImageSize";
        public const String PICTURE_WIDTH_INTERNAL_NAME = "ImageWidth";
        public const String PICTURE_PREVIEW_INTERNAL_NAME = "PreviewOnForm";
        public const String REQUIRED_FIELD_INTERNAL_NAME = "RequiredField";
        public const String SELECTION_CHECKBOX_INTERNAL_NAME = "SelectedFlag";
        public const String SCOPE_ID_INTERNAL_NAME = "ScopeId";
        public const String SELECT_FILE_NAME_INTERNAL_NAME = "SelectFilename";
        public const String SELECT_TITLE_INTERNAL_NAME = "SelectTitle";
        public const String SERVER_RELATIVE_URL_INTERNAL_NAME = "ServerUrl";
        public const String SHARED_FILE_INDEX_INTERNAL_NAME = "_SharedFileIndex";
        public const String SORT_TYPE_INTERNAL_NAME = "SortBehavior";
        public const String SOURCE_URL_INTERNAL_NAME = "_SourceUrl";

        //public const String UI_VERSION_INTERNAL_NAME = "_UIVersion";
        public const String VIRUS_STATUS_INTERNAL_NAME = "VirusStatus";

        public const String WORKFLOW_INSTANCE_ID_INTERNAL_NAME = "WorkflowInstanceID";
        public const String WORKFLOW_VERSION_INTERNAL_NAME = "WorkflowVersion";

        #endregion SharePoint Fields Internal Names

        //public const String NAME_GUID = "8553196d-ec8d-4564-9861-3dbe931050c8";
        //public const String BASE_NAME_GUID = "7615464b-559e-4302-b8e2-8f440b913101";
        //public const String CHECK_IN_COMMENT_GUID = "58014f77-5463-437b-ab67-eec79532da67";
        //public const String CHECKED_OUT_TO_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String ENCODED_ABSOLUTE_URL_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String FILE_SIZE_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String FILE_TYPE_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String CHECKED_OUT_USERID_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String SERVER_RELATIVE_URL_GUID = "3f277a5c-c7ae-4bbe-9d44-0456fb548f94";
        //public const String URL_PATH_GUID = "94f89715-e097-4e8b-ba79-ea02aa8b7adb";
        //public const String Content_Type_GUID = "c042a256-787d-4a6f-8a8a-cf6ab767f12d";
        //public const String Folder_Child_Count = "960ff01f-2b6d-4f1b-9c3f-e19ad8927341";
        //public const String Item_Child_Count ="b824e17e-a1b3-426e-aecf-f0184d900485";

        #region Document Library Field Internal Names

        public const String NAME_INTERNAL_NAME = "FileLeafRef";
        public const String URL_PATH_INTERNAL_NAME = "FileRef";
        public const String BASE_NAME_INTERNAL_NAME = "BaseName";

        public const String Content_Type_INTERNAL_NAME = "ContentType";
        public const String Folder_Child_Count_INTERNAL_NAME = "FolderChildCount";
        public const String Item_Child_Count_INTERNAL_NAME = "ItemChildCount";
        public const String DOCUMENT_LIBRARY_FILE_SIZE_INTERNAL_NAME = "FileSizeDisplay";

        public const String DOCUMENT_FILE_DIRECTORY_INTERNAL_NAME = "FileDirRef";
        public const String DOCUMENT_FILE_RELATIVE_URL_INTERNAL_NAME = "ServerUrl";
        public const String DOCUMENT_FILE_ABSOLUTE_URL_INTERNAL_NAME = "EncodedAbsUrl";
        public const String DOCUMENT_FILE_TYPE_INTERNAL_NAME = "File_x0020_Type";

        #endregion Document Library Field Internal Names

        #region Document Library Field Guids

        public const String DOCUMENT_LIBRARY_FILE_SIZE_GUID = "78a07ba4-bda8-4357-9e0f-580d64487583";
        public const String DOCUMENT_FILE_DIRECTORY_GUID = "56605df6-8fa1-47e4-a04c-5b384d59609f";
        public const String DOCUMENT_FILE_RELATIVE_URL_GUID = "105f76ce-724a-4bba-aece-f81f2fce58f5";
        public const String DOCUMENT_FILE_ABSOLUTE_URL_GUID = "7177cfc7-f399-4d4d-905d-37dd51bc90bf";
        public const String DOCUMENT_FILE_TYPE_GUID = "39360f11-34cf-4356-9945-25c44e68dade";

        #endregion Document Library Field Guids

        #region User Information List Field Guids

        //public const String ACCOUNT_GUID = "bfc6f32c-668c-43c4-a903-847cca2f9b3c";

        #endregion User Information List Field Guids

        #region User information List Fields Internal Names

        public const String ACCOUNT_INTERNAL_NAME = "Name";

        #endregion User information List Fields Internal Names

        //ADD MORE FROM "MORE FIELDS - COPY SOURCE"

        #region Announcements Fields guids

        //public const String ATTACHMENT_GUID = "67df98f4-9dec-48ff-a553-29bece9c5bf4";
        //public const String CREATED_BY_GUID = "1df5e554-ec7e-46a6-901d-d85a3881cb18";
        //public const String EDIT_GUID = "503f1caa-358e-4918-9094-4a2cdc4bc034";
        //public const String FOLDER_CHILD_COUNT_GUID = "960ff01f-2b6d-4f1b-9c3f-e19ad8927341";
        //public const String ITEM_CHILD_COUNT_GUID = "1d22ea11-1e32-424e-89ab-9fedbadb6ce1";
        //public const String VERISON_GUID = "dce8262a-3ae9-45aa-aab4-83bd75fb738a";
        //public const String APPROVAL_COMMENTS_GUID = "34ad21eb-75bd-4544-8c73-0e08330291fe";
        //public const String CIENT_ID_GUID = "6d2c4fde-3605-428e-a236-ce5f3dc2b4d4";

        #endregion Announcements Fields guids

        #region Annoucements Fields Internal Name

        public const String ATTACHMENT_INTERNAL_NAME = "Attachments";
        public const String BODY_INTERNAL_NAME = "Body";
        public const String CONTENT_TYPE_INTERNAL_NAME = "ContentType";
        public const String CREATED_INTERNAL_NAME = "Created";
        public const String CREATED_BY_INTERNAL_NAME = "Author";
        public const String EDIT_INTERNAL_NAME = "Edit";
        public const String EXPIRES_INTERNAL_NAME = "Expires";
        public const String FOLDER_CHILD_COUNT_INTERNAL_NAME = "FolderChildCount";
        public const String ITEM_CHILD_COUNT_INTERNAL_NAME = "ItemChildCount";
        public const String MODIFIED_INTERNAL_NAME = "Modified";
        public const String MODIFIED_BY_INTERNAL_NAME = "Editor";
        public const String TITLE_INTERNAL_NAME = "Title";
        public const String VERISON_INTERNAL_NAME = "_UIVersionString";
        public const String APPROVAL_STATUS_INTERNAL_NAME = "_ModerationStatus";
        public const String APPROVAL_COMMENTS_INTERNAL_NAME = "_ModerationComments";
        public const String CLIENT_ID_INTERNAL_NAME = "SyncClientId";
        // public const String CONTENT_TYPE_INTERNAL_NAME="ContentTypeId";

        #endregion Annoucements Fields Internal Name

        #region Calendar Fields guids

        public const String LOCATION_GUID = "288f5f32-8462-4175-8f09-dd7ba29359a9";
        public const String ATTENDEES_GUID = "8137f7ad-9170-4c1d-a17b-4ca7f557bc88";
        public const String CATEGORY_GUID = "6df9bd52-550e-4a30-bc31-a4366832a87d";
        public const String START_TIME_GUID = "64cd368d-2f95-4bfc-a1f9-8d4324ecb007";
        public const String END_TIME_GUID = "2684f9f2-54be-429f-ba06-76754fc056bf";
        public const String DESCRIPTION_GUID = "9da97a8a-1da5-4a77-98d3-4bc10456e700";
        public const String ALL_DAY_EVENT_GUID = "7d95d1f4-f5fd-4a70-90cd-b35abc9b5bc8";
        public const String RECURRENCE_GUID = "f2e63656-135e-4f1c-8fc2-ccbe74071901";
        public const String RECURRENCEDATA_GUID = "d12572d0-0a1e-4438-89b5-4d0430be7603";
        public const String WORKSPACE_GUID = "08fc65f9-48eb-4e99-bd61-5946c439e691";
        public const String EVENT_TYPE_GUID = "5d1d4e76-091a-4e03-ae83-6a59847731c0";
        public const String RECURRENCE_CK_GUID = "dfcc8fff-7c4c-45d6-94ed-14ce0719efef";
        public const String UCK_GUID = "63055d04-01b5-48f3-9e1e-e564e7c6b23b";
        public const String TIME_ZONE_GUID = "6cc1c612-748a-48d8-88f2-944f477f301b";
        public const String XML_T_ZONE_GUID = "c4b72ed6-45aa-4422-bff1-2b6750d30819";

        #endregion Calendar Fields guids

        #region Calendar Fields internal names

        public const String LOCATION_INTERNAL_NAME = "Location";
        public const String ATTENDEES_INTERNAL_NAME = "Attendees";
        public const String CATEGORY_INTERNAL_NAME = "Category";
        public const String START_TIME_INTERNAL_NAME = "EventDate";
        public const String END_TIME_INTERNAL_NAME = "EndDate";
        public const String DESCRIPTION_INTERNAL_NAME = "Description";
        public const String ALL_DAY_EVENT_INTERNAL_NAME = "fAllDayEvent";
        public const String RECURRENCE_INTERNAL_NAME = "fRecurrence";
        public const String RECURRENCEDATA_INTERNAL_NAME = "RecurrenceData";
        public const String WORKSPACE_INTERNAL_NAME = "WorkspaceLink";
        public const String EVENT_TYPE_INTERNAL_NAME = "EventType";
        public const String RECURRENCE_CK_INTERNAL_NAME = "RecurrenceCK";
        public const String UCK_INTERNAL_NAME = "UCK";
        public const String TIME_ZONE_INTERNAL_NAME = "TimeZone";
        public const String XML_T_ZONE_INTERNAL_NAME = "XMLTZone";

        #endregion Calendar Fields internal names

        #region Issue Tracking Internal Names

        public const String ISSUE_STATUS_INTERNAL_NAME = "Status";

        #endregion Issue Tracking Internal Names

        #region Task Fields Fields GUIDs

        //public const String PERCENT_COMPLETE_GUID = "d2311440-1ed6-46ea-b46d-daa643dc3886";
        //public const String ASSIGNED_TO_GUID = "53101f38-dd2e-458c-b245-0c236cc13d1a";
        public const String ATTACHMENTS_GUID = "67df98f4-9dec-48ff-a553-29bece9c5bf4";

        //       public const String CONTENT_TYPE_GUID = "c042a256-787d-4a6f-8a8a-cf6ab767f12d";
        //       public const String CREATED_GUID = "8c06beca-0777-48f7-91c7-6da68bc07b69";
        //       public const String CREATED_BY_GUID = "1df5e554-ec7e-46a6-901d-d85a3881cb18";
        //        public const String DESCRIPTION_GUID = "7662cd2c-f069-4dba-9e35-082cf976e170";
        //public const String DUE_DATE_GUID = "cd21b4c2-6841-4f9e-a23a-738a65f99889";
        //       public const String EDIT_GUID = "503f1caa-358e-4918-9094-4a2cdc4bc034";
        //public const String _GUID = "";
        //public const String _GUID = "";
        //public const String _GUID = "";
        //public const String _GUID = "";
        //public const String _GUID = "";

        #endregion Task Fields Fields GUIDs

        #region Task Fields Internal Name

        public const String PERCENT_COMPLETE_INTERNAL_NAME = "PercentComplete";
        public const String ASSIGNEDTO_INTERNAL_NAME = "AssignedTo";
        public const String ATTACHMENTS_INTERNAL_NAME = "Attachments";

        //       public const String CONTENT_TYPE_INTERNAL_NAME = "ContentType";
        //       public const String CREATED_INTERNAL_NAME = "Created";
        //       public const String CREATED_BY_INTERNAL_NAME = "Author";
        //       public const String DESCRIPTION_INTERNAL_NAME = "Body";
        public const String DUEDATE_INTERNAL_NAME = "DueDate";

        //       public const String EDIT_INTERNAL_NAME = "Edit";
        //public const String _INTERNAL_NAME = "";
        //public const String _INTERNAL_NAME = "";

        #endregion Task Fields Internal Name

        #region SharePoint Field Internal Names

        //TODO:

        public const String TITLE_LINKED_TO_ITEM_WITH_EDIT_MENU_INTERNAL_NAME = "";
        public const String TITLE_LINKED_TO_ITEM_INTERNAL_NAME = "";
        public const String UNIQUE_ID_INTERNAL_NAME = "UniqueId";
        public const String ID_INTERNAL_NAME = "ID";
        public const String CONTENT_TYPE_ID_INTERNAL_NAME = "ContentTypeId";

        //       public const String CONTENT_TYPE_INTERNAL_NAME = "ContentType";
        //       public const String TITLE_INTERNAL_NAME = "Title";
        //       public const String MODIFIED_INTERNAL_NAME = "Modified";
        //       public const String CREATED_INTERNAL_NAME = "Created";
        public const String AUTHOR_INTERNAL_NAME = "Author";

        public const String EDITOR_INTERNAL_NAME = "Editor";
        public const String VERSION_INTERNAL_NAME = "_UIVersionString";
        public const String MOBILE_PHONE_INTERNAL_NAME = "";
        public const String CITY_INTERNAL_NAME = "";
        public const String STATE_PROVINCE_INTERNAL_NAME = "";
        public const String ZIP_POSTAL_CODE_INTERNAL_NAME = "";
        public const String COUNTRY_REGION_INTERNAL_NAME = "";
        public const String WEB_PAGE_INTERNAL_NAME = "";
        public const String NOTES_INTERNAL_NAME = "";
        public const String CHECKED_OUT_TO_INTERNAL_NAME = "";
        public const String CHECK_IN_COMMENT_INTERNAL_NAME = "";
        public const String FILE_SIZE_DISPLAY_INTERNAL_NAME = "";

        //public const String NAME_LINKED_TO_ITEM_WITH_EDIT_MENU_INTERNAL_NAME = "";
        public const String PRIORITY_INTERNAL_NAME = "";

        public const String STATUS_INTERNAL_NAME = "";
        public const String PERCENTCOMPLETE_INTERNAL_NAME = "";

        //    public const String BODY_INTERNAL_NAME = "";
        public const String STARTDATE_INTERNAL_NAME = "";

        //public const String DUEDATE_INTERNAL_NAME = "";
        //public const String ASSIGNEDTO_INTERNAL_NAME = "";
        public const String TASKGROUP_INTERNAL_NAME = "";

        public const String FIRST_NAME_INTERNAL_NAME = "";
        public const String FULL_NAME_INTERNAL_NAME = "";
        public const String EMAIL_ADDRESS_INTERNAL_NAME = "";
        public const String COMPANY_INTERNAL_NAME = "";
        public const String JOB_TITLE_INTERNAL_NAME = "";
        public const String BUSINESS_PHONE_INTERNAL_NAME = "";
        public const String HOME_PHONE_INTERNAL_NAME = "";
        public const String FAX_NUMBER_INTERNAL_NAME = "";
        public const String ADDRESS_INTERNAL_NAME = "";

        //     public const String EXPIRES_INTERNAL_NAME = "";
        public const String URL_INTERNAL_NAME = "";

        public const String COMMENTS_INTERNAL_NAME = "";

        #endregion SharePoint Field Internal Names
    }
}