using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RTSGameTests
{
    [TestClass]
    public class ResourceTests
    {
        
        [TestMethod]
        public void OperatorMinus()
        {

            Resources testObj = new Resources();
            for (int i = 0; i < testObj.ResourceArray.Length; i++)
            {
                testObj.ResourceArray[i] = i * 2;
            }
            Resources testObj2 = new Resources();
            for (int i = 0; i < testObj2.ResourceArray.Length; i++)
            {
                testObj2.ResourceArray[i] = i;
            }
            Resources result = testObj - testObj2;
            for (int i = 0; i < result.ResourceArray.Length; i++)
            {
                Assert.AreEqual(i, result.ResourceArray[i]);
            }
        }
        [TestMethod]
        public void OperatorPlus()
        {

        }
    }
}
