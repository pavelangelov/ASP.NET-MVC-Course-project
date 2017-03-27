using System.Web;

namespace Bg_Fishing.Tests.MvcClient.Mocks
{
    public class MockHttpPostedFileBase : HttpPostedFileBase
    {
        private int contentLength;

        public override int ContentLength
        {
            get
            {
                return this.contentLength;
            }
        }

        public override string FileName
        {
            get
            {
                return "Test file";
            }
        }

        public bool IsSaveAsCalled { get; set; }

        public void SetContentLength(int value)
        {
            this.contentLength = value;
        }

        public override void SaveAs(string filename)
        {
            this.IsSaveAsCalled = true;
        }
    }
}
