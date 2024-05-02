using System.ComponentModel.DataAnnotations;

namespace PennyworthSL.Models
{
    public class InventoryPostingSL
    {
        [Required]
        public string JournalRemark { get; set; } = String.Empty;
        [Required]
        public List<InventoryPostingLines> InventoryPostingLines { get; set; } = new List<InventoryPostingLines>();

    }
    public class InventoryPostingLines
    {
        [Required]
        public string ItemCode { get; set; } = String.Empty;
        [Required]
        public string WarehouseCode { get; set; } = String.Empty;
        [Required]
        public int BinEntry { get; set; }
        [Required]
        public int CountedQuantity { get; set; }
        [Required]
        public int BaseEntry { get; set; }
        [Required]
        public int BaseLine { get; set; }
        [Required]
        public string BaseReference { get; set; } = String.Empty;
        [Required]
        public List<InventoryPostingBatchNumbers> InventoryPostingBatchNumbers { get; set; } = new List<InventoryPostingBatchNumbers>();
    }

    public class InventoryPostingBatchNumbers
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string BatchNumber { get; set; } = String.Empty;
        [Required]
        public int BaseLineNumber { get; set; } 

            
    }
}
