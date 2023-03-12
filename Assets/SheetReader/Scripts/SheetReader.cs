using System;
using System.Collections.Generic;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

using UnityEngine;

namespace SC.SheetReader
{
    public class SheetReader
    {
        private string _spreadsheetId;

        private SheetsService service;

        public SheetReader(SheetReaderInfo readerInfo)
        {
            _spreadsheetId = readerInfo.SpreadsheetId;
        
            service = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = GoogleCredential
                    .FromJson(readerInfo.Credentials.text)
                }
            );
        }

        public IList<IList<object>> GetSheetRange(String sheetNameAndRange)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(_spreadsheetId, sheetNameAndRange);

            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                return values;
            }
            else
            {
                Debug.Log("No data found.");
                return null;
            }
        }
    }
}