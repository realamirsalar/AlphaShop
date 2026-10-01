using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Infrastructure.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AlphaShop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PaymentService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<PaymentResponseDTO> ZarinpalRequestSend(int orderId, int totalCost, string callBackUrl)
        {
            try
            {
                ZarinpalRequestDTO zarinpalRequest = new ZarinpalRequestDTO()
                {
                    MerchantId = _configuration.GetValue<string>(SD.ZarinPalMerchantId),
                    Amount = totalCost,
                    Description = "این یک تست است و من درحال آموزش هستم!",
                    CallbackUrl = callBackUrl
                };
                var jsonRequest = JsonSerializer.Serialize(zarinpalRequest);
                var contentRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var requestUrl = _configuration.GetValue<string>(SD.ZarinPalPaymentRequestUrl);
                var response = await _httpClient.PostAsync(requestUrl, contentRequest);
                var responseString = await response.Content.ReadAsStringAsync();
                using var responseJson = JsonDocument.Parse(responseString);


                if (!(responseJson.RootElement.TryGetProperty("data", out JsonElement dataNode) &&
                    dataNode.ValueKind == JsonValueKind.Object))
                {
                    return new PaymentResponseDTO()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ارتباط با درگاه پرداخت برقرار نشد. لطفاً دقایقی دیگر تلاش کنید."
                    };
                }
                if (!(dataNode.TryGetProperty("code", out JsonElement codeNode) && codeNode.TryGetInt32(out int code) && code == 100))
                {
                    var zarinpalSpecificMessage = "خطای نامشخص از طرف زرین پال!";
                    if (dataNode.TryGetProperty("message", out JsonElement zarinpalMsgNode) && zarinpalMsgNode.ValueKind == JsonValueKind.String)
                    {
                        zarinpalSpecificMessage = zarinpalMsgNode.GetString();
                    }
                    return new PaymentResponseDTO()
                    {
                        IsSuccess = false,
                        ErrorMessage = "پرداخت با خطا مواجه شد. " + zarinpalSpecificMessage
                    };
                }
                if (!(dataNode.TryGetProperty("authority", out JsonElement authorityNode)
                            && authorityNode.ValueKind == JsonValueKind.String))
                {
                    return new PaymentResponseDTO()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ارتباط با درگاه پرداخت برقرار نشد. لطفاً دقایقی دیگر تلاش کنید."
                    };

                }
                string authority = authorityNode.GetString();

                //........... update oreder ===> Move to Controller


                var paymentGetWay = _configuration.GetValue<string>(SD.ZarinPalPaymentGatewayUrl);
                string paymentUrl = paymentGetWay + authority;
                return new PaymentResponseDTO()
                {
                    IsSuccess = true,
                    PaymentUrl = paymentUrl,
                    Authority = authority,/////
                    InfoMessage = "درحال انتقال به درگاه پرداخت ...."
                };

            }
            catch (HttpRequestException ex)
            {
                return new PaymentResponseDTO()
                {
                    IsSuccess = false,
                    ErrorMessage = "ارتباط با درگاه پرداخت برقرار نشد. لطفاً دقایقی دیگر تلاش کنید."
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponseDTO()
                {
                    IsSuccess = false,
                    ErrorMessage = "یک خطای سیستمی رخ داده است. با پشتیبانی تماس بگیرید."
                };
            }
        }


        public async Task<PaymentVerifyResultDto> ZarinPalVerifyData(string authority, int amount)
        {
            try
            {
                //var order = _unitOfWork.OrderHeaderRepository.GetByAuthority(authority);
                //if (order == null)
                //{
                //    return new PaymentVerifyResultDto()
                //    {
                //        IsSuccess = false,
                //        ErrorMessage = "سفارش یافت نشد!"
                //    };
                //}

                //if (order.PaymentState == SD.StatusApproved)
                //{
                //    return new PaymentVerifyResultDto()
                //    {
                //        IsSuccess = true, // چون قبلا با موفقیت پرداخت شده
                //        OrderId = order.Id,
                //        ErrorMessage = "این سفارش قبلا با موفقیت پرداخت شده است."
                //    };
                //}


                var verifyData = new ZarinPalResponseDto()
                {
                    MerchantId = _configuration.GetValue<string>(SD.ZarinPalMerchantId),
                    Authority = authority,
                    Amount = amount
                };

                var json = JsonSerializer.Serialize(verifyData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // آدرس سندباکس برای تایید پرداخت
                var verifyUrl = _configuration.GetValue<string>(SD.ZarinPalPaymentVerificationUrl);
                var response =
                    await _httpClient.PostAsync(verifyUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                using var jsonDoc = JsonDocument.Parse(responseString);

                if (!(jsonDoc.RootElement.TryGetProperty("data", out JsonElement dataNode) &&
                   dataNode.ValueKind == JsonValueKind.Object))
                {
                    return new PaymentVerifyResultDto()
                    {
                        IsSuccess = false,
                        ErrorMessage = "پرداخت ناموفق بود!"
                    };
                }

                else if (dataNode.TryGetProperty("code", out JsonElement codeNode))
                {
                    int code = codeNode.GetInt32();

                    if (code == 100 || code == 101)
                    {
                        string refId = "0";
                        if (dataNode.TryGetProperty("ref_id", out var refIdNode))
                        {
                            refId = refIdNode.GetInt64().ToString();
                        }

                        //// Update order .......

                        //_shoppingCartService.UpdateStatus(order.Id, SD.StatusApproved);
                        //_shoppingCartService.UpdatePaymentStatus(order.Id, authority, SD.ZarinPalGateway, refId);


                        return new PaymentVerifyResultDto()
                        {
                            IsSuccess = true,
                            RefId = refId,
                            ErrorMessage = "پرداخت موفق بود!"
                        };

                    }
                }

                return new PaymentVerifyResultDto()
                {
                    IsSuccess = false,
                    ErrorMessage = "پرداخت ناموفق بود!"
                };

            }
            catch (HttpRequestException ex)
            {
                return new PaymentVerifyResultDto()
                {
                    IsSuccess = false,
                    ErrorMessage = "خطا در ارتباط با درگاه پرداخت!"
                };
            }
            catch (Exception e)
            {
                return new PaymentVerifyResultDto()
                {
                    IsSuccess = false,
                    ErrorMessage = "یک خطای ناشناخته در زمان تایید پرداخت رخ داد.!"
                };
            }
        }
    }
}
