using AlphaShop.Application.Common.DTOs;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> ZarinpalRequestSend(int orderId, int totalCost, string callBackUrl);
        Task<PaymentVerifyResultDto> ZarinPalVerifyData(string authority, int amount);
    }
}
