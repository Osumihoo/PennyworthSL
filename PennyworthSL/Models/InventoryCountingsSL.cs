using System.ComponentModel.DataAnnotations;

namespace PennyworthSL.Models
{
    public class InventoryCountingsSL
    {

        [Required]
        public string CountingType { get; set; } = String.Empty;
        [Required]
        public List<InventoryCountingLines> InventoryCountingLines { get; set; } = new List<InventoryCountingLines>();
        

    }
    public class InventoryCountingLines
    {
        [Required]
        public int LineNumber { get; set; } 
        [Required]
        public string ItemCode { get; set; } = String.Empty;
        [Required]
        public string Freeze { get; set; } = String.Empty;
        [Required]
        public string WarehouseCode { get; set; } = String.Empty;
        [Required]
        public int BinEntry { get; set; }
        [Required]
        public string Counted { get; set; } = String.Empty;
        [Required]
        public int CountedQuantity { get; set; }
        [Required]
        public List<InventoryCountingBatchNumbers> InventoryCountingBatchNumbers { get; set; } = new List<InventoryCountingBatchNumbers> { };
    }

    public class InventoryCountingBatchNumbers
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string BatchNumber { get; set; } = String.Empty;

        public int BaseLineNumber { get; set; } 
    }
}
