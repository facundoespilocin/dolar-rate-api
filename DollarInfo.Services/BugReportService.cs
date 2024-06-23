﻿using AutoMapper;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Interfaces;
using DollarInfo.Utils;
using DollarInfo.Utils.EmailService;
using System.Reflection;

namespace DollarInfo.Services
{
    public class BugReportService : IBugReportService
    {
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly TemplateService _templateService;

        public BugReportService(
            IMapper mapper,
            EmailService emailService,
            TemplateService templateService)
        {
            _mapper = mapper;
            _emailService = emailService;
            _templateService = templateService;
        }

        public async Task Post(BugReportRequest request)
        {
            string template = await _templateService.GetTemplateAsync("BugReport.html");

            string emailBody = template
                .Replace("{{UserName}}", request.BugReport.Name)
                .Replace("{{BugDescription}}", request.BugReport.Description)
                .Replace("{{CurrentDate}}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

            await _emailService.SendEmailAsync(Constants.EmailFrom, Constants.Subject, emailBody);
        }
    }
}