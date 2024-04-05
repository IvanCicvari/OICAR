namespace Admin.Models
{
    public class VideoViewModel
    {
        public int Idvideo { get; set; }

        public int? UserId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? UploadDate { get; set; }

        public string? ThumbnailUrl { get; set; }

        public string? VideoUrl { get; set; }

        public int? Duration { get; set; }

        public string? Categories { get; set; }

        public string? PrivacySetting { get; set; }

        public int? TotalLikes { get; set; }

        public int? TotalViews { get; set; }

        public int? TotalSubscribers { get; set; }
    }
}
