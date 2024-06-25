namespace DollarInfo.Utils
{
    public static class Constants
    {
        public const string EmailFrom = "dolarinformation@gmail.com";
        public const string Subject = "[Bug] - Reporte de Bug";

        public const string BugReportBody = @"<!DOCTYPE html>
                                            <html>
                                            <head>
                                                <link href=""https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600&display=swap"" rel=""stylesheet"">
                                                <style>
                                                    .header {
                                                        color: black;
                                                        border: 2px solid #8C08B9;
                                                        padding: 10px;
                                                        text-align: center;
                                                        border-radius: 15px;
                                                    }

                                                    body {
                                                        font-family: 'Montserrat', sans-serif;
                                                        font-size: 14px;
                                                    }

                                                    .content {
                                                        padding: 20px;
                                                        text-align: center;
                                                        font-family: 'Montserrat', sans-serif;
                                                        font-size: 14px;
                                                    }
                                                </style>
                                            </head>
                                            <body>
                                                <div class='header'>
                                                    <h1>Reporte de Bug</h1>
                                                </div>
                                                <div class='content'>
                                                    <p>Hola!</p>
                                                    <p>El usuario <strong>{{UserName}}</strong> con el email <strong>{{UserEmail}}</strong> ha reportado un nuevo bug:</p>
                                                    <p>{{BugDescription}}</p>
                                                    <p>El bug fue reportado el dia <strong>{{CurrentDate}}</strong></p>
                                                </div>
                                            </body>
                                            </html>";

        public class DateFormats
        {
            public class Dash
            {
                public const string Default = "yyyy-MM-dd";
            }

            public class Slashes
            {
                public const string DayFirst = "dd/MM/yyyy";
                public const string MonthFirst = "MM/dd/yyyy HH:mm:ss";
            }
        }
    }
}