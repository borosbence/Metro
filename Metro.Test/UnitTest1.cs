using Metro.Model;
using Metro.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Metro.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"../../Data/metro.xlsx")]
        public void FileExist()
        {

            var myfile = "metro.xlsx";
            Assert.IsTrue(File.Exists(myfile));
        }

        [TestMethod]
        public void VonalakSzama()
        {
            var repo = new MetroRepository("metro.xlsx");
            Assert.AreEqual(4, repo.MetroVonalak.Count);
        }

        [TestMethod]
        public void ParkokSzama()
        {
            var repo = new MetroRepository("metro.xlsx");
            var parkMegallo = repo.Allomasok.FindAll(x => x.AllomasNev.Contains("tér")).Count;
            Assert.IsFalse(parkMegallo < 5);
        }
    }
}
