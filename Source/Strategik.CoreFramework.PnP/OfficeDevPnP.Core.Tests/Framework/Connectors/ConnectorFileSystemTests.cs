﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using System.IO;

namespace OfficeDevPnP.Core.Tests.Framework.Connectors
{
    [TestClass]
    public class ConnectorFileSystemTests
    {
        #region Test variables

        private static string testContainer = "pnptest";
        private static string testContainerSecure = "pnptestsecure";

        #endregion Test variables

        #region Test initialize and cleanup

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            // File system setup
            if (File.Exists(@".\resources\blabla.png"))
            {
                File.Delete(@".\resources\blabla.png");
            }

            if (File.Exists(@".\Resources\Templates\blabla.png"))
            {
                File.Delete(@".\Resources\Templates\blabla.png");
            }

            if (File.Exists(@".\Resources\Templates\newfolder\blabla.png"))
            {
                File.Delete(@".\Resources\Templates\newfolder\blabla.png");
            }

            if (Directory.Exists(@".\Resources\Templates\newfolder"))
            {
                Directory.Delete(@".\Resources\Templates\newfolder");
            }
        }

        #endregion Test initialize and cleanup

        #region File connector tests

        /// <summary>
        /// Get file as string from provided directory and folder. Specify both directory and container
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFile1Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "Templates");

            string file = fileSystemConnector.GetFile("ProvisioningTemplate-2015-03-Sample-01.xml");
            Assert.IsNotNull(file);

            string file2 = fileSystemConnector.GetFile("Idonotexist.xml");
            Assert.IsNull(file2);
        }

        /// <summary>
        /// Get file as string from provided directory and folder. Specify both directory and container, but container contains multiple elements
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFile2Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".", @"Resources\Templates");

            string file = fileSystemConnector.GetFile("ProvisioningTemplate-2015-03-Sample-01.xml");
            Assert.IsNotNull(file);

            string file2 = fileSystemConnector.GetFile("Idonotexist.xml");
            Assert.IsNull(file2);
        }

        /// <summary>
        /// Get file as string from provided directory and folder. Specify only directory and container, but override the container in the GetFile method
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFile3Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".", @"wrong");

            string file = fileSystemConnector.GetFile("ProvisioningTemplate-2015-03-Sample-01.xml", @"Resources\Templates");
            Assert.IsNotNull(file);

            string file2 = fileSystemConnector.GetFile("Idonotexist.xml", "Templates");
            Assert.IsNull(file2);
        }

        /// <summary>
        /// Get files in the specified directory
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFiles1Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "Templates");
            var files = fileSystemConnector.GetFiles();
            Assert.IsTrue(files.Count > 0);
        }

        /// <summary>
        /// Get files in the specified directory, override the set container in the GetFiles method
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFiles2Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "");
            var files = fileSystemConnector.GetFiles("Templates");
            Assert.IsTrue(files.Count > 0);

            var files2 = fileSystemConnector.GetFiles("");
            Assert.IsTrue(files2.Count > 0);
        }

        /// <summary>
        /// Get file as stream.
        /// </summary>
        [TestMethod]
        public void FileConnectorGetFileBytes1Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "");

            using (var bytes = fileSystemConnector.GetFileStream("office365.png"))
            {
                Assert.IsTrue(bytes.Length > 0);
            }

            using (var bytes2 = fileSystemConnector.GetFileStream("Idonotexist.xml"))
            {
                Assert.IsNull(bytes2);
            }
        }

        /// <summary>
        /// Save file to default container
        /// </summary>
        [TestMethod]
        public void FileConnectorSaveStream1Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "");
            long byteCount = 0;
            using (var fileStream = File.OpenRead(@".\resources\office365.png"))
            {
                byteCount = fileStream.Length;
                fileSystemConnector.SaveFileStream("blabla.png", fileStream);
            }

            //read the file
            using (var bytes = fileSystemConnector.GetFileStream("blabla.png"))
            {
                Assert.IsTrue(byteCount == bytes.Length);
            }

            // file will be deleted at end of test
        }

        /// <summary>
        /// Save file to specified container using a non existing folder...folder will be created on the fly
        /// </summary>
        [TestMethod]
        public void FileConnectorSaveStream2Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".", "wrong");
            long byteCount = 0;
            using (var fileStream = File.OpenRead(@".\resources\office365.png"))
            {
                byteCount = fileStream.Length;
                fileSystemConnector.SaveFileStream("blabla.png", @"Resources\Templates\newfolder", fileStream);
            }

            //read the file
            using (var bytes = fileSystemConnector.GetFileStream("blabla.png", @"Resources\Templates\newfolder"))
            {
                Assert.IsTrue(byteCount == bytes.Length);
            }

            // file will be deleted at end of test
        }

        /// <summary>
        /// Save file to specified container, check if overwrite works
        /// </summary>
        [TestMethod]
        public void FileConnectorSaveStream3Test()
        {
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".", "wrong");
            using (var fileStream = File.OpenRead(@".\resources\custombg.jpg"))
            {
                fileSystemConnector.SaveFileStream("blabla.png", @"Resources\Templates", fileStream);
            }

            long byteCount = 0;
            using (var fileStream = File.OpenRead(@".\resources\office365.png"))
            {
                byteCount = fileStream.Length;
                fileSystemConnector.SaveFileStream("blabla.png", @"Resources\Templates", fileStream);
            }

            //read the file
            using (var bytes = fileSystemConnector.GetFileStream("blabla.png", @"Resources\Templates"))
            {
                Assert.IsTrue(byteCount == bytes.Length);
            }

            // file will be deleted at end of test
        }

        /// <summary>
        /// Save file to default container
        /// </summary>
        [TestMethod]
        public void FileConnectorDelete1Test()
        {
            // upload the file
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".\Resources", "");
            using (var fileStream = File.OpenRead(@".\resources\office365.png"))
            {
                fileSystemConnector.SaveFileStream("blabla.png", fileStream);
            }

            // delete the file
            fileSystemConnector.DeleteFile("blabla.png");

            //read the file
            using (var bytes = fileSystemConnector.GetFileStream("blabla.png"))
            {
                Assert.IsNull(bytes);
            }

            // file will be deleted at end of test
        }

        /// <summary>
        /// Save file to default container
        /// </summary>
        [TestMethod]
        public void FileConnectorDelete2Test()
        {
            // upload the file
            FileSystemConnector fileSystemConnector = new FileSystemConnector(@".", "wrong");
            using (var fileStream = File.OpenRead(@".\resources\office365.png"))
            {
                fileSystemConnector.SaveFileStream("blabla.png", @"Resources\Templates", fileStream);
            }

            // delete the file
            fileSystemConnector.DeleteFile("blabla.png", @"Resources\Templates");

            //read the file
            using (var bytes = fileSystemConnector.GetFileStream("blabla.png", @"Resources\Templates"))
            {
                Assert.IsNull(bytes);
            }

            // file will be deleted at end of test
        }

        #endregion File connector tests
    }
}