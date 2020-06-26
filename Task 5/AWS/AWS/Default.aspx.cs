using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3;
using Amazon.S3.Model;

namespace AWS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // Display the Image
            {
                System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
                Byte[] bytes = br.ReadBytes((Int32)stream.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                Image1.Visible = true;

                var awsKey = "AKIATZH7I3Q72EVZSWNK";
                var awsSecretKey = "ZgcsP1L7WdJeXLOaj6xKb+nuT/cZ978jH8GYJenC";
                var bucketRegion = Amazon.RegionEndpoint.USEast1;   // Your bucket region
                var s3 = new AmazonS3Client(awsKey, awsSecretKey, bucketRegion);
                var putRequest = new PutObjectRequest();
                putRequest.BucketName = "phi2020";        // Your bucket name
                putRequest.ContentType = "image/jpeg";
                putRequest.InputStream = FileUpload1.PostedFile.InputStream;
                // key will be the name of the image in your bucket
                putRequest.Key = FileUpload1.FileName;
                Debug.WriteLine("Filename = " + FileUpload1.FileName);
                PutObjectResponse putResponse = s3.PutObject(putRequest);
            }
        }
    }
}
