using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AlphaShop.Infrastructure.DTOs
{
    public class ZarinPalResponseDto
    {

        [JsonPropertyName("merchant_id")]
        public string MerchantId { get; set; }
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
        [JsonPropertyName("authority")]
        public string Authority { get; set; }
    }
}
