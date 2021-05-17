namespace backend.Models
{
    public class AppSettings
    {
        public static AppSettings appSettings { get; set; }
        public string JwtSecret { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }
        public string BucketName { get; set; }
        public string FurnitureDir { get; set; }
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
        public string Region { get; set; }
    }
}