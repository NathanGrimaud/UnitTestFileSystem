using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystem;
using System.Collections.Generic;
namespace UnitTestFileSystem
{
    [TestClass]
    public class UnitTest1
    {
        public Directory racine;
        public File courrant;
        public File file;

        [TestInitialize]
        public void SetUp()
        {
            racine = new Directory("root") {path = "#",isRoot = true };
            courrant = racine;
        }
        [TestMethod]
        public void tCreationRacine()
        {
            Assert.AreEqual(true, racine.isRoot);
        }
        [TestMethod]
        public void tRacineFalse()
        {
            courrant.isRoot = false;
            Assert.AreEqual(false, courrant.isRoot);
        }
        [TestMethod]
        public void tGetPathTrue()
        {
            racine.chmod(7);
            string path = racine.getPath();
            Assert.AreEqual("#", path);
        }
        [TestMethod]
        public void tGetPathFalse()
        {
            racine.chmod(7);
            string path = racine.getPath();
            Assert.AreNotEqual("", path);
        }
        [TestMethod]
        public void tRenameDirTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            Assert.AreEqual(true, dir.rename("next", "dir"));
        }
        
        [TestMethod]
        public void tRenameFileTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.create("next");            
            Assert.AreEqual(true, dir.rename("next","File"));
        }
        [TestMethod]
        public void tRenameDirFalseNom()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            Assert.AreEqual(false, dir.rename("directory", "dir"));
        }
        [TestMethod]
        public void tRenameDirFalsePerm()
        {
            courrant.chmod(0);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            Assert.AreEqual(false, dir.rename("next", "dir"));
        }
        [TestMethod]
        public void tRenameFileFalseNom()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.create("next");
            Assert.AreEqual(false, dir.rename("directory", "dir"));
        }
       [TestMethod]
        public void chmod7True()
        {
            racine.chmod(7);
            Assert.AreEqual(true, racine.canWrite());
        }
       [TestMethod]
       public void chmod7False()
       {
           racine.chmod(7);
           Assert.AreNotEqual(6, racine.permission);
       }
        [TestMethod]
       public void chmod4True()
        {
            racine.chmod(4);
            Assert.AreEqual(true, racine.canRead());
        }
        [TestMethod]
        public void chmod4False()
        {
            racine.chmod(4);
            Assert.AreEqual(false, racine.canWrite());
        }
        [TestMethod]
        public void chmod1True()
        {
            racine.chmod(1);
            Assert.AreEqual(true, racine.canExecute());
        }
        [TestMethod]
        public void chmod1False()
        {
            racine.chmod(1);
            Assert.AreEqual(false, racine.canRead());
        }
        [TestMethod]
        public void tCDtrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            List<File> f = new List<File>();
            Assert.AreEqual(dir.search("next",f)[0], dir.cd("next"));            
        }
        [TestMethod]
        public void tCDfalse()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            Assert.AreNotEqual(dir, dir.cd("next"));   
        }
        [TestMethod]
        public void tCDfalsePerm()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("next");
            List<File> f = new List<File>();
            File file = dir.search("next", f)[0];
            courrant.chmod(0);
            Assert.AreNotEqual(f, dir.cd("next"));         
        }
       [TestMethod]
        public void tMkdirTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            Assert.AreEqual(true, dir.mkdir("next"));
        }
       [TestMethod]
       public void tMkdirNom()
       {
           courrant.chmod(7);
           Directory dir = (Directory)courrant;
           dir.mkdir("next");
           courrant = dir.cd("next");
           Assert.AreEqual("next", courrant.name);
       }
       [TestMethod]
       public void tCreateTrue()
       {
           courrant.chmod(7);
           Directory dir = (Directory)courrant;
           Assert.AreEqual(true, dir.create("file"));
       }
       [TestMethod]
       public void tDeleteDirTrue()
       {
           courrant.chmod(7);
           Directory dir = (Directory)courrant;
           dir.mkdir("Dir");
           Assert.AreEqual(true, dir.delete("Dir"));
       }
       [TestMethod]
       public void tDeleteFileFalse()
       {
           courrant.chmod(0);
           Directory dir = (Directory)courrant;
           dir.create("File");
           Assert.AreEqual(false, dir.delete("File"));
       }
        [TestMethod]
       public void tSearchFileTrue()
       {
           courrant.chmod(7);
           Directory dir = (Directory)courrant;
           dir.create("File");
           File l = dir.cd("File");
           List<File> test = new List<File>();
           test.Add(l);
           List<File> search = new List<File>();
           search = dir.search("File",search);
           Assert.AreEqual(test[0],search[0] );
       }
        public void tSearchDirTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("dir");
            File l = dir.cd("dir");
            List<File> test = new List<File>();
            test.Add(l);
            List<File> search = new List<File>();
            search = dir.search("dir", search);
            Assert.AreEqual(test[0], search[0]);
        }
        [TestMethod]
        public void tSearchFileFalse()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.create("File");
            File l = dir.cd("File");
            List<File> test = new List<File>();
            test.Add(l);
            List<File> search = new List<File>();
            search = dir.search("Files", search);
            Assert.AreNotEqual(test.Count, search.Count);
        }
        [TestMethod]
        public void tSearchDirFalse()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("Dir");
            File l = dir.cd("Dir");
            List<File> test = new List<File>();
            test.Add(l);
            List<File> search = new List<File>();
            search = dir.search("Dirs", search);
            Assert.AreNotEqual(test.Count, search.Count);
        }
        [TestMethod]
        public void tLsTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.create("File");
            File l = dir.cd("File");
            List<File> test = new List<File>();
            test.Add(l);
            List<File> search = new List<File>();
            search = dir.ls();
            Assert.AreEqual(test[0], search[0]);
        }
        [TestMethod]
        public void tLsFalse()
        {
            courrant.chmod(0);
            Directory dir = (Directory)courrant;
            dir.create("File");
            File l = dir.cd("File");
            List<File> test = new List<File>();
            test.Add(l);
            List<File> search = new List<File>();
            search = dir.ls();
            Assert.AreNotEqual(test.Count, search.Count);
        }
        [TestMethod]
        public void tGetParentTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("Dir");
            File cd = dir.cd("Dir");
            Assert.AreEqual(dir, cd.getParent());
        }

        [TestMethod]
        public void tGetRootTrue()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            dir.mkdir("Dir");
            File cd = dir.cd("Dir");
            Assert.AreEqual(true, cd.getRoot().isRoot);
        }

        [TestMethod]
        public void tNameTrue()
        {
            courrant.chmod(7);
            Assert.AreEqual("root",courrant.name );
        }
        [TestMethod]
        public void tNameFalsePerm()
        {
            courrant.chmod(7);
            Assert.AreNotEqual("#", courrant.name);
        }
        [TestMethod]
        public void tGetFileTrue()
        {
            courrant.chmod(7);
            File test = new File((Directory)courrant, "test");
            Assert.AreEqual(true, test.getFile());
        }
        [TestMethod]
        public void tGetFileFalse()
        {
            courrant.chmod(7);
            Directory dir = (Directory)courrant;
            Assert.AreNotEqual(true,dir.getFile());
        }
    }
}
