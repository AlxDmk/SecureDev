using CardStorageService.Data;
using System.Collections.Generic;

namespace CardStorageService.Controllers.Models.Requests
{
    public class GetByClientIdCardsResponse : IOperationResult
    {
        public int ErrorCode {get;set;}

        public string ErrorMessage {get;set;}

        public IList<CardDto> Cards { get; set; }
    }
}
