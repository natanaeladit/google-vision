using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using System;

namespace google_vision
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DetectText();
            //Console.WriteLine(GetBuildNumberAndRevision());

            /*System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTime parsedDate = DateTime.ParseExact("20200224", "yyyyMMdd", provider);
            TimeSpan parseTime = TimeSpan.ParseExact("183316", "hhmmss", provider);

            DateTime dateTime = parsedDate + parseTime;
            Console.WriteLine(dateTime.ToString());*/

            Console.ReadLine();
        }

        private static string GetBuildNumberAndRevision()
        {
            System.DateTime utcNow = System.DateTime.UtcNow;
            TimeSpan timeofDay = System.DateTime.UtcNow.TimeOfDay;
            string buildNumber = Math.Round((utcNow - System.DateTime.Parse("2000-01-01")).TotalDays).ToString();
            string rev = Math.Round((timeofDay).TotalSeconds / 2).ToString();
            return string.Join(".", buildNumber, rev);
        }

        public static void DetectText()
        {
            // Instantiates a client
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromFile(@"test.png");
            // Performs label detection on the image file
            var response = client.DetectLabels(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Console.WriteLine(annotation.Description);
            }
        }

        public static object AuthImplicit(string projectId)
        {
            // If you don't specify credentials when constructing the client, the
            // client library will look for credentials in the environment.
            var credential = GoogleCredential.GetApplicationDefault();
            var storage = StorageClient.Create(credential);
            // Make an authenticated API request.
            var buckets = storage.ListBuckets(projectId);
            foreach (var bucket in buckets)
            {
                Console.WriteLine(bucket.Name);
            }
            return null;
        }
    }
}
