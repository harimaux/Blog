using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class StockAvatars
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
