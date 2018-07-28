﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using OPS.protocol;
using System.Collections.Generic;

namespace OPSTests.util
{
    [TestClass]
    public class SFTPTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SFTPHelperTest()
        {
            SFTPHelper sftp = new OPS.protocol.SFTPHelper("192.168.164.131", "22", "root", "123456");
            ArrayList files = sftp.GetFileList("/data/logs");
            foreach (string s in files)
            {
             Console.WriteLine(s);
            }
            Assert.IsTrue(files.ToArray().Length > 0);
        }

        [TestMethod]
        public void SFtpClientTest()
        {
            SFtpClient ftp = new SFtpClient("192.168.164.131", 22, "root", "123456");
            ftp.Connect();
            List<FtpFile> files = ftp.getFileList("/data/logs/a");
            Console.WriteLine("file count:"+ files.ToArray().Length);
            foreach (FtpFile f in files) {
                Console.WriteLine(f.fullname);
            }
        }

        [TestMethod]
        public void SFtpClientTestDownload()
        {
            SFtpClient ftp = new SFtpClient("192.168.164.131", 22, "root", "123456");
            ftp.Connect();
            List<FtpFile> files = ftp.getFileList("/data/logs/a");
            Console.WriteLine("file count:" + files.ToArray().Length);
            foreach (FtpFile f in files)
            {
                Console.WriteLine(f.fullname);
                if (!f.isDirectory)
                {
                    DownloadAsyncResult result = new DownloadAsyncResult(1,1);
                    AsyncCallback callback = new AsyncCallback(DownloadCallBack);
                    ftp.DownloadFile(f.fullname, "E:\\" + f.name, callback);

                    Console.WriteLine("download completeed!");
                }
            }
        }

        private void DownloadCallBack(IAsyncResult result) {
            Console.WriteLine(result.AsyncState);
        }
    }
}
