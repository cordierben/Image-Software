using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projet_Info
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[] octet = { 7, 2, 8, 8 };
            MyImage test = new MyImage("test");
            int resultat = test.convertir_endian_to_int(octet);
            byte[] octet2 = test.convertir_int_to_endian(resultat);
            Assert.AreEqual(octet[0], octet2[0]);
        }

        [TestMethod]
        public void TestMethod2()
        {
            byte[,][] noiretblanc = new byte[5, 5][];
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<5;j++)
                {
                    noiretblanc[i, j] = new byte[3];
                    noiretblanc[i, j][0] = 165;
                    noiretblanc[i, j][1] = 4;
                    noiretblanc[i, j][2] = 9;
                }
            }
            Manip test = new Manip(noiretblanc);
            byte[,][] noiretblanc2 = test.noirblanc();
            Assert.Equals(165, noiretblanc2[0, 0][0]);
        }
    }
}
