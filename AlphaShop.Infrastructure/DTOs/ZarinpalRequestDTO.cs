using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AlphaShop.Infrastructure.DTOs
{
    public class ZarinpalRequestDTO
    {
        [JsonPropertyName("merchant_id")]
        public string MerchantId { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }
    }
}
